/***********************
 @ author:zlong
 @ Date:2015-01-17
 @ Desc:物资类别界面层
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
    /// bg_goodsCategoryHandler 的摘要说明
    /// </summary>
    public class bg_goodsCategoryHandler : IHttpHandler
    {
        DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
        GoodsCategoryBLL goodsCategoryBll = new GoodsCategoryBLL();
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
                        SearchGoodsCategory(context);
                        break;                    
                    case "add":
                        AddGoodsCategory(userFromCookie, context);
                        break;
                    case "edit":
                        EditGoodsCategory(userFromCookie, context);
                        break;
                    case "delete":
                        DelGoodsCategory(userFromCookie, context);
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
        /// 查询物资类别
        /// </summary>
        /// <param name="context"></param>
        private void SearchGoodsCategory(HttpContext context)
        {
            string sort = context.Request.Params["sort"];  //排序列
            string order = context.Request.Params["order"];  //排序方式 asc或者desc
            int pageindex = int.Parse(context.Request.Params["page"]);
            int pagesize = int.Parse(context.Request.Params["rows"]);

            int totalCount;   //输出参数
            string strJson = goodsCategoryBll.GetPagerData(sort + " " + order, pagesize, pageindex, out totalCount);
            context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

            userOperateLog.OperateInfo = "查询物资类别";
            userOperateLog.IfSuccess = true;
            userOperateLog.Description = "排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 添加物资类别
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void AddGoodsCategory(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("goodsCategory", "add", userFromCookie.Id))
            {
                string ui_goodsCategory_CategoryName_add = context.Request.Params["ui_goodsCategory_CategoryName_add"] ?? "";
                string ui_goodsCategory_Remark_add = context.Request.Params["ui_goodsCategory_Remark_add"] ?? "";

                DriveMgr.Model.GoodsCategoryModel goodsCategoryAdd = new Model.GoodsCategoryModel();
                goodsCategoryAdd.CategoryName = ui_goodsCategory_CategoryName_add.Trim();
                goodsCategoryAdd.Remark = ui_goodsCategory_Remark_add.Trim();

                goodsCategoryAdd.CreateDate = DateTime.Now;
                goodsCategoryAdd.CreatePerson = userFromCookie.UserId;
                goodsCategoryAdd.UpdatePerson = userFromCookie.UserId;
                goodsCategoryAdd.UpdateDate = DateTime.Now;

                if (!goodsCategoryBll.IsExistGoodsCategory(goodsCategoryAdd.CategoryName))
                {
                    if (goodsCategoryBll.AddGoodsCategory(goodsCategoryAdd))
                    {
                        userOperateLog.OperateInfo = "添加物资类别";
                        userOperateLog.IfSuccess = true;
                        userOperateLog.Description = "添加成功，物资类别：" + ui_goodsCategory_CategoryName_add.Trim();
                        context.Response.Write("{\"msg\":\"添加成功！\",\"success\":true}");
                    }
                    else
                    {
                        userOperateLog.OperateInfo = "添加物资类别";
                        userOperateLog.IfSuccess = false;
                        userOperateLog.Description = "添加失败";
                        context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                    }
                }
                else
                {
                    context.Response.Write("{\"msg\":\"该物资类别已经存在！\",\"success\":true}");
                }                
            }
            else
            {
                userOperateLog.OperateInfo = "添加物资类别";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 编辑物资类别
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void EditGoodsCategory(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("goodsCategory", "edit", userFromCookie.Id))
            {
                int id = Convert.ToInt32(context.Request.Params["id"]);
                DriveMgr.Model.GoodsCategoryModel goodsCategoryEdit = goodsCategoryBll.GetGoodsCategoryModel(id);
                string ui_goodsCategory_CategoryName_edit = context.Request.Params["ui_goodsCategory_CategoryName_edit"] ?? "";
                string ui_goodsCategory_Remark_edit = context.Request.Params["ui_goodsCategory_Remark_edit"] ?? "";

                goodsCategoryEdit.CategoryName = ui_goodsCategory_CategoryName_edit.Trim();
                goodsCategoryEdit.Remark = ui_goodsCategory_Remark_edit.Trim();

                goodsCategoryEdit.UpdatePerson = userFromCookie.UserId;
                goodsCategoryEdit.UpdateDate = DateTime.Now;

                if (goodsCategoryBll.UpdateGoodsCategory(goodsCategoryEdit))
                {
                    userOperateLog.OperateInfo = "修改物资类别";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "修改成功，物资类别主键：" + goodsCategoryEdit.Id;
                    context.Response.Write("{\"msg\":\"修改成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "修改物资类别";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "修改失败";
                    context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "修改物资类别";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 删除物资类别
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void DelGoodsCategory(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("goodsCategory", "delete", userFromCookie.Id))
            {
                string ids = context.Request.Params["id"].Trim(',');
                if (goodsCategoryBll.DeleteGoodsCategoryList(ids))
                {
                    userOperateLog.OperateInfo = "删除物资类别";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "删除成功，物资类别主键：" + ids;
                    context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "删除物资类别";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "删除失败";
                    context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "删除物资类别";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }
    }
}