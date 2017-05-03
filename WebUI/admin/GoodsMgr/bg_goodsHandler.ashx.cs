/***********************
 @ author:zlong
 @ Date:2015-01-17
 @ Desc:物品界面层
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
    /// bg_goodsHandler 的摘要说明
    /// </summary>
    public class bg_goodsHandler : IHttpHandler
    {
        DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
        GoodsBLL goodsBll = new GoodsBLL();
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
                        SearchGoods(context);
                        break;
                    case "add":
                        AddGoods(userFromCookie, context);
                        break;
                    case "getGoodsCategoryDT":
                        GoodsCategoryBLL goodsCategoryBll = new GoodsCategoryBLL();
                        context.Response.Write(goodsCategoryBll.GetGoodsCategoryDT());
                        break;
                    case "edit":
                        EditGoods(userFromCookie, context);
                        break;
                    case "delete":
                        DelGoods(userFromCookie, context);
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
        /// 查询物品
        /// </summary>
        /// <param name="context"></param>
        private void SearchGoods(HttpContext context)
        {
            string sort = context.Request.Params["sort"];  //排序列
            string order = context.Request.Params["order"];  //排序方式 asc或者desc
            int pageindex = int.Parse(context.Request.Params["page"]);
            int pagesize = int.Parse(context.Request.Params["rows"]);

            int totalCount;   //输出参数
            string strJson = goodsBll.GetPagerData(sort + " " + order, pagesize, pageindex, out totalCount);
            context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

            userOperateLog.OperateInfo = "查询物品";
            userOperateLog.IfSuccess = true;
            userOperateLog.Description = "排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 添加物品
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void AddGoods(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("goods", "add", userFromCookie.Id))
            {
                string ui_goods_GoodsName_add = context.Request.Params["ui_goods_GoodsName_add"] ?? "";
                int ui_goods_GoodsCategory_add = Int32.Parse(context.Request.Params["ui_goods_GoodsCategory_add"]);
                int ui_goods_MinQuantity_add = Int32.Parse(context.Request.Params["ui_goods_MinQuantity_add"]);
                int ui_goods_MaxQuantity_add = Int32.Parse(context.Request.Params["ui_goods_MaxQuantity_add"]);
                int ui_goods_RealQuantity_add = Int32.Parse(context.Request.Params["ui_goods_RealQuantity_add"]);
                string ui_goods_Specification_add = context.Request.Params["ui_goods_Specification_add"] ?? "";
                string ui_goods_Remark_add = context.Request.Params["ui_goods_Remark_add"] ?? "";

                DriveMgr.Model.GoodsModel goodsAdd = new Model.GoodsModel();
                goodsAdd.GoodsName = ui_goods_GoodsName_add.Trim();
                goodsAdd.GoodsCategoryID = ui_goods_GoodsCategory_add;
                goodsAdd.MinQuantity = ui_goods_MinQuantity_add;
                goodsAdd.MaxQuantity = ui_goods_MaxQuantity_add;
                goodsAdd.RealQuantity = ui_goods_RealQuantity_add;
                goodsAdd.Specification = ui_goods_Specification_add.Trim();
                goodsAdd.Remark = ui_goods_Remark_add.Trim();

                goodsAdd.CreateDate = DateTime.Now;
                goodsAdd.CreatePerson = userFromCookie.UserId;
                goodsAdd.UpdatePerson = userFromCookie.UserId;
                goodsAdd.UpdateDate = DateTime.Now;

                if (!goodsBll.IsExistGoods(goodsAdd.GoodsName))
                {
                    if (goodsBll.AddGoods(goodsAdd))
                    {
                        userOperateLog.OperateInfo = "添加物品";
                        userOperateLog.IfSuccess = true;
                        userOperateLog.Description = "添加成功，物品：" + ui_goods_GoodsName_add.Trim();
                        context.Response.Write("{\"msg\":\"添加成功！\",\"success\":true}");
                    }
                    else
                    {
                        userOperateLog.OperateInfo = "添加物品";
                        userOperateLog.IfSuccess = false;
                        userOperateLog.Description = "添加失败";
                        context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                    }
                }
                else
                {
                    context.Response.Write("{\"msg\":\"该物品已经存在！\",\"success\":true}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "添加物品";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 编辑物品
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void EditGoods(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("goods", "edit", userFromCookie.Id))
            {
                int id = Convert.ToInt32(context.Request.Params["id"]);
                DriveMgr.Model.GoodsModel goodsEdit = goodsBll.GetGoodsModel(id);
                string ui_goods_GoodsName_edit = context.Request.Params["ui_goods_GoodsName_edit"] ?? "";
                int ui_goods_GoodsCategory_edit = Int32.Parse(context.Request.Params["ui_goods_GoodsCategory_edit"]);
                int ui_goods_MinQuantity_edit = Int32.Parse(context.Request.Params["ui_goods_MinQuantity_edit"]);
                int ui_goods_MaxQuantity_edit = Int32.Parse(context.Request.Params["ui_goods_MaxQuantity_edit"]);
                int ui_goods_RealQuantity_edit = Int32.Parse(context.Request.Params["ui_goods_RealQuantity_edit"]);
                string ui_goods_Specification_edit = context.Request.Params["ui_goods_Specification_edit"] ?? "";
                string ui_goods_Remark_edit = context.Request.Params["ui_goods_Remark_edit"] ?? "";

                goodsEdit.GoodsName = ui_goods_GoodsName_edit.Trim();
                goodsEdit.GoodsCategoryID = ui_goods_GoodsCategory_edit;
                goodsEdit.MinQuantity = ui_goods_MinQuantity_edit;
                goodsEdit.MaxQuantity = ui_goods_MaxQuantity_edit;
                goodsEdit.RealQuantity = ui_goods_RealQuantity_edit;
                goodsEdit.Specification = ui_goods_Specification_edit.Trim();
                goodsEdit.Remark = ui_goods_Remark_edit.Trim();

                goodsEdit.UpdatePerson = userFromCookie.UserId;
                goodsEdit.UpdateDate = DateTime.Now;

                if (goodsBll.UpdateGoods(goodsEdit))
                {
                    userOperateLog.OperateInfo = "修改物品";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "修改成功，物品主键：" + goodsEdit.Id;
                    context.Response.Write("{\"msg\":\"修改成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "修改物品";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "修改失败";
                    context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "修改物品";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 删除物品
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void DelGoods(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("goods", "delete", userFromCookie.Id))
            {
                string ids = context.Request.Params["id"].Trim(',');
                if (goodsBll.DeleteGoodsList(ids))
                {
                    userOperateLog.OperateInfo = "删除物品";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "删除成功，物品主键：" + ids;
                    context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "删除物品";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "删除失败";
                    context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "删除物品";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }
    }
}