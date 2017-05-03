/***********************
 @ author:zlong
 @ Date:2015-01-09
 @ Desc:收入分类界面层
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
    /// bg_incomeCategoryHandler 的摘要说明
    /// </summary>
    public class bg_incomeCategoryHandler : IHttpHandler
    {
        DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
        IncomeCategoryBLL incomeCategoryBll = new IncomeCategoryBLL();
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
                        SearchIncomeCategory(context);
                        break;
                    case "add":
                        AddIncomeCategory(userFromCookie, context);
                        break;
                    case "edit":
                        EditIncomeCategory(userFromCookie, context);
                        break;
                    case "delete":
                        DelIncomeCategory(userFromCookie, context);
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
        /// 查询收入分类
        /// </summary>
        /// <param name="context"></param>
        private void SearchIncomeCategory(HttpContext context)
        {
            string sort = context.Request.Params["sort"];  //排序列
            string order = context.Request.Params["order"];  //排序方式 asc或者desc
            int pageindex = int.Parse(context.Request.Params["page"]);
            int pagesize = int.Parse(context.Request.Params["rows"]);
            
            string ui_incomeCategory_incomeCategoryname = context.Request.Params["ui_incomeCategory_incomeCategoryname"] ?? "";
            string ui_incomeCategory_startDate = context.Request.Params["ui_incomeCategory_startDate"] ?? "";
            string ui_incomeCategory_endDate = context.Request.Params["ui_incomeCategory_endDate"] ?? "";
            
            int totalCount;   //输出参数
            string strJson = incomeCategoryBll.GetPagerData(ui_incomeCategory_incomeCategoryname, ui_incomeCategory_startDate, ui_incomeCategory_endDate, sort + " " + order, pagesize, pageindex, out totalCount);
            context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

            userOperateLog.OperateInfo = "查询收入分类";
            userOperateLog.IfSuccess = true;
            userOperateLog.Description = "排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 添加收入分类
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void AddIncomeCategory(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("incomeCategory", "add", userFromCookie.Id))
            {
                string ui_incomeCategory_CategoryName_add = context.Request.Params["ui_incomeCategory_CategoryName_add"] ?? "";
                string ui_incomeCategory_Remark_add = context.Request.Params["ui_incomeCategory_Remark_add"] ?? "";

                DriveMgr.Model.IncomeCategoryModel incomeCategoryAdd = new Model.IncomeCategoryModel();
                incomeCategoryAdd.CategoryName = ui_incomeCategory_CategoryName_add.Trim();
                incomeCategoryAdd.Remark = ui_incomeCategory_Remark_add.Trim();
                incomeCategoryAdd.CreateTime = DateTime.Now;
                incomeCategoryAdd.CreatePerson = userFromCookie.UserId;
                incomeCategoryAdd.UpdatePerson = userFromCookie.UserId;
                incomeCategoryAdd.UpdateTime = DateTime.Now;

                if (incomeCategoryBll.AddIncomeCategory(incomeCategoryAdd))
                {
                    userOperateLog.OperateInfo = "添加收入分类";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "添加成功，类型：" + ui_incomeCategory_CategoryName_add.Trim();
                    context.Response.Write("{\"msg\":\"添加成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "添加收入分类";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "添加失败";
                    context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "添加收入分类";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 编辑收入分类
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void EditIncomeCategory(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("incomeCategory", "edit", userFromCookie.Id))
            {
                int id = Convert.ToInt32(context.Request.Params["id"]);
                DriveMgr.Model.IncomeCategoryModel incomeCategoryEdit = incomeCategoryBll.GetIncomeCategoryModel(id);
                string ui_incomeCategory_CategoryName_edit = context.Request.Params["ui_incomeCategory_CategoryName_edit"] ?? "";
                string ui_incomeCategory_Remark_edit = context.Request.Params["ui_incomeCategory_Remark_edit"] ?? "";
                
                incomeCategoryEdit.CategoryName = ui_incomeCategory_CategoryName_edit.Trim();
                incomeCategoryEdit.Remark = ui_incomeCategory_Remark_edit.Trim();
                incomeCategoryEdit.UpdatePerson = userFromCookie.UserId;
                incomeCategoryEdit.UpdateTime = DateTime.Now;

                if (incomeCategoryBll.UpdateIncomeCategory(incomeCategoryEdit))
                {
                    userOperateLog.OperateInfo = "修改收入分类";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "修改成功，收入分类主键：" + incomeCategoryEdit.Id;
                    context.Response.Write("{\"msg\":\"修改成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "修改收入分类";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "修改失败";
                    context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "修改收入分类";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 删除收入分类
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void DelIncomeCategory(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("incomeCategory", "delete", userFromCookie.Id))
            {
                string ids = context.Request.Params["id"].Trim(',');
                if (incomeCategoryBll.DeleteIncomeCategoryList(ids))
                {
                    userOperateLog.OperateInfo = "删除收入分类";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "删除成功，收入分类主键：" + ids;
                    context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "删除收入分类";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "删除失败";
                    context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "删除收入分类";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }
    }
}