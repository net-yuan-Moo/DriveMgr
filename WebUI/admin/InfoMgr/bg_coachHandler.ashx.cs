using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DriveMgr.WebUI.admin.InfoMgr
{
    /// <summary>
    /// bg_coachHandler 的摘要说明
    /// </summary>
    public class bg_coachHandler : IHttpHandler
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

                        string ui_coach_name = context.Request.Params["ui_coach_name"] ?? "";
                        string ui_coach_address = context.Request.Params["ui_coach_address"] ?? "";
                        string ui_coach_phonenum = context.Request.Params["ui_coach_phonenum"] ?? "";

                        strWhere += " and CoachStatus = 1";
                        if (ui_coach_name.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_coach_name))   //防止sql注入
                            strWhere += string.Format(" and CoachName like '%{0}%'", ui_coach_name.Trim());
                        if (ui_coach_address.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_coach_address))
                            strWhere += string.Format(" and Address like '%{0}%'", ui_coach_address.Trim());
                        if (ui_coach_phonenum.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_coach_phonenum))
                            strWhere += string.Format(" and CardNum like '%{0}%'", ui_coach_phonenum.Trim());
                      
                        int totalCount;   //输出参数
                        string strJson = new DriveMgr.BLL.CoachBLL().GetPager("tb_Coach", "*", sort + " " + order, pagesize, pageindex, strWhere, out totalCount);
                        context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

                        userOperateLog.OperateInfo = "查询教练信息";
                        userOperateLog.IfSuccess = true;
                        userOperateLog.Description = "查询条件：" + strWhere + " 排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "add":
                        if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("coach", "add", userFromCookie.Id))
                        {
                            string ui_coach_name_add = context.Request.Params["ui_coach_name_add"] ?? "";
                            string ui_coach_cardnum_add = context.Request.Params["ui_coach_cardnum_add"] ?? "";
                            int ui_coach_age_add = Int32.Parse(context.Request.Params["ui_coach_age_add"] ?? "-1");
                            bool ui_coach_sex_add = context.Request.Params["ui_coach_sex_add"] == "0" ? false : true;
                            string ui_coach_phonenum_add = context.Request.Params["ui_coach_phonenum_add"] ?? "";
                            string ui_coach_address_add = context.Request.Params["ui_coach_address_add"] ?? "";

                            DriveMgr.Model.CoachModel coachAdd = new Model.CoachModel();
                            coachAdd.CoachName = ui_coach_name_add.Trim();
                            coachAdd.CardNum = ui_coach_cardnum_add.Trim();
                            coachAdd.Age = ui_coach_age_add;
                            coachAdd.Sex = ui_coach_sex_add;
                            coachAdd.Phone = ui_coach_phonenum_add.Trim();
                            coachAdd.Address = ui_coach_address_add.Trim();


                            bool coachResult = new DriveMgr.BLL.CoachBLL().Add(coachAdd);
                            if (coachResult)
                            {
                                userOperateLog.OperateInfo = "添加教练";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "添加成功，教练名称：" + coachAdd.CoachName;
                                context.Response.Write("{\"msg\":\"添加成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "添加教练";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "添加失败";
                                context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "添加教练";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "edit":
                        if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("coach", "edit", userFromCookie.Id))
                        {
                            int id = Convert.ToInt32(context.Request.Params["coachid_edit"]);
                            string ui_coach_name_edit = context.Request.Params["ui_coach_name_edit"] ?? "";
                            string ui_coach_cardnum_edit = context.Request.Params["ui_coach_cardnum_edit"] ?? "";
                            int ui_coach_age_edit = Int32.Parse(context.Request.Params["ui_coach_age_edit"] ?? "-1");
                            bool ui_coach_sex_edit = context.Request.Params["ui_coach_sex_edit"] == "0" ? false : true;
                            string ui_coach_phonenum_edit = context.Request.Params["ui_coach_phonenum_edit"] ?? "";
                            string ui_coach_address_edit = context.Request.Params["ui_coach_address_edit"] ?? "";

                            DriveMgr.Model.CoachModel coachEdit = new Model.CoachModel();
                            coachEdit.ID = id;
                            coachEdit.CoachName = ui_coach_name_edit.Trim();
                            coachEdit.CardNum = ui_coach_cardnum_edit.Trim();
                            coachEdit.Age = ui_coach_age_edit;
                            coachEdit.Sex = ui_coach_sex_edit;
                            coachEdit.Phone = ui_coach_phonenum_edit.Trim();
                            coachEdit.Address = ui_coach_address_edit.Trim();

                            if (new DriveMgr.BLL.CoachBLL().Update(coachEdit))
                            {
                                userOperateLog.OperateInfo = "修改教练";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "修改成功，教练主键：" + coachEdit.ID;
                                context.Response.Write("{\"msg\":\"修改成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "修改教练";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "修改失败";
                                context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "修改教练";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "delete":
                        if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("coach", "delete", userFromCookie.Id))
                        {
                            string ids = context.Request.Params["coachid_delete"].Trim(',');
                            if (new DriveMgr.BLL.CoachBLL().DeleteList(ids))
                            {
                                userOperateLog.OperateInfo = "删除教练";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "删除成功，教练主键：" + ids;
                                context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "删除教练";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "删除失败";
                                context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "删除教练";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "getAllCoach":
                        string strAllWhere = "1=1 and CoachStatus = 1";
                        DataTable dt = new DriveMgr.BLL.CoachBLL().GetList(strAllWhere);
                        string strAllJson =  DriveMgr.Common.JsonHelper.ToJson(dt);
                        
                        context.Response.Write(strAllJson);

                        userOperateLog.OperateInfo = "查询教练信息";
                        userOperateLog.IfSuccess = true;
                        userOperateLog.Description = "查询所有教练信息：";
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
                userOperateLog.OperateInfo = "教练功能异常";
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