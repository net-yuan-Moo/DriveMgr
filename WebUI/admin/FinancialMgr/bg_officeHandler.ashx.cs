/***********************
 @ author:zlong
 @ Date:2015-01-11
 @ Desc:办公费用界面层
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
    /// bg_officeHandler 的摘要说明
    /// </summary>
    public class bg_officeHandler : IHttpHandler
    {
        DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
        OfficeBLL officeBll = new OfficeBLL();
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
                        SearchOffice(context);
                        break;
                    case "add":
                        AddOffice(userFromCookie, context);
                        break;
                    case "edit":
                        EditOffice(userFromCookie, context);
                        break;
                    case "delete":
                        DelOffice(userFromCookie, context);
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
        /// 查询办公费用
        /// </summary>
        /// <param name="context"></param>
        private void SearchOffice(HttpContext context)
        {
            string sort = context.Request.Params["sort"];  //排序列
            string order = context.Request.Params["order"];  //排序方式 asc或者desc
            int pageindex = int.Parse(context.Request.Params["page"]);
            int pagesize = int.Parse(context.Request.Params["rows"]);

            string ui_office_officeUse = context.Request.Params["ui_office_officeUse"] ?? "";
            string ui_office_tagPerson = context.Request.Params["ui_office_tagPerson"] ?? "";
            string ui_office_createStartDate = context.Request.Params["ui_office_createStartDate"] ?? "";
            string ui_office_createEndDate = context.Request.Params["ui_office_createEndDate"] ?? "";

            int totalCount;   //输出参数
            string strJson = officeBll.GetPagerData(ui_office_officeUse, ui_office_tagPerson, ui_office_createStartDate, ui_office_createEndDate, sort + " " + order, pagesize, pageindex, out totalCount);
            context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

            userOperateLog.OperateInfo = "查询办公费用";
            userOperateLog.IfSuccess = true;
            userOperateLog.Description = "排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 添加办公费用
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void AddOffice(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("office", "add", userFromCookie.Id))
            {
                string ui_office_OfficeUse_add = context.Request.Params["ui_office_OfficeUse_add"] ?? "";
                string ui_office_TagPerson_add = context.Request.Params["ui_office_TagPerson_add"] ?? "";
                string ui_office_OfficeAmount_add = context.Request.Params["ui_office_OfficeAmount_add"] ?? "";
                string ui_office_UseDate_add = context.Request.Params["ui_office_UseDate_add"] ?? "";
                string ui_office_Remark_add = context.Request.Params["ui_office_Remark_add"] ?? "";

                DriveMgr.Model.OfficeModel officeAdd = new Model.OfficeModel();
                officeAdd.OfficeUse = ui_office_OfficeUse_add.Trim();
                officeAdd.TagPerson = ui_office_TagPerson_add.Trim();
                officeAdd.OfficeAmount = decimal.Parse(ui_office_OfficeAmount_add);
                officeAdd.UseDate = DateTime.Parse(ui_office_UseDate_add);
                officeAdd.Remark = ui_office_Remark_add.Trim();

                officeAdd.CreateDate = DateTime.Now;
                officeAdd.CreatePerson = userFromCookie.UserId;
                officeAdd.UpdatePerson = userFromCookie.UserId;
                officeAdd.UpdateDate = DateTime.Now;

                if (officeBll.AddOffice(officeAdd))
                {
                    userOperateLog.OperateInfo = "添加办公费用";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "添加成功，办公用途：" + ui_office_OfficeUse_add.Trim();
                    context.Response.Write("{\"msg\":\"添加成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "添加办公费用";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "添加失败";
                    context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "添加办公费用";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 编辑办公
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void EditOffice(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("office", "edit", userFromCookie.Id))
            {
                int id = Convert.ToInt32(context.Request.Params["id"]);
                DriveMgr.Model.OfficeModel officeEdit = officeBll.GetOfficeModel(id);
                string ui_office_OfficeUse_edit = context.Request.Params["ui_office_OfficeUse_edit"] ?? "";
                string ui_office_TagPerson_edit = context.Request.Params["ui_office_TagPerson_edit"] ?? "";
                string ui_office_OfficeAmount_edit = context.Request.Params["ui_office_OfficeAmount_edit"] ?? "";
                string ui_office_UseDate_edit = context.Request.Params["ui_office_UseDate_edit"] ?? "";
                string ui_office_Remark_edit = context.Request.Params["ui_office_Remark_edit"] ?? "";

                officeEdit.OfficeUse = ui_office_OfficeUse_edit.Trim();
                officeEdit.TagPerson = ui_office_TagPerson_edit.Trim();
                officeEdit.OfficeAmount = decimal.Parse(ui_office_OfficeAmount_edit);
                officeEdit.UseDate = DateTime.Parse(ui_office_UseDate_edit);
                officeEdit.Remark = ui_office_Remark_edit.Trim();

                officeEdit.UpdatePerson = userFromCookie.UserId;
                officeEdit.UpdateDate = DateTime.Now;

                if (officeBll.UpdateOffice(officeEdit))
                {
                    userOperateLog.OperateInfo = "修改办公费用";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "修改成功，办公费用主键：" + officeEdit.Id;
                    context.Response.Write("{\"msg\":\"修改成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "修改办公费用";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "修改失败";
                    context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "修改办公费用";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 删除办公
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void DelOffice(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("office", "delete", userFromCookie.Id))
            {
                string ids = context.Request.Params["id"].Trim(',');
                if (officeBll.DeleteOfficeList(ids))
                {
                    userOperateLog.OperateInfo = "删除办公费用";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "删除成功，办公费用主键：" + ids;
                    context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "删除办公费用";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "删除失败";
                    context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "删除办公费用";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }
    }
}