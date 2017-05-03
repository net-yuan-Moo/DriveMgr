/***********************
 @ author:zlong
 @ Date:2015-01-11
 @ Desc:差旅费用界面层
 * ********************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DriveMgr;
using DriveMgr.BLL;

namespace DriveMgr.WebUI.FinancialMgr
{
    /// <summary>
    /// bg_travelHandler 的摘要说明
    /// </summary>
    public class bg_travelHandler : IHttpHandler
    {
        DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
        TravelBLL travelBll = new TravelBLL();
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
                        SearchTravel(context);
                        break;
                    case "add":
                        AddTravel(userFromCookie, context);
                        break;
                    case "edit":
                        EditTravel(userFromCookie, context);
                        break;
                    case "delete":
                        DelTravel(userFromCookie, context);
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
        /// 查询差旅费用
        /// </summary>
        /// <param name="context"></param>
        private void SearchTravel(HttpContext context)
        {
            string sort = context.Request.Params["sort"];  //排序列
            string order = context.Request.Params["order"];  //排序方式 asc或者desc
            int pageindex = int.Parse(context.Request.Params["page"]);
            int pagesize = int.Parse(context.Request.Params["rows"]);

            string ui_travel_travelUse = context.Request.Params["ui_travel_travelUse"] ?? "";
            string ui_travel_travelPerson = context.Request.Params["ui_travel_travelPerson"] ?? "";
            string ui_travel_createStartDate = context.Request.Params["ui_travel_createStartDate"] ?? "";
            string ui_travel_createEndDate = context.Request.Params["ui_travel_createEndDate"] ?? "";

            int totalCount;   //输出参数
            string strJson = travelBll.GetPagerData(ui_travel_travelUse, ui_travel_travelPerson, ui_travel_createStartDate, ui_travel_createEndDate, sort + " " + order, pagesize, pageindex, out totalCount);
            context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

            userOperateLog.OperateInfo = "查询差旅费用";
            userOperateLog.IfSuccess = true;
            userOperateLog.Description = "排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 添加差旅费用
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void AddTravel(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("travel", "add", userFromCookie.Id))
            {
                string ui_travel_TravelUse_add = context.Request.Params["ui_travel_TravelUse_add"] ?? "";
                string ui_travel_TravelPerson_add = context.Request.Params["ui_travel_TravelPerson_add"] ?? "";
                string ui_travel_TravelAmount_add = context.Request.Params["ui_travel_TravelAmount_add"] ?? "";
                string ui_travel_TraveDate_add = context.Request.Params["ui_travel_TraveDate_add"] ?? "";
                string ui_travel_Remark_add = context.Request.Params["ui_travel_Remark_add"] ?? "";

                DriveMgr.Model.TravelModel travelAdd = new Model.TravelModel();
                travelAdd.TravelUse = ui_travel_TravelUse_add.Trim();
                travelAdd.TravelPerson = ui_travel_TravelPerson_add.Trim();
                travelAdd.TravelAmount = decimal.Parse(ui_travel_TravelAmount_add);
                travelAdd.TraveDate = DateTime.Parse(ui_travel_TraveDate_add);
                travelAdd.Remark = ui_travel_Remark_add.Trim();

                travelAdd.CreateDate = DateTime.Now;
                travelAdd.CreatePerson = userFromCookie.UserId;
                travelAdd.UpdatePerson = userFromCookie.UserId;
                travelAdd.UpdateDate = DateTime.Now;

                if (travelBll.AddTravel(travelAdd))
                {
                    userOperateLog.OperateInfo = "添加差旅费用";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "添加成功，差旅用途：" + ui_travel_TravelUse_add.Trim();
                    context.Response.Write("{\"msg\":\"添加成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "添加差旅费用";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "添加失败";
                    context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "添加差旅费用";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 编辑差旅
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void EditTravel(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("travel", "edit", userFromCookie.Id))
            {
                int id = Convert.ToInt32(context.Request.Params["id"]);
                DriveMgr.Model.TravelModel travelEdit = travelBll.GetTravelModel(id);
                string ui_travel_TravelUse_edit = context.Request.Params["ui_travel_TravelUse_edit"] ?? "";
                string ui_travel_TravelPerson_edit = context.Request.Params["ui_travel_TravelPerson_edit"] ?? "";
                string ui_travel_TravelAmount_edit = context.Request.Params["ui_travel_TravelAmount_edit"] ?? "";
                string ui_travel_TraveDate_edit = context.Request.Params["ui_travel_TraveDate_edit"] ?? "";
                string ui_travel_Remark_edit = context.Request.Params["ui_travel_Remark_edit"] ?? "";

                travelEdit.TravelUse = ui_travel_TravelUse_edit.Trim();
                travelEdit.TravelPerson = ui_travel_TravelPerson_edit.Trim();
                travelEdit.TravelAmount = decimal.Parse(ui_travel_TravelAmount_edit);
                travelEdit.TraveDate = DateTime.Parse(ui_travel_TraveDate_edit);
                travelEdit.Remark = ui_travel_Remark_edit.Trim();

                travelEdit.UpdatePerson = userFromCookie.UserId;
                travelEdit.UpdateDate = DateTime.Now;

                if (travelBll.UpdateTravel(travelEdit))
                {
                    userOperateLog.OperateInfo = "修改差旅费用";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "修改成功，差旅费用主键：" + travelEdit.Id;
                    context.Response.Write("{\"msg\":\"修改成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "修改差旅费用";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "修改失败";
                    context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "修改差旅费用";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 删除差旅
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void DelTravel(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("travel", "delete", userFromCookie.Id))
            {
                string ids = context.Request.Params["id"].Trim(',');
                if (travelBll.DeleteTravelList(ids))
                {
                    userOperateLog.OperateInfo = "删除差旅费用";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "删除成功，差旅费用主键：" + ids;
                    context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "删除差旅费用";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "删除失败";
                    context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "删除差旅费用";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }
    }
}