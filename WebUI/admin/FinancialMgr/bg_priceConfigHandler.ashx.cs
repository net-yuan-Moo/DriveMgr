/***********************
 @ author:zlong
 @ Date:2015-01-11
 @ Desc:单价配置界面层
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
    /// bg_priceConfigHandler 的摘要说明
    /// </summary>
    public class bg_priceConfigHandler : IHttpHandler
    {
        DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
        PriceConfigBLL priceConfigBll = new PriceConfigBLL();
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
                        SearchPriceConfig(context);
                        break;
                    case "getAll":
                        string aa = priceConfigBll.GetPriceConfigDT(0);
                        context.Response.Write(priceConfigBll.GetPriceConfigDT(0));
                        break;
                    case "add":
                        AddPriceConfig(userFromCookie, context);
                        break;
                    case "edit":
                        EditPriceConfig(userFromCookie, context);
                        break;
                    case "delete":
                        DelPriceConfig(userFromCookie, context);
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
        /// 查询单价配置
        /// </summary>
        /// <param name="context"></param>
        private void SearchPriceConfig(HttpContext context)
        {
            string sort = context.Request.Params["sort"];  //排序列
            string order = context.Request.Params["order"];  //排序方式 asc或者desc
            int pageindex = int.Parse(context.Request.Params["page"]);
            int pagesize = int.Parse(context.Request.Params["rows"]);

            int totalCount;   //输出参数
            string strJson = priceConfigBll.GetPagerData(sort + " " + order, pagesize, pageindex, out totalCount);
            context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

            userOperateLog.OperateInfo = "查询单价配置";
            userOperateLog.IfSuccess = true;
            userOperateLog.Description = "排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 添加单价配置
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void AddPriceConfig(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("priceConfig", "add", userFromCookie.Id))
            {
                string ui_priceConfig_PriceTypeName_add = context.Request.Params["ui_priceConfig_PriceTypeName_add"] ?? "";
                string ui_priceConfig_ConfigType_add = context.Request.Params["ui_priceConfig_ConfigType_add"] ?? "";
                string ui_priceConfig_Price_add = context.Request.Params["ui_priceConfig_Price_add"] ?? "";
                string ui_priceConfig_Remark_add = context.Request.Params["ui_priceConfig_Remark_add"] ?? "";

                string test = context.Request.Params["ccTest"] ?? "";

                DriveMgr.Model.PriceConfigModel priceConfigAdd = new Model.PriceConfigModel();
                priceConfigAdd.PriceTypeName = ui_priceConfig_PriceTypeName_add.Trim();
                priceConfigAdd.Price = decimal.Parse(ui_priceConfig_Price_add);
                priceConfigAdd.ConfigType = Int32.Parse(ui_priceConfig_ConfigType_add);
                priceConfigAdd.Remark = ui_priceConfig_Remark_add.Trim();

                priceConfigAdd.CreateDate = DateTime.Now;
                priceConfigAdd.CreatePerson = userFromCookie.UserId;
                priceConfigAdd.UpdatePerson = userFromCookie.UserId;
                priceConfigAdd.UpdateDate = DateTime.Now;

                if (priceConfigBll.AddPriceConfig(priceConfigAdd))
                {
                    userOperateLog.OperateInfo = "添加单价配置";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "添加成功，单价配置：" + ui_priceConfig_PriceTypeName_add.Trim();
                    context.Response.Write("{\"msg\":\"添加成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "添加单价配置";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "添加失败";
                    context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "添加单价配置";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 编辑单价配置
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void EditPriceConfig(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("priceConfig", "edit", userFromCookie.Id))
            {
                int id = Convert.ToInt32(context.Request.Params["id"]);
                DriveMgr.Model.PriceConfigModel priceConfigEdit = priceConfigBll.GetPriceConfigModel(id);
                string ui_priceConfig_PriceTypeName_edit = context.Request.Params["ui_priceConfig_PriceTypeName_edit"] ?? "";
                string ui_priceConfig_ConfigType_edit = context.Request.Params["ui_priceConfig_ConfigType_edit"] ?? "";
                string ui_priceConfig_Price_edit = context.Request.Params["ui_priceConfig_Price_edit"] ?? "";
                string ui_priceConfig_Remark_edit = context.Request.Params["ui_priceConfig_Remark_edit"] ?? "";

                priceConfigEdit.PriceTypeName = ui_priceConfig_PriceTypeName_edit.Trim();
                priceConfigEdit.Price = decimal.Parse(ui_priceConfig_Price_edit);
                priceConfigEdit.ConfigType = Int32.Parse(ui_priceConfig_ConfigType_edit);
                priceConfigEdit.Remark = ui_priceConfig_Remark_edit.Trim();

                priceConfigEdit.UpdatePerson = userFromCookie.UserId;
                priceConfigEdit.UpdateDate = DateTime.Now;

                if (priceConfigBll.UpdatePriceConfig(priceConfigEdit))
                {
                    userOperateLog.OperateInfo = "修改单价配置";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "修改成功，单价配置主键：" + priceConfigEdit.Id;
                    context.Response.Write("{\"msg\":\"修改成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "修改单价配置";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "修改失败";
                    context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "修改单价配置";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 删除单价配置
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void DelPriceConfig(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("priceConfig", "delete", userFromCookie.Id))
            {
                string ids = context.Request.Params["id"].Trim(',');
                if (priceConfigBll.DeletePriceConfigList(ids))
                {
                    userOperateLog.OperateInfo = "删除单价配置";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "删除成功，单价配置主键：" + ids;
                    context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "删除单价配置";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "删除失败";
                    context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "删除单价配置";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }
    }
}