using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DriveMgr.WebUI.admin.InfoMgr
{
    /// <summary>
    /// bg_distributeStudentsHandler 的摘要说明
    /// </summary>
    public class bg_distributeStudentsHandler : IHttpHandler
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

                        string ui_distributeStudents_name = context.Request.Params["ui_distributeStudents_name"] ?? "";
                        string ui_distributeStudents_code = context.Request.Params["ui_distributeStudents_code"] ?? "";
                        string ui_distributeStudents_coach = context.Request.Params["ui_distributeStudents_coach"] ?? "";


                        strWhere += " and flag=1 and PeriodsID = (SELECT TOP 1 CurrentPeroidID FROM tb_CurrentPeroid)";
                        if (ui_distributeStudents_name.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_distributeStudents_name))   //防止sql注入
                            strWhere += string.Format(" and StudentsName like '%{0}%'", ui_distributeStudents_name.Trim());
                        if (ui_distributeStudents_code.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_distributeStudents_code))
                            strWhere += string.Format(" and StudentCode like '%{0}%'", ui_distributeStudents_code.Trim());
                        if (ui_distributeStudents_coach.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_distributeStudents_coach))
                            strWhere += string.Format(" and CoachID ={0}", ui_distributeStudents_coach.Trim());



                        int totalCount;   //输出参数
                        string strJson = new DriveMgr.BLL.ScoresBLL().GetPager("V_DistributeStudents", "*", sort + " " + order, pagesize, pageindex, strWhere, out totalCount);
                        context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

                        userOperateLog.OperateInfo = "查询分配学员";
                        userOperateLog.IfSuccess = true;
                        userOperateLog.Description = "查询条件：" + strWhere + " 排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "autoDistributeStu": //分配学员
                        if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("distributeStudents", "autoDistributeStu", userFromCookie.Id))
                        {
                            string distributeResult = new DriveMgr.BLL.CoachBLL().AutoDistributeStudents(userFromCookie.UserId);
                            userOperateLog.OperateInfo = "自动分配学员";
                            userOperateLog.IfSuccess = true;
                            userOperateLog.Description = distributeResult;

                            context.Response.Write("{\"msg\":\""+distributeResult+"\",\"success\":true}");
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "自动分配学员";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "edit":
                        if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("distributeStudents", "edit", userFromCookie.Id))
                        {
                            int id = Convert.ToInt32(context.Request.Params["ui_distributeStudentsid_edit"]);
                            int ui_distributeStudents_coach_edit = Int32.Parse(context.Request.Params["ui_distributeStudents_coach_edit"] ?? "0");


                            DriveMgr.Model.DistributeStudentsModel distributeStudentsEdit = new Model.DistributeStudentsModel();
                            distributeStudentsEdit.ID = id;
                            distributeStudentsEdit.CoachID = ui_distributeStudents_coach_edit;
                            distributeStudentsEdit.CreateTime = DateTime.Now;
                            distributeStudentsEdit.Operater = userFromCookie.UserName;

                            if (new DriveMgr.BLL.CoachBLL().EditDistributeStudents(distributeStudentsEdit))
                            {
                                userOperateLog.OperateInfo = "修改分配学员信息";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "修改成功，分配学员主键：" + distributeStudentsEdit.ID;
                                context.Response.Write("{\"msg\":\"修改分配学员信息成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "修改分配学员信息";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "修改失败";
                                context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "修改分配学员信息";
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
                userOperateLog.OperateInfo = "分配学员功能异常";
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