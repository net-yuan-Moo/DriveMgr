using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DriveMgr.WebUI.admin.StudentsMgr
{
    /// <summary>
    /// bg_archivesHandler 的摘要说明
    /// </summary>
    public class bg_archivesHandler : IHttpHandler
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

                        string ui_studentsArchives_name = context.Request.Params["ui_studentsArchives_name"] ?? "";
                        string ui_studentsArchives_code = context.Request.Params["ui_studentsArchives_code"] ?? "";
                        string ui_studentsArchives_cardnum = context.Request.Params["ui_studentsArchives_cardnum"] ?? "";
                        string ui_studentsArchives_archivescode = context.Request.Params["ui_studentsArchives_archivescode"] ?? "";

                        strWhere += " and flag=1";
                        if (ui_studentsArchives_name.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_studentsArchives_name))   //防止sql注入
                            strWhere += string.Format(" and StudentsName like '%{0}%'", ui_studentsArchives_name.Trim());
                        if (ui_studentsArchives_code.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_studentsArchives_code))
                            strWhere += string.Format(" and StudentCode like '%{0}%'", ui_studentsArchives_code.Trim());
                        if (ui_studentsArchives_cardnum.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_studentsArchives_cardnum))
                            strWhere += string.Format(" and CardNum like '%{0}%'", ui_studentsArchives_cardnum.Trim());
                        if (ui_studentsArchives_archivescode.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_studentsArchives_archivescode))
                            strWhere += string.Format(" and ArchivesCode like '%{0}%'", ui_studentsArchives_archivescode.Trim());
                       

                        int totalCount;   //输出参数
                        string strJson = new DriveMgr.BLL.ArchivesBLL().GetPager("V_Archives", "*", sort + " " + order, pagesize, pageindex, strWhere, out totalCount);
                        context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

                        userOperateLog.OperateInfo = "查询档案";
                        userOperateLog.IfSuccess = true;
                        userOperateLog.Description = "查询条件：" + strWhere + " 排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "edit":
                        if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("archives", "edit", userFromCookie.Id))
                        {
                            int id = Convert.ToInt32(context.Request.Params["archivesid_edit"]);
                            string ui_archives_remark_edit = context.Request.Params["ui_archives_remark_edit"] ?? "";

                            DriveMgr.Model.ArchivesModel archivesEdit = new Model.ArchivesModel();
                            archivesEdit.ID = id;
                            archivesEdit.Remark = ui_archives_remark_edit.Trim();
                           

                            if (new DriveMgr.BLL.ArchivesBLL().Update(archivesEdit))
                            {
                                userOperateLog.OperateInfo = "修改档案信息";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "修改成功，档案主键：" + archivesEdit.ID;
                                context.Response.Write("{\"msg\":\"修改档案信息成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "修改档案信息";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "修改失败";
                                context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "修改档案信息";
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
                userOperateLog.OperateInfo = "档案功能异常";
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