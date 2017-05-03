/***********************
 @ author:zlong
 @ Date:2015-01-11
 @ Desc:学费界面层
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
    /// bg_tuitionHandler 的摘要说明
    /// </summary>
    public class bg_tuitionHandler : IHttpHandler
    {
        DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
        TuitionBLL tuitionBll = new TuitionBLL();
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
                        SearchTuition(context);
                        break;
                    case "add":
                        AddTuition(userFromCookie, context);
                        break;
                    case "edit":
                        EditTuition(userFromCookie, context);
                        break;
                    case "delete":
                        DelTuition(userFromCookie, context);
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
        /// 查询学费
        /// </summary>
        /// <param name="context"></param>
        private void SearchTuition(HttpContext context)
        {
            string sort = context.Request.Params["sort"];  //排序列
            string order = context.Request.Params["order"];  //排序方式 asc或者desc
            int pageindex = int.Parse(context.Request.Params["page"]);
            int pagesize = int.Parse(context.Request.Params["rows"]);

            int totalCount;   //输出参数
            string strJson = tuitionBll.GetPagerData(sort + " " + order, pagesize, pageindex, out totalCount);
            context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

            userOperateLog.OperateInfo = "查询学费";
            userOperateLog.IfSuccess = true;
            userOperateLog.Description = "排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 添加学费
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void AddTuition(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("tuition", "add", userFromCookie.Id))
            {
                string ui_tuition_LocalType_add = context.Request.Params["ui_tuition_LocalType_add"] ?? "";
                string ui_tuition_Costs_add = context.Request.Params["ui_tuition_Costs_add"] ?? "";
                string ui_tuition_Remark_add = context.Request.Params["ui_tuition_Remark_add"] ?? "";

                DriveMgr.Model.TuitionModel tuitionAdd = new Model.TuitionModel();
                tuitionAdd.Costs = decimal.Parse(ui_tuition_Costs_add);
                tuitionAdd.LocalType = Int32.Parse(ui_tuition_LocalType_add);
                tuitionAdd.Remark = ui_tuition_Remark_add.Trim();

                tuitionAdd.CreateDate = DateTime.Now;
                tuitionAdd.CreatePerson = userFromCookie.UserId;
                tuitionAdd.UpdatePerson = userFromCookie.UserId;
                tuitionAdd.UpdateDate = DateTime.Now;

                if (!tuitionBll.IsExistTuition(tuitionAdd.LocalType.Value))
                {
                    if (tuitionBll.AddTuition(tuitionAdd))
                    {
                        userOperateLog.OperateInfo = "添加学费";
                        userOperateLog.IfSuccess = true;
                        userOperateLog.Description = "添加成功，学费：" + ui_tuition_Costs_add.Trim();
                        context.Response.Write("{\"msg\":\"添加成功！\",\"success\":true}");
                    }
                    else
                    {
                        userOperateLog.OperateInfo = "添加学费";
                        userOperateLog.IfSuccess = false;
                        userOperateLog.Description = "添加失败";
                        context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                    }
                }
                else
                {
                    context.Response.Write("{\"msg\":\"该信息已经存在！\",\"success\":true}");
                }
                
            }
            else
            {
                userOperateLog.OperateInfo = "添加学费";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 编辑学费
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void EditTuition(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("tuition", "edit", userFromCookie.Id))
            {
                int id = Convert.ToInt32(context.Request.Params["id"]);
                DriveMgr.Model.TuitionModel tuitionEdit = tuitionBll.GetTuitionModel(id);
                string ui_tuition_Costs_edit = context.Request.Params["ui_tuition_Costs_edit"] ?? "";
                string ui_tuition_Remark_edit = context.Request.Params["ui_tuition_Remark_edit"] ?? "";

                tuitionEdit.Costs = decimal.Parse(ui_tuition_Costs_edit);
                tuitionEdit.Remark = ui_tuition_Remark_edit.Trim();

                tuitionEdit.UpdatePerson = userFromCookie.UserId;
                tuitionEdit.UpdateDate = DateTime.Now;

                if (tuitionBll.UpdateTuition(tuitionEdit))
                {
                    userOperateLog.OperateInfo = "修改学费";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "修改成功，学费主键：" + tuitionEdit.Id;
                    context.Response.Write("{\"msg\":\"修改成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "修改学费";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "修改失败";
                    context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "修改学费";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 删除学费
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void DelTuition(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("tuition", "delete", userFromCookie.Id))
            {
                string ids = context.Request.Params["id"].Trim(',');
                if (tuitionBll.DeleteTuitionList(ids))
                {
                    userOperateLog.OperateInfo = "删除学费";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "删除成功，学费主键：" + ids;
                    context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "删除学费";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "删除失败";
                    context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "删除学费";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }
    }
}