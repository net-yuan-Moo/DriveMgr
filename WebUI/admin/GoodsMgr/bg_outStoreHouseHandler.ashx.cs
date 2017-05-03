/***********************
 @ author:zlong
 @ Date:2015-01-20
 @ Desc:出库单信息界面层
 * ********************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DriveMgr;
using DriveMgr.BLL;

namespace DriveMgr.WebUI.GoodsMgr
{
    /// <summary>
    /// bg_outStoreHouseHandler 的摘要说明
    /// </summary>
    public class bg_outStoreHouseHandler : IHttpHandler
    {
        DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
        OutStoreHouseBLL outStoreHouseBll = new OutStoreHouseBLL();
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
                        SearchOutStoreHouse(context);
                        break;
                    case "delete":
                        DelOutStoreHouse(userFromCookie, context);
                        break;
                    case "getPriceConfigDT":
                        PriceConfigBLL priceConfigBll = new PriceConfigBLL();
                        context.Response.Write(priceConfigBll.GetPriceConfigDT(1));
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
        /// 查询出库单信息
        /// </summary>
        /// <param name="context"></param>
        private void SearchOutStoreHouse(HttpContext context)
        {
            string sort = context.Request.Params["sort"];  //排序列
            string order = context.Request.Params["order"];  //排序方式 asc或者desc
            int pageindex = int.Parse(context.Request.Params["page"]);
            int pagesize = int.Parse(context.Request.Params["rows"]);

            string ui_outStoreHouse_outSN = context.Request.Params["ui_outStoreHouse_outSN"] ?? "";
            string ui_outStoreHouse_createStartDate = context.Request.Params["ui_outStoreHouse_createStartDate"] ?? "";
            string ui_outStoreHouse_createEndDate = context.Request.Params["ui_outStoreHouse_createEndDate"] ?? "";

            int totalCount;   //输出参数
            string strJson = outStoreHouseBll.GetPagerData(ui_outStoreHouse_outSN, ui_outStoreHouse_createStartDate, ui_outStoreHouse_createEndDate, sort + " " + order, pagesize, pageindex, out totalCount);
            context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

            userOperateLog.OperateInfo = "查询出库单信息";
            userOperateLog.IfSuccess = true;
            userOperateLog.Description = "排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 删除车辆
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void DelOutStoreHouse(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("outStoreHouse", "delete", userFromCookie.Id))
            {
                string ids = context.Request.Params["id"].Trim(',');
                if (outStoreHouseBll.DeleteOutStoreHouseList(ids))
                {
                    userOperateLog.OperateInfo = "删除出库单信息";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "删除成功，出库单信息主键：" + ids;
                    context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "删除出库单信息";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "删除失败";
                    context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "删除出库单信息";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }
    }
}