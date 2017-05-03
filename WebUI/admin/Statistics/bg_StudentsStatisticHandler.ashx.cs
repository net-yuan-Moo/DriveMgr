using DriveMgr.BLL;
using DriveMgr.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DriveMgr.WebUI.admin.Statistics
{
    /// <summary>
    /// bg_StudentsStatisticHandler 的摘要说明
    /// </summary>
    public class bg_StudentsStatisticHandler : IHttpHandler
    {
        DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
        RegistrationBLL bll = new RegistrationBLL();

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
                        GetStudentsInfo(context);
                        break;
                    default:
                        context.Response.Write("{\"msg\":\"参数错误！\",\"success\":false}");
                        break;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"msg\":\"" + DriveMgr.Common.JsonHelper.StringFilter(ex.Message) + "\",\"success\":false}");
                userOperateLog.OperateInfo = "学员学习情况统计功能异常";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = DriveMgr.Common.JsonHelper.StringFilter(ex.Message);
                DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
            }
        }

        /// <summary>
        /// 查询学员情况
        /// </summary>
        /// <param name="context"></param>
        private void GetStudentsInfo(HttpContext context)
        {
            int checkedPeriod = Int32.Parse(context.Request.Params["checkedPeriod"]);
            //string peridName = context.Request.Params["peridName"];
            //int chartType = (int)HighchartTypeEnum.柱状图;
            //HighchartTypeEnum type = (HighchartTypeEnum)Enum.Parse(typeof(HighchartTypeEnum), chartType.ToString());
            DataTable dt = bll.GetStudentsByPeroid(checkedPeriod).Tables[0];

            //HighChartOptions chart = bll.GetStudentsHighchart(type, checkedPeriod, peridName);
            //var tmd = new { value = chart, label = type.ToString() };

            context.Response.Write(dt.ToJson());

            userOperateLog.OperateInfo = "查询学员学习情况";
            userOperateLog.IfSuccess = true;
            userOperateLog.Description = "";
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}