/***********************
 @ author:zlong
 @ Date:2015-01-09
 @ Desc:支出分类界面层
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
    /// bg_expensesCategoryHandler 的摘要说明
    /// </summary>
    public class bg_expensesCategoryHandler : IHttpHandler
    {

        DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
        ExpensesCategoryBLL expensesCategoryBll = new ExpensesCategoryBLL();
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
                        SearchExpensesCategory(context);
                        break;
                    case "add":
                        AddExpensesCategory(userFromCookie, context);
                        break;
                    case "edit":
                        EditExpensesCategory(userFromCookie, context);
                        break;
                    case "delete":
                        DelExpensesCategory(userFromCookie, context);
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
        /// 查询支出分类
        /// </summary>
        /// <param name="context"></param>
        private void SearchExpensesCategory(HttpContext context)
        {
            string sort = context.Request.Params["sort"];  //排序列
            string order = context.Request.Params["order"];  //排序方式 asc或者desc
            int pageindex = int.Parse(context.Request.Params["page"]);
            int pagesize = int.Parse(context.Request.Params["rows"]);

            string ui_expensesCategory_expensesCategoryname = context.Request.Params["ui_expensesCategory_expensesCategoryname"] ?? "";
            string ui_expensesCategory_startDate = context.Request.Params["ui_expensesCategory_startDate"] ?? "";
            string ui_expensesCategory_endDate = context.Request.Params["ui_expensesCategory_endDate"] ?? "";

            int totalCount;   //输出参数
            string strJson = expensesCategoryBll.GetPagerData(ui_expensesCategory_expensesCategoryname, ui_expensesCategory_startDate, ui_expensesCategory_endDate, sort + " " + order, pagesize, pageindex, out totalCount);
            context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

            userOperateLog.OperateInfo = "查询支出分类";
            userOperateLog.IfSuccess = true;
            userOperateLog.Description = "排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 添加支出分类
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void AddExpensesCategory(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("expensesCategory", "add", userFromCookie.Id))
            {
                string ui_expensesCategory_CategoryName_add = context.Request.Params["ui_expensesCategory_CategoryName_add"] ?? "";
                string ui_expensesCategory_Remark_add = context.Request.Params["ui_expensesCategory_Remark_add"] ?? "";

                DriveMgr.Model.ExpensesCategoryModel expensesCategoryAdd = new Model.ExpensesCategoryModel();
                expensesCategoryAdd.CategoryName = ui_expensesCategory_CategoryName_add.Trim();
                expensesCategoryAdd.Remark = ui_expensesCategory_Remark_add.Trim();
                expensesCategoryAdd.CreateDate = DateTime.Now;
                expensesCategoryAdd.CreatePerson = userFromCookie.UserId;
                expensesCategoryAdd.UpdatePerson = userFromCookie.UserId;
                expensesCategoryAdd.UpdateDate = DateTime.Now;

                if (expensesCategoryBll.AddExpensesCategory(expensesCategoryAdd))
                {
                    userOperateLog.OperateInfo = "添加支出分类";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "添加成功，类型：" + ui_expensesCategory_CategoryName_add.Trim();
                    context.Response.Write("{\"msg\":\"添加成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "添加支出分类";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "添加失败";
                    context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "添加支出分类";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 编辑支出分类
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void EditExpensesCategory(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("expensesCategory", "edit", userFromCookie.Id))
            {
                int id = Convert.ToInt32(context.Request.Params["id"]);
                DriveMgr.Model.ExpensesCategoryModel expensesCategoryEdit = expensesCategoryBll.GetExpensesCategoryModel(id);
                string ui_expensesCategory_CategoryName_edit = context.Request.Params["ui_expensesCategory_CategoryName_edit"] ?? "";
                string ui_expensesCategory_Remark_edit = context.Request.Params["ui_expensesCategory_Remark_edit"] ?? "";

                expensesCategoryEdit.CategoryName = ui_expensesCategory_CategoryName_edit.Trim();
                expensesCategoryEdit.Remark = ui_expensesCategory_Remark_edit.Trim();
                expensesCategoryEdit.UpdatePerson = userFromCookie.UserId;
                expensesCategoryEdit.UpdateDate = DateTime.Now;

                if (expensesCategoryBll.UpdateExpensesCategory(expensesCategoryEdit))
                {
                    userOperateLog.OperateInfo = "修改支出分类";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "修改成功，支出分类主键：" + expensesCategoryEdit.Id;
                    context.Response.Write("{\"msg\":\"修改成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "修改支出分类";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "修改失败";
                    context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "修改支出分类";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 删除支出分类
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void DelExpensesCategory(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("expensesCategory", "delete", userFromCookie.Id))
            {
                string ids = context.Request.Params["id"].Trim(',');
                if (expensesCategoryBll.DeleteExpensesCategoryList(ids))
                {
                    userOperateLog.OperateInfo = "删除支出分类";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "删除成功，支出分类主键：" + ids;
                    context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "删除支出分类";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "删除失败";
                    context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "删除支出分类";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }
    }
}