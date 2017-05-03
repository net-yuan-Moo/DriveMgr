/***********************
 @ author:zlong
 @ Date:2015-01-20
 @ Desc:出库明细信息界面层
 * ********************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DriveMgr;
using DriveMgr.BLL;
using DriveMgr.Model;
using System.Web.Script.Serialization;

namespace DriveMgr.WebUI.GoodsMgr
{
    /// <summary>
    /// bg_outStoreHouseDetailsHandler 的摘要说明
    /// </summary>
    public class bg_outStoreHouseDetailsHandler : IHttpHandler
    {
        DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
        OutStoreHouseDetailsBLL outStoreHouseDetailsBll = new OutStoreHouseDetailsBLL();
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
                        SearchOutStoreHouseDetails(context);
                        break;
                    case "searchEnterStoreHouseDetails":
                        SearchOutDetailsByID(context);
                        break;
                    case "delete":
                        DelOutStoreHouseDetails(userFromCookie, context);
                        break;
                    case "getOutStoreHouseDT":
                        OutStoreHouseBLL outStoreHouseBLL = new OutStoreHouseBLL();
                        context.Response.Write(outStoreHouseBLL.GetOutStoreHouseDT());
                        break;
                    case "getGoodsCategoryDT":
                        GoodsCategoryBLL goodsCategoryBLL = new GoodsCategoryBLL();
                        context.Response.Write(goodsCategoryBLL.GetGoodsCategoryDT());
                        break;
                    case "getGoodsDT":
                        GoodsBLL goodsBLL = new GoodsBLL();
                        context.Response.Write(goodsBLL.GetGoodsDT());
                        break;
                    case "add":
                        AddOutStoreHouseDetails(userFromCookie, context);
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
        /// 查询出库明细信息
        /// </summary>
        /// <param name="context"></param>
        private void SearchOutStoreHouseDetails(HttpContext context)
        {
            string sort = context.Request.Params["sort"];  //排序列
            string order = context.Request.Params["order"];  //排序方式 asc或者desc
            int pageindex = int.Parse(context.Request.Params["page"]);
            int pagesize = int.Parse(context.Request.Params["rows"]);

            string ui_outStoreHouseDetails_outDetailsSN = context.Request.Params["ui_outStoreHouseDetails_outDetailsSN"] ?? "";
            string ui_outStoreHouseDetails_outSN = context.Request.Params["ui_outStoreHouseDetails_outSN"] ?? "";
            string ui_outStoreHouseDetails_goodsName = context.Request.Params["ui_outStoreHouseDetails_goodsName"] ?? "";
            string ui_outStoreHouseDetails_categoryName = context.Request.Params["ui_outStoreHouseDetails_categoryName"] ?? "";

            int totalCount;   //输出参数
            string strJson = outStoreHouseDetailsBll.GetPagerData(ui_outStoreHouseDetails_outDetailsSN, ui_outStoreHouseDetails_outSN, ui_outStoreHouseDetails_goodsName, ui_outStoreHouseDetails_categoryName, sort + " " + order, pagesize, pageindex, out totalCount);
            context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

            userOperateLog.OperateInfo = "查询出库明细信息";
            userOperateLog.IfSuccess = true;
            userOperateLog.Description = "排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 根据出库单编号查询出库明细信息
        /// </summary>
        /// <param name="context"></param>
        private void SearchOutDetailsByID(HttpContext context)
        {
            string outStoreHouseID = context.Request.Params["outStoreHouseID"];
            string sort = context.Request.Params["sort"];  //排序列
            string order = context.Request.Params["order"];  //排序方式 asc或者desc
            int pageindex = int.Parse(context.Request.Params["page"]);
            int pagesize = int.Parse(context.Request.Params["rows"]);
            int totalCount;   //输出参数
            string strJson = outStoreHouseDetailsBll.GetPagerData(string.Empty, outStoreHouseID, string.Empty, string.Empty, sort + " " + order, pagesize, pageindex, out totalCount);
            context.Response.Write(strJson);

        }

        /// <summary>
        /// 删除出库单明细
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void DelOutStoreHouseDetails(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("outStoreHouseDetails", "delete", userFromCookie.Id))
            {
                string ids = context.Request.Params["id"].Trim(',');
                if (outStoreHouseDetailsBll.DeleteOutStoreHouseDetailsList(ids))
                {
                    userOperateLog.OperateInfo = "删除出库明细信息";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "删除成功，出库明细信息主键：" + ids;
                    context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "删除出库明细信息";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "删除失败";
                    context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "删除出库明细信息";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        private void AddOutStoreHouseDetails(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            GoodsBLL goodsBll = new GoodsBLL();
            OutStoreHouseBLL outStoreHouseBLL = new OutStoreHouseBLL();

            string outDetailStr = "[" + context.Request.Params["outDetailStr"].Trim(',') + "]";
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<OutStoreHouseDetailsModel> outStoreHouseDetailsModelList = serializer.Deserialize<List<OutStoreHouseDetailsModel>>(outDetailStr);

            OutStoreHouseModel outStoreHouseAdd = new OutStoreHouseModel();
            outStoreHouseAdd.OutDate = DateTime.Now;
            outStoreHouseAdd.HandlePerson = userFromCookie.UserId;
            outStoreHouseAdd.CreateDate = DateTime.Now;
            outStoreHouseAdd.CreatePerson = userFromCookie.UserId;
            outStoreHouseAdd.UpdatePerson = userFromCookie.UserId;
            outStoreHouseAdd.UpdateDate = DateTime.Now;
            outStoreHouseAdd.DeleteMark = false;

            int outStoreHouseID = outStoreHouseBLL.AddOutStoreHouse(outStoreHouseAdd);

            foreach (OutStoreHouseDetailsModel model in outStoreHouseDetailsModelList)
            {
                OutStoreHouseDetailsModel newOutStoreHouseDetailsModel = new OutStoreHouseDetailsModel();
                newOutStoreHouseDetailsModel.GoodsID = model.GoodsID;
                newOutStoreHouseDetailsModel.OutQuantity = model.OutQuantity;
                newOutStoreHouseDetailsModel.OutStoreHouseID = outStoreHouseID;
                newOutStoreHouseDetailsModel.DeleteMark = false;
                if (outStoreHouseDetailsBll.AddOutStoreHouseDetails(newOutStoreHouseDetailsModel))    //添加入库明细
                {
                    GoodsModel goodsModel = goodsBll.GetGoodsModel(newOutStoreHouseDetailsModel.GoodsID.Value);
                    goodsModel.RealQuantity -= newOutStoreHouseDetailsModel.OutQuantity;
                    goodsBll.UpdateGoods(goodsModel);    //更新物品数量
                }
            }

            context.Response.Write("{\"msg\":\"出库成功！\",\"success\":true}");

        }

    }
}