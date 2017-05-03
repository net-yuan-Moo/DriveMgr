/***********************
 @ author:zlong
 @ Date:2015-01-11
 @ Desc:贷款费用界面层
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
    /// bg_loanHandler 的摘要说明
    /// </summary>
    public class bg_loanHandler : IHttpHandler
    {
        DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
        LoanBLL loanBll = new LoanBLL();
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
                        SearchLoan(context);
                        break;
                    case "add":
                        AddLoan(userFromCookie, context);
                        break;
                    case "edit":
                        EditLoan(userFromCookie, context);
                        break;
                    case "delete":
                        DelLoan(userFromCookie, context);
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
        /// 查询贷款费用
        /// </summary>
        /// <param name="context"></param>
        private void SearchLoan(HttpContext context)
        {
            string sort = context.Request.Params["sort"];  //排序列
            string order = context.Request.Params["order"];  //排序方式 asc或者desc
            int pageindex = int.Parse(context.Request.Params["page"]);
            int pagesize = int.Parse(context.Request.Params["rows"]);

            string ui_loan_bank = context.Request.Params["ui_loan_bank"] ?? "";
            string ui_loan_lenders = context.Request.Params["ui_loan_lenders"] ?? "";
            string ui_loan_createStartDate = context.Request.Params["ui_loan_createStartDate"] ?? "";
            string ui_loan_createEndDate = context.Request.Params["ui_loan_createEndDate"] ?? "";

            int totalCount;   //输出参数
            string strJson = loanBll.GetPagerData(ui_loan_bank, ui_loan_lenders, ui_loan_createStartDate, ui_loan_createEndDate, sort + " " + order, pagesize, pageindex, out totalCount);
            context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

            userOperateLog.OperateInfo = "查询贷款费用";
            userOperateLog.IfSuccess = true;
            userOperateLog.Description = "排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 添加贷款费用
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void AddLoan(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("loan", "add", userFromCookie.Id))
            {
                string ui_loan_Bank_add = context.Request.Params["ui_loan_Bank_add"] ?? "";
                string ui_loan_Lenders_add = context.Request.Params["ui_loan_Lenders_add"] ?? "";
                string ui_loan_LoanAmount_add = context.Request.Params["ui_loan_LoanAmount_add"] ?? "";
                string ui_loan_LoanDate_add = context.Request.Params["ui_loan_LoanDate_add"] ?? "";
                string ui_loan_Remark_add = context.Request.Params["ui_loan_Remark_add"] ?? "";

                DriveMgr.Model.LoanModel loanAdd = new Model.LoanModel();
                loanAdd.Bank = ui_loan_Bank_add.Trim();
                loanAdd.Lenders = ui_loan_Lenders_add.Trim();
                loanAdd.LoanAmount = decimal.Parse(ui_loan_LoanAmount_add);
                loanAdd.LoanDate = DateTime.Parse(ui_loan_LoanDate_add);
                loanAdd.Remark = ui_loan_Remark_add.Trim();

                loanAdd.CreateDate = DateTime.Now;
                loanAdd.CreatePerson = userFromCookie.UserId;
                loanAdd.UpdatePerson = userFromCookie.UserId;
                loanAdd.UpdateDate = DateTime.Now;

                if (loanBll.AddLoan(loanAdd))
                {
                    userOperateLog.OperateInfo = "添加贷款费用";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "添加成功，贷款银行：" + ui_loan_Bank_add.Trim();
                    context.Response.Write("{\"msg\":\"添加成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "添加贷款费用";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "添加失败";
                    context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "添加贷款费用";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 编辑贷款
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void EditLoan(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("loan", "edit", userFromCookie.Id))
            {
                int id = Convert.ToInt32(context.Request.Params["id"]);
                DriveMgr.Model.LoanModel loanEdit = loanBll.GetLoanModel(id);
                string ui_loan_Bank_edit = context.Request.Params["ui_loan_Bank_edit"] ?? "";
                string ui_loan_Lenders_edit = context.Request.Params["ui_loan_Lenders_edit"] ?? "";
                string ui_loan_LoanAmount_edit = context.Request.Params["ui_loan_LoanAmount_edit"] ?? "";
                string ui_loan_LoanDate_edit = context.Request.Params["ui_loan_LoanDate_edit"] ?? "";
                string ui_loan_Remark_edit = context.Request.Params["ui_loan_Remark_edit"] ?? "";

                loanEdit.Bank = ui_loan_Bank_edit.Trim();
                loanEdit.Lenders = ui_loan_Lenders_edit.Trim();
                loanEdit.LoanAmount = decimal.Parse(ui_loan_LoanAmount_edit);
                loanEdit.LoanDate = DateTime.Parse(ui_loan_LoanDate_edit);
                loanEdit.Remark = ui_loan_Remark_edit.Trim();

                loanEdit.UpdatePerson = userFromCookie.UserId;
                loanEdit.UpdateDate = DateTime.Now;

                if (loanBll.UpdateLoan(loanEdit))
                {
                    userOperateLog.OperateInfo = "修改贷款费用";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "修改成功，贷款费用主键：" + loanEdit.Id;
                    context.Response.Write("{\"msg\":\"修改成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "修改贷款费用";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "修改失败";
                    context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "修改贷款费用";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 删除贷款
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void DelLoan(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("loan", "delete", userFromCookie.Id))
            {
                string ids = context.Request.Params["id"].Trim(',');
                if (loanBll.DeleteLoanList(ids))
                {
                    userOperateLog.OperateInfo = "删除贷款费用";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "删除成功，贷款费用主键：" + ids;
                    context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "删除贷款费用";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "删除失败";
                    context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "删除贷款费用";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }
    }
}