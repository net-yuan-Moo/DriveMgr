/***********************
 @ author:zlong
 @ Date:2015-01-11
 @ Desc:业务招待费用界面层
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
    /// bg_businessEntertainHandler 的摘要说明
    /// </summary>
    public class bg_businessEntertainHandler : IHttpHandler
    {
        DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
        BusinessEntertainBLL businessEntertainBll = new BusinessEntertainBLL();
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
                        SearchBusinessEntertain(context);
                        break;
                    case "add":
                        AddBusinessEntertain(userFromCookie, context);
                        break;
                    case "edit":
                        EditBusinessEntertain(userFromCookie, context);
                        break;
                    case "delete":
                        DelBusinessEntertain(userFromCookie, context);
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
        /// 查询业务招待费用
        /// </summary>
        /// <param name="context"></param>
        private void SearchBusinessEntertain(HttpContext context)
        {
            string sort = context.Request.Params["sort"];  //排序列
            string order = context.Request.Params["order"];  //排序方式 asc或者desc
            int pageindex = int.Parse(context.Request.Params["page"]);
            int pagesize = int.Parse(context.Request.Params["rows"]);

            string ui_businessEntertain_entertainObject = context.Request.Params["ui_businessEntertain_entertainObject"] ?? "";
            string ui_businessEntertain_entertainUse = context.Request.Params["ui_businessEntertain_entertainUse"] ?? "";
            string ui_businessEntertain_transactor = context.Request.Params["ui_businessEntertain_transactor"] ?? "";
            string ui_businessEntertain_createStartDate = context.Request.Params["ui_businessEntertain_createStartDate"] ?? "";
            string ui_businessEntertain_createEndDate = context.Request.Params["ui_businessEntertain_createEndDate"] ?? "";

            int totalCount;   //输出参数
            string strJson = businessEntertainBll.GetPagerData(ui_businessEntertain_entertainObject, ui_businessEntertain_entertainUse, ui_businessEntertain_transactor,
                                                                                  ui_businessEntertain_createStartDate, ui_businessEntertain_createEndDate, sort + " " + order, pagesize, pageindex, out totalCount);
            context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

            userOperateLog.OperateInfo = "查询业务招待费用";
            userOperateLog.IfSuccess = true;
            userOperateLog.Description = "排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 添加业务招待费用
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void AddBusinessEntertain(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("businessEntertain", "add", userFromCookie.Id))
            {
                string ui_businessEntertain_EntertainObject_add = context.Request.Params["ui_businessEntertain_EntertainObject_add"] ?? "";
                string ui_businessEntertain_EntertainUse_add = context.Request.Params["ui_businessEntertain_EntertainUse_add"] ?? "";
                string ui_businessEntertain_Transactor_add = context.Request.Params["ui_businessEntertain_Transactor_add"] ?? "";
                string ui_businessEntertain_EntertainAmount_add = context.Request.Params["ui_businessEntertain_EntertainAmount_add"] ?? "";
                string ui_businessEntertain_TransactDate_add = context.Request.Params["ui_businessEntertain_TransactDate_add"] ?? "";
                string ui_businessEntertain_Remark_add = context.Request.Params["ui_businessEntertain_Remark_add"] ?? "";

                DriveMgr.Model.BusinessEntertainModel businessEntertainAdd = new Model.BusinessEntertainModel();
                businessEntertainAdd.EntertainObject = ui_businessEntertain_EntertainObject_add.Trim();
                businessEntertainAdd.EntertainUse = ui_businessEntertain_EntertainUse_add.Trim();
                businessEntertainAdd.Transactor = ui_businessEntertain_Transactor_add.Trim();
                businessEntertainAdd.EntertainAmount = decimal.Parse(ui_businessEntertain_EntertainAmount_add);
                businessEntertainAdd.TransactDate = DateTime.Parse(ui_businessEntertain_TransactDate_add);
                businessEntertainAdd.Remark = ui_businessEntertain_Remark_add.Trim();

                businessEntertainAdd.CreateDate = DateTime.Now;
                businessEntertainAdd.CreatePerson = userFromCookie.UserId;
                businessEntertainAdd.UpdatePerson = userFromCookie.UserId;
                businessEntertainAdd.UpdateDate = DateTime.Now;

                if (businessEntertainBll.AddBusinessEntertain(businessEntertainAdd))
                {
                    userOperateLog.OperateInfo = "添加业务招待费用";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "添加成功，招待对象：" + ui_businessEntertain_EntertainObject_add.Trim();
                    context.Response.Write("{\"msg\":\"添加成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "添加业务招待费用";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "添加失败";
                    context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "添加业务招待费用";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 编辑业务招待费用
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void EditBusinessEntertain(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("businessEntertain", "edit", userFromCookie.Id))
            {
                int id = Convert.ToInt32(context.Request.Params["id"]);
                DriveMgr.Model.BusinessEntertainModel businessEntertainEdit = businessEntertainBll.GetBusinessEntertainModel(id);
                string ui_businessEntertain_EntertainObject_edit = context.Request.Params["ui_businessEntertain_EntertainObject_edit"] ?? "";
                string ui_businessEntertain_EntertainUse_edit = context.Request.Params["ui_businessEntertain_EntertainUse_edit"] ?? "";
                string ui_businessEntertain_Transactor_edit = context.Request.Params["ui_businessEntertain_Transactor_edit"] ?? "";
                string ui_businessEntertain_EntertainAmount_edit = context.Request.Params["ui_businessEntertain_EntertainAmount_edit"] ?? "";
                string ui_businessEntertain_TransactDate_edit = context.Request.Params["ui_businessEntertain_TransactDate_edit"] ?? "";
                string ui_businessEntertain_Remark_edit = context.Request.Params["ui_businessEntertain_Remark_edit"] ?? "";

                businessEntertainEdit.EntertainObject = ui_businessEntertain_EntertainObject_edit.Trim();
                businessEntertainEdit.EntertainUse = ui_businessEntertain_EntertainUse_edit.Trim();
                businessEntertainEdit.Transactor = ui_businessEntertain_Transactor_edit.Trim();
                businessEntertainEdit.EntertainAmount = decimal.Parse(ui_businessEntertain_EntertainAmount_edit);
                businessEntertainEdit.TransactDate = DateTime.Parse(ui_businessEntertain_TransactDate_edit);
                businessEntertainEdit.Remark = ui_businessEntertain_Remark_edit.Trim();

                businessEntertainEdit.UpdatePerson = userFromCookie.UserId;
                businessEntertainEdit.UpdateDate = DateTime.Now;

                if (businessEntertainBll.UpdateBusinessEntertain(businessEntertainEdit))
                {
                    userOperateLog.OperateInfo = "修改业务招待费用";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "修改成功，业务招待费用主键：" + businessEntertainEdit.Id;
                    context.Response.Write("{\"msg\":\"修改成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "修改业务招待费用";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "修改失败";
                    context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "修改业务招待费用";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 删除业务招待费用
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void DelBusinessEntertain(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("businessEntertain", "delete", userFromCookie.Id))
            {
                string ids = context.Request.Params["id"].Trim(',');
                if (businessEntertainBll.DeleteBusinessEntertainList(ids))
                {
                    userOperateLog.OperateInfo = "删除业务招待费用";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "删除成功，业务招待费用主键：" + ids;
                    context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "删除业务招待费用";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "删除失败";
                    context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "删除业务招待费用";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }
    }
}