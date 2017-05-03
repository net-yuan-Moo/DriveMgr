using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace DriveMgr.WebUI.admin.StudentsMgr
{
    /// <summary>
    /// bg_peroid_setup_Handler 的摘要说明
    /// </summary>
    public class bg_peroid_setup_Handler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request.Params["action"];
            DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
            try
            {
                DriveMgr.Model.User userFromCookie = DriveMgr.Common.UserHelper.GetUser(context);   //获取cookie里的用户对象
                userOperateLog = new Model.UserOperateLog();
                userOperateLog.UserIp = context.Request.UserHostAddress;
                userOperateLog.UserName = userFromCookie.UserId;
                switch (action)
                {
                    case "search":
                        string strWhere = "1=1";
                        string sort = context.Request.Params["sort"];  //排序列
                        string order = context.Request.Params["order"];  //排序方式 asc或者desc
                        int pageindex = int.Parse(context.Request.Params["page"]);
                        int pagesize = int.Parse(context.Request.Params["rows"]);


                        string ui_peroid_setup_code = context.Request.Params["ui_peroid_setup_code"] ?? "";
                       
                        strWhere += " and flag=1";
                        if (ui_peroid_setup_code.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_peroid_setup_code))   //防止sql注入
                            strWhere += string.Format(" and PeriodCode like '%{0}%'", ui_peroid_setup_code.Trim());
                        

                        int totalCount;   //输出参数
                        string strJson = new DriveMgr.BLL.PeriodsBLL().GetPager("V_PeroidsAndCurrent", "*", sort + " " + order, pagesize, pageindex, strWhere, out totalCount);
                        context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

                        userOperateLog.OperateInfo = "查询期数";
                        userOperateLog.IfSuccess = true;
                        userOperateLog.Description = "查询条件：" + strWhere + " 排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "add":
                        if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("peroid_setup", "add", userFromCookie.Id))
                        {
                            string ui_peroid_setup_code_add = context.Request.Params["ui_peroid_setup_code_add"] ?? "";
                            string ui_peroid_setup_remark_add = context.Request.Params["ui_peroid_setup_remark_add"] ?? "";

                            DriveMgr.Model.PeriodsModel periodsAdd = new Model.PeriodsModel();
                            periodsAdd.PeriodCode = ui_peroid_setup_code_add;
                            periodsAdd.Remark = ui_peroid_setup_remark_add;

                            bool result = new DriveMgr.BLL.PeriodsBLL().Add(periodsAdd);
                            if (result)
                            {
                                userOperateLog.OperateInfo = "添加期数";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "添加期数，期数" + ui_peroid_setup_code_add;
                                context.Response.Write("{\"msg\":\"添加期数成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "添加期数";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "添加失败";
                                context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "添加期数";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "edit":
                        if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("peroid_setup", "edit", userFromCookie.Id))
                        {
                            int id = Convert.ToInt32(context.Request.Params["id_peroid_setup"]);
                            string ui_peroid_setup_code_edit = context.Request.Params["ui_peroid_setup_code_edit"] ?? "";
                            string ui_peroid_setup_remark_edit = context.Request.Params["ui_peroid_setup_remark_edit"] ?? "";

                            DriveMgr.Model.PeriodsModel periodsEdit = new Model.PeriodsModel();
                            periodsEdit.PeriodCode = ui_peroid_setup_code_edit;
                            periodsEdit.Remark = ui_peroid_setup_remark_edit;
                            periodsEdit.ID = id;

                            if (new DriveMgr.BLL.PeriodsBLL().Update(periodsEdit))
                            {
                                userOperateLog.OperateInfo = "修改期数";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "修改成功，期数主键：" + periodsEdit.ID;
                                context.Response.Write("{\"msg\":\"修改成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "修改期数";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "修改失败";
                                context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "修改期数";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "delete":
                        if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("peroid_setup", "delete", userFromCookie.Id))
                        {
                            string ids = context.Request.Params["id"].Trim(',');
                            if (new DriveMgr.BLL.PeriodsBLL().DeleteList(ids))
                            {
                                userOperateLog.OperateInfo = "删除期数";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "删除成功，期数主键：" + ids;
                                context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "删除期数";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "删除失败";
                                context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "删除期数";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "setupnew":
                        if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("peroid_setup", "setupnew", userFromCookie.Id))
                        {
                            int id = Convert.ToInt32(context.Request.Params["currentid_peroid_setup"]);
                            if (new DriveMgr.BLL.PeriodsBLL().SetupToCurrent(id))
                            {
                                userOperateLog.OperateInfo = "设置期数为当前期数";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "设置成功，期数主键：" + id;
                                context.Response.Write("{\"msg\":\"设置成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "设置期数为当前期数";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "设置失败";
                                context.Response.Write("{\"msg\":\"设置失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "设置期数为当前期数";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    default:
                        context.Response.Write("{\"msg\":\"参数错误！\",\"success\":false}");
                        break;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"msg\":\"" + DriveMgr.Common.JsonHelper.StringFilter(ex.Message) + "\",\"success\":false}");
                userOperateLog.OperateInfo = "期数功能异常";
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