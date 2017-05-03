using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DriveMgr.WebUI.admin.StudentsDynamic
{
    /// <summary>
    /// bg_examScoresHandler 的摘要说明
    /// </summary>
    public class bg_examScoresHandler : IHttpHandler
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

                        string ui_examScores_stuname = context.Request.Params["ui_examScores_stuname"] ?? "";
                        string ui_examScores_code = context.Request.Params["ui_examScores_code"] ?? "";
                        string ui_examScores_subjectname = context.Request.Params["ui_examScores_subjectname"] ?? "";
                        string ui_examScores_examStatus = context.Request.Params["ui_examScores_examStatus"] ?? "";

                        strWhere += " and flag=1 and PeriodsID = (SELECT TOP 1 CurrentPeroidID FROM tb_CurrentPeroid)";
                        if (ui_examScores_stuname.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_examScores_stuname))   //防止sql注入
                            strWhere += string.Format(" and StudentsName like '%{0}%'", ui_examScores_stuname.Trim());
                        if (ui_examScores_code.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_examScores_code))
                            strWhere += string.Format(" and StudentCode like '%{0}%'", ui_examScores_code.Trim());
                       

                        if (ui_examScores_subjectname.Trim() != "" && ui_examScores_examStatus!="")
                        {
                            int ui_examScores_subjectid = Int32.Parse(ui_examScores_subjectname);
                            int ui_examScores_examStatusId = Int32.Parse(ui_examScores_examStatus);
                            switch (ui_examScores_subjectid)
                            {
                                case 1:
                                    strWhere += string.Format(" and OneStatus ={0}", ui_examScores_examStatusId);
                                    break;
                                case 2:
                                    strWhere += string.Format(" and TwoStatus ={0}", ui_examScores_examStatusId);
                                    break;
                                case 3:
                                    strWhere += string.Format(" and ThreeStatus ={0}", ui_examScores_examStatusId);
                                    break;
                                case 4:
                                    strWhere += string.Format(" and FourStatus ={0}", ui_examScores_examStatusId);
                                    break;
                                default:
                                    break;
                            }
                        }

                        int totalCount;   //输出参数
                        string strJson = new DriveMgr.BLL.ScoresBLL().GetPager("V_ExamScores", "*", sort + " " + order, pagesize, pageindex, strWhere, out totalCount);
                        context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

                        userOperateLog.OperateInfo = "查询成绩";
                        userOperateLog.IfSuccess = true;
                        userOperateLog.Description = "查询条件：" + strWhere + " 排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "edit":
                        if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("examScores", "edit", userFromCookie.Id))
                        {
                            int id = Convert.ToInt32(context.Request.Params["examScoresid_edit"]);
                            decimal ui_examScores_oneScore_edit = Decimal.Parse(context.Request.Params["ui_examScores_oneScore_edit"] ?? "0");
                            int ui_examScores_oneStatus_edit = Int32.Parse(context.Request.Params["ui_examScores_oneStatus_edit"] ?? "-1");
                            decimal ui_examScores_twoScore_edit = Decimal.Parse(context.Request.Params["ui_examScores_twoScore_edit"] ?? "0");
                            int ui_examScores_twoStatus_edit = Int32.Parse(context.Request.Params["ui_examScores_twoStatus_edit"] ?? "-1");
                            decimal ui_examScores_threeScore_edit = Decimal.Parse(context.Request.Params["ui_examScores_threeScore_edit"] ?? "0");
                            int ui_examScores_threeStatus_edit = Int32.Parse(context.Request.Params["ui_examScores_threeStatus_edit"] ?? "-1");
                            decimal ui_examScores_fourScore_edit = Decimal.Parse(context.Request.Params["ui_examScores_fourScore_edit"] ?? "0");
                            int ui_examScores_fourStatus_edit = Int32.Parse(context.Request.Params["ui_examScores_fourStatus_edit"] ?? "-1");
                            string ui_examScores_remark_edit = context.Request.Params["ui_examScores_remark_edit"] ?? "";

                            DriveMgr.Model.ScoresModel scoresEdit = new Model.ScoresModel();
                            scoresEdit.ID = id;
                            scoresEdit.ScoreOne = ui_examScores_oneScore_edit;
                            scoresEdit.OneStatus = ui_examScores_oneStatus_edit;

                            scoresEdit.ScoreTwo = ui_examScores_twoScore_edit;
                            scoresEdit.TwoStatus = ui_examScores_twoStatus_edit;

                            scoresEdit.SocreThree = ui_examScores_threeScore_edit;
                            scoresEdit.ThreeStatus = ui_examScores_threeStatus_edit;

                            scoresEdit.ScoreFour = ui_examScores_fourScore_edit;
                            scoresEdit.FourStatus = ui_examScores_fourStatus_edit;
                            scoresEdit.Remark = ui_examScores_remark_edit;


                            if (new DriveMgr.BLL.ScoresBLL().Update(scoresEdit))
                            {
                                userOperateLog.OperateInfo = "修改成绩信息";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "修改成功，成绩主键：" + scoresEdit.ID;
                                context.Response.Write("{\"msg\":\"修改成绩信息成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "修改成绩信息";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "修改失败";
                                context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "修改成绩信息";
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
                userOperateLog.OperateInfo = "成绩功能异常";
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