using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DriveMgr.WebUI.admin.StudentsDynamic
{
    /// <summary>
    /// bg_preAppointmentHandler 的摘要说明
    /// </summary>
    public class bg_preAppointmentHandler : IHttpHandler
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

                        string ui_preAppointment_name = context.Request.Params["ui_preAppointment_name"] ?? "";
                        string ui_preAppointment_code = context.Request.Params["ui_preAppointment_code"] ?? "";
                        string ui_preAppointment_appstatus = context.Request.Params["ui_preAppointment_appstatus"] ?? "";

                        string subjectId = context.Request.Params["subjectId"] ?? "";

                        string ui_preAppointment_AppStartTime = context.Request.Params["ui_preAppointment_AppStartTime"] ?? "";
                        string ui_preAppointment_AppEndTime = context.Request.Params["ui_preAppointment_AppEndTime"] ?? "";

                        strWhere += " and flag=1 and PeriodsID = (SELECT TOP 1 CurrentPeroidID FROM tb_CurrentPeroid)";
                        if (subjectId.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(subjectId))   //防止sql注入
                            strWhere += string.Format(" and SubjectID = '{0}'", subjectId.Trim());
                        if (ui_preAppointment_AppStartTime.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_preAppointment_AppStartTime))   //防止sql注入
                            strWhere += string.Format(" and AppointDate >= '{0}'", ui_preAppointment_AppStartTime.Trim());
                        if (ui_preAppointment_AppEndTime.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_preAppointment_AppEndTime))   //防止sql注入
                            strWhere += string.Format(" and AppointDate <= '{0}'", ui_preAppointment_AppEndTime.Trim());

                        if (ui_preAppointment_appstatus.Trim() != "select" && ui_preAppointment_appstatus.Trim() != "")
                            strWhere += " and AppointmentStatus = '" + ui_preAppointment_appstatus.Trim() + "'";

                        if (ui_preAppointment_name.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_preAppointment_name))   //防止sql注入
                            strWhere += string.Format(" and StudentsName like '%{0}%'", ui_preAppointment_name.Trim());
                        if (ui_preAppointment_code.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_preAppointment_code))
                            strWhere += string.Format(" and StudentCode like '%{0}%'", ui_preAppointment_code.Trim());
                       

                        int totalCount;   //输出参数
                        string strJson = new DriveMgr.BLL.AppointmentBLL().GetPager("V_PreAppointment", "*", sort + " " + order, pagesize, pageindex, strWhere, out totalCount);
                        context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

                        userOperateLog.OperateInfo = "查询预约";
                        userOperateLog.IfSuccess = true;
                        userOperateLog.Description = "查询条件：" + strWhere + " 排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "appointment":  //预约考试
                        if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("preAppointment", "preAppointment", userFromCookie.Id))
                        {
                            int id = Convert.ToInt32(context.Request.Params["preAppointmentid_edit"]);
                            DateTime ui_preAppointment_appDate_edit = DateTime.Parse(context.Request.Params["ui_preAppointment_appDate_edit"] ?? "");
                            int editsubjectId = Int32.Parse(context.Request.Params["subjectId"]);
                            string ui_preAppointment_remark_edit = context.Request.Params["ui_preAppointment_remark_edit"] ?? "";

                            DriveMgr.Model.AppointmentModel appointmentEdit = new Model.AppointmentModel();
                            appointmentEdit.ID = id;
                            appointmentEdit.AppointDate = ui_preAppointment_appDate_edit;
                            appointmentEdit.Operater = userFromCookie.UserName;
                            appointmentEdit.AppointStatus = 1;
                            appointmentEdit.SubjectID = editsubjectId;
                            appointmentEdit.Remark = ui_preAppointment_remark_edit;


                            if (new DriveMgr.BLL.AppointmentBLL().Update(appointmentEdit))
                            {
                                userOperateLog.OperateInfo = "预约考试信息";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "预约成功，预约主键：" + appointmentEdit.ID+"；预约科目："+appointmentEdit.SubjectID;
                                context.Response.Write("{\"msg\":\"预约信息成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "预约考试信息";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "预约失败";
                                context.Response.Write("{\"msg\":\"预约失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "预约考试信息";
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
                userOperateLog.OperateInfo = "预约考试功能异常";
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