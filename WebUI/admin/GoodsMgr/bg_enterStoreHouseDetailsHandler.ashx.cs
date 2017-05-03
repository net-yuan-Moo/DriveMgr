/***********************
 @ author:zlong
 @ Date:2015-01-20
 @ Desc:入库明细信息界面层
 * ********************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DriveMgr;
using DriveMgr.BLL;
using System.Web.Script.Serialization;
using DriveMgr.Model;

namespace DriveMgr.WebUI.GoodsMgr
{
    /// <summary>
    /// bg_enterStoreHouseDetailsHandler 的摘要说明
    /// </summary>
    public class bg_enterStoreHouseDetailsHandler : IHttpHandler
    {
        DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
        EnterStoreHouseDetailsBLL enterStoreHouseDetailsBll = new EnterStoreHouseDetailsBLL();
        
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
                        SearchEnterStoreHouseDetails(context);
                        break;
                    case "searchEnterStoreHouseDetails":
                        SearchEnterDetailsByID(context);
                        break;
                    case "delete":
                        DelEnterStoreHouseDetails(userFromCookie, context);
                        break;
                    case "getEnterStoreHouseDT":
                        EnterStoreHouseBLL enterStoreHouseBLL = new EnterStoreHouseBLL();
                        context.Response.Write(enterStoreHouseBLL.GetEnterStoreHouseDT());
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
                        AddEnterStoreHouseDetails(userFromCookie, context);
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
        /// 查询入库明细信息
        /// </summary>
        /// <param name="context"></param>
        private void SearchEnterStoreHouseDetails(HttpContext context)
        {
            string sort = context.Request.Params["sort"];  //排序列
            string order = context.Request.Params["order"];  //排序方式 asc或者desc
            int pageindex = int.Parse(context.Request.Params["page"]);
            int pagesize = int.Parse(context.Request.Params["rows"]);

            string ui_enterStoreHouseDetails_enterDetailsSN = context.Request.Params["ui_enterStoreHouseDetails_enterDetailsSN"] ?? "";
            string ui_enterStoreHouseDetails_enterSN = context.Request.Params["ui_enterStoreHouseDetails_enterSN"] ?? "";
            string ui_enterStoreHouseDetails_goodsName = context.Request.Params["ui_enterStoreHouseDetails_goodsName"] ?? "";
            string ui_enterStoreHouseDetails_categoryName = context.Request.Params["ui_enterStoreHouseDetails_categoryName"] ?? "";

            int totalCount;   //输出参数
            string strJson = enterStoreHouseDetailsBll.GetPagerData(ui_enterStoreHouseDetails_enterDetailsSN, ui_enterStoreHouseDetails_enterSN, ui_enterStoreHouseDetails_goodsName, ui_enterStoreHouseDetails_categoryName, sort + " " + order, pagesize, pageindex, out totalCount);
            context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

            userOperateLog.OperateInfo = "查询入库明细信息";
            userOperateLog.IfSuccess = true;
            userOperateLog.Description = "排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 根据入库单编号查询入库明细信息
        /// </summary>
        /// <param name="context"></param>
        private void SearchEnterDetailsByID(HttpContext context)
        {
            string enterStoreHouseID = context.Request.Params["enterStoreHouseID"];
            string sort = context.Request.Params["sort"];  //排序列
            string order = context.Request.Params["order"];  //排序方式 asc或者desc
            int pageindex = int.Parse(context.Request.Params["page"]);
            int pagesize = int.Parse(context.Request.Params["rows"]);
            int totalCount;   //输出参数
            string strJson = enterStoreHouseDetailsBll.GetPagerData(string.Empty, enterStoreHouseID, string.Empty, string.Empty, sort + " " + order, pagesize, pageindex, out totalCount);
            context.Response.Write(strJson);            
                        
        }

        /// <summary>
        /// 删除车辆
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void DelEnterStoreHouseDetails(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("enterStoreHouseDetails", "delete", userFromCookie.Id))
            {
                string ids = context.Request.Params["id"].Trim(',');
                if (enterStoreHouseDetailsBll.DeleteEnterStoreHouseDetailsList(ids))
                {
                    userOperateLog.OperateInfo = "删除入库明细信息";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "删除成功，入库明细信息主键：" + ids;
                    context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "删除入库明细信息";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "删除失败";
                    context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "删除入库明细信息";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        private void AddEnterStoreHouseDetails(DriveMgr.Model.User userFromCookie, HttpContext context)
        {           
            GoodsBLL goodsBll = new GoodsBLL();
            EnterStoreHouseBLL enterStoreHouseBLL = new EnterStoreHouseBLL();

            string enterDetail = "[" + context.Request.Params["enterDetailStr"].Trim(',') + "]";
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<EnterStoreHouseDetailsModel> enterStoreHouseDetailsModelList = serializer.Deserialize<List<EnterStoreHouseDetailsModel>>(enterDetail);            

            EnterStoreHouseModel enterStoreHouseAdd = new EnterStoreHouseModel();
            enterStoreHouseAdd.EnterDate = DateTime.Now;
            enterStoreHouseAdd.HandlePerson = userFromCookie.UserId;
            enterStoreHouseAdd.CreateDate = DateTime.Now;
            enterStoreHouseAdd.CreatePerson = userFromCookie.UserId;
            enterStoreHouseAdd.UpdatePerson = userFromCookie.UserId;
            enterStoreHouseAdd.UpdateDate = DateTime.Now;
            enterStoreHouseAdd.DeleteMark = false;

            int enterStoreHouseID = enterStoreHouseBLL.AddEnterStoreHouse(enterStoreHouseAdd);

            foreach (EnterStoreHouseDetailsModel model in enterStoreHouseDetailsModelList)
            {
                EnterStoreHouseDetailsModel newEnterStoreHouseDetailsModel = new EnterStoreHouseDetailsModel();
                newEnterStoreHouseDetailsModel.GoodsID = model.GoodsID;
                newEnterStoreHouseDetailsModel.EnterQuantity = model.EnterQuantity;
                newEnterStoreHouseDetailsModel.EnterStoreHouseID = enterStoreHouseID;
                newEnterStoreHouseDetailsModel.DeleteMark = false;
                if (enterStoreHouseDetailsBll.AddEnterStoreHouseDetails(newEnterStoreHouseDetailsModel))    //添加入库明细
                {
                    GoodsModel goodsModel = goodsBll.GetGoodsModel(newEnterStoreHouseDetailsModel.GoodsID.Value);
                    goodsModel.RealQuantity += newEnterStoreHouseDetailsModel.EnterQuantity;
                    goodsBll.UpdateGoods(goodsModel);    //更新物品数量
                }
            }

            context.Response.Write("{\"msg\":\"入库成功！\",\"success\":true}");

        }

    }
}

