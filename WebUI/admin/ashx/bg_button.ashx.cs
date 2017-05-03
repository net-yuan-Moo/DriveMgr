using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using DriveMgr.Common;

namespace DriveMgr.WebUI.admin.ashx
{
    /// <summary>
    /// 按钮表操作
    /// </summary>
    public class bg_button : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request.Params["action"];
            DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
            try
            {
                DriveMgr.Model.User user = DriveMgr.Common.UserHelper.GetUser(context);   //获取cookie里的用户对象
                userOperateLog = new Model.UserOperateLog();
                userOperateLog.UserIp = context.Request.UserHostAddress;
                userOperateLog.UserName = user.UserId;

                switch (action)
                {
                    case "getbutton":   //根据用户的权限获取用户点击的菜单有权限的按钮
                        string pageName = context.Request.Params["pagename"];
                        string menuCode = context.Request.Params["menucode"];   //菜单标识码
                        DataTable dt = new DriveMgr.BLL.Button().GetButtonByMenuCodeAndUserId(menuCode, user.Id);
                        string result = DriveMgr.Common.ToolbarHelper.GetToolBar(dt, pageName);
                        context.Response.Write(result);
                        break;
                    case "getAllButton":
                        string allresult = new DriveMgr.BLL.Button().GetAllButton();
                        context.Response.Write(allresult);
                        break;
                    case "getButtonByMenu":
                        int menuId = Int32.Parse(context.Request.Params["menuId"]);
                        string byMenuresult = new DriveMgr.BLL.Button().GetButtonByMenu(menuId).ToJson();
                        context.Response.Write(byMenuresult);
                        break;
                    case "search":
                        string strWhere = "1=1";
                        string sort = context.Request.Params["sort"];  //排序列
                        string order = context.Request.Params["order"];  //排序方式 asc或者desc
                        int pageindex = int.Parse(context.Request.Params["page"]);
                        int pagesize = int.Parse(context.Request.Params["rows"]);

                        int totalCount;   //输出参数
                        string strJson = new DriveMgr.BLL.Button().GetPager("tbButton", "Id,Name,Code,Icon,Sort,AddDate,Description", sort + " " + order, pagesize, pageindex, strWhere, out totalCount);
                        context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");
                        userOperateLog.OperateInfo = "查询按钮";
                        userOperateLog.IfSuccess = true;
                        userOperateLog.Description = "查询条件：" + strWhere + " 排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "add":
                        if (user != null && new DriveMgr.BLL.Authority().IfAuthority("button", "add", user.Id))
                        {
                            string ui_button_buttonname_add = context.Request.Params["ui_button_buttonname_add"] ?? "";
                            string ui_button_codename_add = context.Request.Params["ui_button_codename_add"] ?? "";
                            string ui_button_iconname_add = context.Request.Params["ui_button_iconname_add"] ?? "";
                            int ui_button_sortname_add = Int32.Parse(context.Request.Params["ui_button_sortname_add"]);
                            string ui_button_description_add = context.Request.Params["ui_button_description_add"] ?? "";

                            DriveMgr.Model.Button buttonAdd = new Model.Button();
                            buttonAdd.Name = ui_button_buttonname_add.Trim();
                            buttonAdd.Code = ui_button_codename_add.Trim();
                            buttonAdd.Icon = ui_button_iconname_add.Trim();
                            buttonAdd.Sort = ui_button_sortname_add;
                            buttonAdd.Description = ui_button_description_add.Trim();
                            buttonAdd.AddDate = DateTime.Now;

                            bool buttonresult = new DriveMgr.BLL.Button().Add(buttonAdd);
                            if (buttonresult)
                            {
                                userOperateLog.OperateInfo = "添加按钮";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "添加成功";
                                context.Response.Write("{\"msg\":\"添加成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "添加按钮";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "添加失败";
                                context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "添加按钮";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "edit":
                        if (user != null && new DriveMgr.BLL.Authority().IfAuthority("button", "edit", user.Id))
                        {
                            int id = Convert.ToInt32(context.Request.Params["id"]);
                            string ui_button_buttonname_edit = context.Request.Params["ui_button_buttonname_edit"] ?? "";
                            string ui_button_codename_edit = context.Request.Params["ui_button_codename_edit"] ?? "";
                            string ui_button_iconname_edit = context.Request.Params["ui_button_iconname_edit"] ?? "";
                            int ui_button_sortnname_edit = Int32.Parse(context.Request.Params["ui_button_sortnname_edit"]);
                            string ui_button_description_edit = context.Request.Params["ui_button_description_edit"] ?? "";

                            DriveMgr.Model.Button buttonEdit = new Model.Button();
                            buttonEdit.Id = id;
                            buttonEdit.Name = ui_button_buttonname_edit.Trim();
                            buttonEdit.Code = ui_button_codename_edit.Trim();
                            buttonEdit.Icon = ui_button_iconname_edit.Trim();
                            buttonEdit.Sort = ui_button_sortnname_edit;
                            buttonEdit.Description = ui_button_description_edit.Trim();
                            buttonEdit.AddDate = DateTime.Now;

                            if (new DriveMgr.BLL.Button().Update(buttonEdit))
                            {
                                userOperateLog.OperateInfo = "修改按钮";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "修改成功，按钮主键：" + buttonEdit.Id;
                                context.Response.Write("{\"msg\":\"修改成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "修改按钮";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "修改失败";
                                context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "修改按钮";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "delete":
                        if (user != null && new DriveMgr.BLL.Authority().IfAuthority("button", "delete", user.Id))
                        {
                            string ids = context.Request.Params["id"].Trim(',');
                            if (new DriveMgr.BLL.Button().DeleteList(ids))
                            {
                                userOperateLog.OperateInfo = "删除按钮";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "删除成功，按钮主键：" + ids;
                                context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "删除按钮";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "删除失败";
                                context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "删除按钮";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    default:
                        context.Response.Write("{\"result\":\"参数错误！\",\"success\":false}");
                        break;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"msg\":\"" + DriveMgr.Common.JsonHelper.StringFilter(ex.Message) + "\",\"success\":false}");
                userOperateLog.OperateInfo = "按钮功能异常";
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
    }
}