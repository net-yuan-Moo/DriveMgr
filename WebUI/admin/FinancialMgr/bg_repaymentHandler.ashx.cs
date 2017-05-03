/***********************
 @ author:zlong
 @ Date:2015-01-11
 @ Desc:还款费用界面层
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
    /// bg_repaymentHandler 的摘要说明
    /// </summary>
    public class bg_repaymentHandler : IHttpHandler
    {
        DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
        RepaymentBLL repaymentBll = new RepaymentBLL();
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
                        SearchRepayment(context);
                        break;
                    case "add":
                        AddRepayment(userFromCookie, context);
                        break;
                    case "edit":
                        EditRepayment(userFromCookie, context);
                        break;
                    case "delete":
                        DelRepayment(userFromCookie, context);
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
        /// 查询还款费用
        /// </summary>
        /// <param name="context"></param>
        private void SearchRepayment(HttpContext context)
        {
            string sort = context.Request.Params["sort"];  //排序列
            string order = context.Request.Params["order"];  //排序方式 asc或者desc
            int pageindex = int.Parse(context.Request.Params["page"]);
            int pagesize = int.Parse(context.Request.Params["rows"]);

            string ui_repayment_bank = context.Request.Params["ui_repayment_bank"] ?? "";
            string ui_repayment_repaymentPerson = context.Request.Params["ui_repayment_repaymentPerson"] ?? "";
            string ui_repayment_createStartDate = context.Request.Params["ui_repayment_createStartDate"] ?? "";
            string ui_repayment_createEndDate = context.Request.Params["ui_repayment_createEndDate"] ?? "";

            int totalCount;   //输出参数
            string strJson = repaymentBll.GetPagerData(ui_repayment_bank, ui_repayment_repaymentPerson, ui_repayment_createStartDate, ui_repayment_createEndDate, sort + " " + order, pagesize, pageindex, out totalCount);
            context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

            userOperateLog.OperateInfo = "查询还款费用";
            userOperateLog.IfSuccess = true;
            userOperateLog.Description = "排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 添加还款费用
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void AddRepayment(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("repayment", "add", userFromCookie.Id))
            {
                string ui_repayment_Bank_add = context.Request.Params["ui_repayment_Bank_add"] ?? "";
                string ui_repayment_RepaymentPerson_add = context.Request.Params["ui_repayment_RepaymentPerson_add"] ?? "";
                string ui_repayment_RepaymentAmount_add = context.Request.Params["ui_repayment_RepaymentAmount_add"] ?? "";
                string ui_repayment_RepaymentDate_add = context.Request.Params["ui_repayment_RepaymentDate_add"] ?? "";
                string ui_repayment_Remark_add = context.Request.Params["ui_repayment_Remark_add"] ?? "";

                DriveMgr.Model.RepaymentModel repaymentAdd = new Model.RepaymentModel();
                repaymentAdd.Bank = ui_repayment_Bank_add.Trim();
                repaymentAdd.RepaymentPerson = ui_repayment_RepaymentPerson_add.Trim();
                repaymentAdd.RepaymentAmount = decimal.Parse(ui_repayment_RepaymentAmount_add);
                repaymentAdd.RepaymentDate = DateTime.Parse(ui_repayment_RepaymentDate_add);
                repaymentAdd.Remark = ui_repayment_Remark_add.Trim();

                repaymentAdd.CreateDate = DateTime.Now;
                repaymentAdd.CreatePerson = userFromCookie.UserId;
                repaymentAdd.UpdatePerson = userFromCookie.UserId;
                repaymentAdd.UpdateDate = DateTime.Now;

                if (repaymentBll.AddRepayment(repaymentAdd))
                {
                    userOperateLog.OperateInfo = "添加还款费用";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "添加成功，还款银行：" + ui_repayment_Bank_add.Trim();
                    context.Response.Write("{\"msg\":\"添加成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "添加还款费用";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "添加失败";
                    context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "添加还款费用";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 编辑还款
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void EditRepayment(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("repayment", "edit", userFromCookie.Id))
            {
                int id = Convert.ToInt32(context.Request.Params["id"]);
                DriveMgr.Model.RepaymentModel repaymentEdit = repaymentBll.GetRepaymentModel(id);
                string ui_repayment_Bank_edit = context.Request.Params["ui_repayment_Bank_edit"] ?? "";
                string ui_repayment_RepaymentPerson_edit = context.Request.Params["ui_repayment_RepaymentPerson_edit"] ?? "";
                string ui_repayment_RepaymentAmount_edit = context.Request.Params["ui_repayment_RepaymentAmount_edit"] ?? "";
                string ui_repayment_RepaymentDate_edit = context.Request.Params["ui_repayment_RepaymentDate_edit"] ?? "";
                string ui_repayment_Remark_edit = context.Request.Params["ui_repayment_Remark_edit"] ?? "";

                repaymentEdit.Bank = ui_repayment_Bank_edit.Trim();
                repaymentEdit.RepaymentPerson = ui_repayment_RepaymentPerson_edit.Trim();
                repaymentEdit.RepaymentAmount = decimal.Parse(ui_repayment_RepaymentAmount_edit);
                repaymentEdit.RepaymentDate = DateTime.Parse(ui_repayment_RepaymentDate_edit);
                repaymentEdit.Remark = ui_repayment_Remark_edit.Trim();

                repaymentEdit.UpdatePerson = userFromCookie.UserId;
                repaymentEdit.UpdateDate = DateTime.Now;

                if (repaymentBll.UpdateRepayment(repaymentEdit))
                {
                    userOperateLog.OperateInfo = "修改还款费用";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "修改成功，还款费用主键：" + repaymentEdit.Id;
                    context.Response.Write("{\"msg\":\"修改成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "修改还款费用";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "修改失败";
                    context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "修改还款费用";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 删除还款
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void DelRepayment(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("repayment", "delete", userFromCookie.Id))
            {
                string ids = context.Request.Params["id"].Trim(',');
                if (repaymentBll.DeleteRepaymentList(ids))
                {
                    userOperateLog.OperateInfo = "删除还款费用";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "删除成功，还款费用主键：" + ids;
                    context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "删除还款费用";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "删除失败";
                    context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "删除还款费用";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }
    }
}