using DriveMgr.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DriveMgr.WebUI.admin.StudentsMgr
{
    /// <summary>
    /// payListHandler 的摘要说明
    /// </summary>
    public class payListHandler : IHttpHandler
    {
        DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象

        RegistrationBLL registrationBLL = new RegistrationBLL();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request.Params["action"];

            try
            {
                DriveMgr.Model.User userFromCookie = DriveMgr.Common.UserHelper.GetUser(context);   //获取cookie里的用户对象
                userOperateLog = new Model.UserOperateLog();
                userOperateLog.UserIp = context.Request.UserHostAddress;
                userOperateLog.UserName = userFromCookie.UserId;
                switch (action)
                {
                    case "search":
                        GetPayListOfStudents(context);
                        break;
                    default:
                        context.Response.Write("{\"msg\":\"参数错误！\",\"success\":false}");
                        break;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"msg\":\"" + DriveMgr.Common.JsonHelper.StringFilter(ex.Message) + "\",\"success\":false}");
                userOperateLog.OperateInfo = "用户功能异常";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = DriveMgr.Common.JsonHelper.StringFilter(ex.Message);
                DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 得到缴费列表信息
        /// </summary>
        /// <param name="context"></param>
        private void GetPayListOfStudents(HttpContext context)
        {
            string sort = context.Request.Params["sort"];  //排序列
            string order = context.Request.Params["order"];  //排序方式 asc或者desc
            int pageindex = int.Parse(context.Request.Params["page"]);
            int pagesize = int.Parse(context.Request.Params["rows"]);

            string ui_students_period = context.Request.Params["ui_students_period"] ?? "";
            string ui_students_name = context.Request.Params["ui_students_name"] ?? "";
            string ui_students_code = context.Request.Params["ui_students_code"] ?? "";
           
            int totalCount;   //输出参数

            string strJson = registrationBLL.GetPayListOfPagerData(ui_students_period,ui_students_name,ui_students_code,sort + " " + order, pagesize, pageindex, out totalCount);
            context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

            userOperateLog.OperateInfo = "查询缴费列表信息";
            userOperateLog.IfSuccess = true;
            userOperateLog.Description = "排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }
    }
}