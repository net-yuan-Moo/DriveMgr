using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DriveMgr.WebUI.admin.ashx
{
    /// <summary>
    /// 后台导航树
    /// </summary>
    public class bg_menu : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request.Params["action"];
            DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
            try
            {
                DriveMgr.Model.User user = DriveMgr.Common.UserHelper.GetUser(context);   //获取cookie里的用户对象
                userOperateLog = new Model.UserOperateLog();
                userOperateLog.UserIp = context.Request.UserHostAddress;
                userOperateLog.UserName = user.UserId;

                switch (action)
                {
                    case "getUserMenu":  //获取特定用户能看到的菜单（左侧树）
                        context.Response.Write(new DriveMgr.BLL.Menu().GetUserMenu(user.Id));
                        break;
                    case "getAllMenu":   //根据角色id获取此角色有的权限（设置角色时自动勾选已经有的按钮权限）
                        int roleid = Convert.ToInt32(context.Request.Params["roleid"]);  //角色id
                        context.Response.Write(new DriveMgr.BLL.Menu().GetAllMenu(roleid));
                        break;
                    case "getMyAuthority":  //前台根据用户名查“我的权限”
                        context.Response.Write(new DriveMgr.BLL.Menu().GetMyAuthority(user.Id));
                        userOperateLog.OperateInfo = "查询我的信息";
                        userOperateLog.IfSuccess = true;
                        userOperateLog.Description = "查询我的信息";
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "search":
                        string strWhere = "1=1 ";
                        string sort = context.Request.Params["sort"] == null ? "Id" : context.Request.Params["sort"];  //排序列
                        string order = context.Request.Params["order"] == null ? "asc" : context.Request.Params["order"];  //排序方式 asc或者desc
                        int pageindex = int.Parse(context.Request.Params["page"]);
                        int pagesize = int.Parse(context.Request.Params["rows"]);

                        int totalCount;   //输出参数
                        string strJson = "";    //输出结果
                        if (order.IndexOf(',') != -1)   //如果有","就是多列排序（不能拿列判断，列名中间可能有","符号）
                        {
                            //多列排序：
                            //sort：ParentId,Sort,AddDate
                            //order：asc,desc,asc
                            string sortMulti = "";  //拼接排序条件，例：ParentId desc,Sort asc
                            string[] sortArray = sort.Split(',');   //列名中间有","符号，这里也要出错。正常不会有
                            string[] orderArray = order.Split(',');
                            for (int i = 0; i < sortArray.Length; i++)
                            {
                                sortMulti += sortArray[i] + " " + orderArray[i] + ",";
                            }
                            strJson = new DriveMgr.BLL.Menu().GetPager("tbMenu", "Id,Name,ParentId,Code,LinkAddress,Icon,Sort,AddDate", sortMulti.Trim(','), pagesize, pageindex, strWhere, out totalCount);
                            userOperateLog.Description = "查询条件：" + strWhere + " 排序：" + sortMulti.Trim(',') + " 页码/每页大小：" + pageindex + " " + pagesize;
                        }
                        else
                        {
                            strJson = new DriveMgr.BLL.Menu().GetPager("tbMenu", "Id,Name,ParentId,Code,LinkAddress,Icon,Sort,AddDate", sort + " " + order, pagesize, pageindex, strWhere, out totalCount);
                            userOperateLog.Description = "查询条件：" + strWhere + " 排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
                        }

                        context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");
                        userOperateLog.OperateInfo = "查询菜单";
                        userOperateLog.IfSuccess = true;
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "bindFatherMenu":
                        string strResult = new DriveMgr.BLL.Menu().GetAllMenuByCondition("1=1");
                        context.Response.Write(strResult);
                        break;
                    case "add":
                        if (user != null && new DriveMgr.BLL.Authority().IfAuthority("menu", "add", user.Id))
                        {
                            DriveMgr.Model.Menu menuAdd = new Model.Menu();
                            menuAdd.Id = Convert.ToInt32(context.Request.Params["id"]);
                            menuAdd.Name = context.Request.Params["ui_menu_menuname_add"] ?? "";
                            menuAdd.Code = context.Request.Params["ui_menu_codename_add"] ?? "";
                            menuAdd.LinkAddress = context.Request.Params["ui_menu_link_add"] ?? "";
                            menuAdd.Icon = context.Request.Params["ui_menu_iconname_add"] ?? "";
                            menuAdd.Sort = Convert.ToInt32(context.Request.Params["ui_menu_sortname_add"]);
                            menuAdd.AddDate = DateTime.Now;

                            if (context.Request.Params["ui_menu_fathermenuname_add"] != null && context.Request.Params["ui_menu_fathermenuname_add"] != "")
                                menuAdd.ParentId = Convert.ToInt32(context.Request.Params["ui_menu_fathermenuname_add"]);
                            else
                                menuAdd.ParentId = 0;   //根节点

                            bool menuResult = new DriveMgr.BLL.Menu().Add(menuAdd);
                            if (menuResult)
                            {
                                userOperateLog.OperateInfo = "添加菜单";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "添加成功";
                                context.Response.Write("{\"msg\":\"添加成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "添加菜单";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "添加失败";
                                context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "添加菜单";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":true}");
                        }
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "edit":
                        if (user != null && new DriveMgr.BLL.Authority().IfAuthority("menu", "edit", user.Id))
                        {
                            DriveMgr.Model.Menu menuEdit = new Model.Menu();
                            menuEdit.Id = Convert.ToInt32(context.Request.Params["id"]);
                            menuEdit.Name = context.Request.Params["ui_menu_menuname_edit"] ?? "";
                            menuEdit.Code = context.Request.Params["ui_menu_codename_edit"] ?? "";
                            menuEdit.LinkAddress = context.Request.Params["ui_menu_link_edit"] ?? "";
                            menuEdit.Icon = context.Request.Params["ui_menu_iconname_edit"] ?? "";
                            menuEdit.Sort = Convert.ToInt32(context.Request.Params["ui_menu_sortname_edit"]);
                            menuEdit.AddDate = DateTime.Now;

                            bool result = new DriveMgr.BLL.Menu().Update(menuEdit);
                            if (result)
                            {
                                userOperateLog.OperateInfo = "修改菜单";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "修改成功，菜单主键：" + menuEdit.Id;
                                context.Response.Write("{\"msg\":\"修改成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "修改菜单";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "修改失败，菜单主键：" + menuEdit.Id;
                                context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "修改菜单";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "delete":
                        if (user != null && new DriveMgr.BLL.Authority().IfAuthority("menu", "delete", user.Id))
                        {
                            string menuIds = context.Request.Params["id"].Trim(',');
                            if (new DriveMgr.BLL.Menu().DeleteList(menuIds))
                            {
                                userOperateLog.OperateInfo = "删除菜单";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "删除成功，菜单主键：" + menuIds;
                                context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "删除菜单";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "删除失败，菜单主键：" + menuIds;
                                context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "删除菜单";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "distribute":
                        if (user != null && new DriveMgr.BLL.Authority().IfAuthority("menu", "setbutton", user.Id))
                        {
                            string menuButtonId = context.Request.Params["menuButtonId"].Trim(',');   //具体的菜单和按钮权限
                            int menuId = Int32.Parse(context.Request.Params["menuId"]);   //具体的菜单和按钮权限
                            if (new DriveMgr.BLL.MenuButton().Add(menuId, menuButtonId))
                            {
                                userOperateLog.OperateInfo = "分配按钮";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "分配按钮，菜单/按钮Id：" + menuButtonId;
                                context.Response.Write("{\"msg\":\"分配按钮成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "分配按钮";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "分配失败";
                                context.Response.Write("{\"msg\":\"分配失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "分配按钮";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    default:
                        context.Response.Write("{\"result\":\"参数错误！\",\"success\":false}");
                        break;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"msg\":\"" + DriveMgr.Common.JsonHelper.StringFilter(ex.Message) + "\",\"success\":false}");
                userOperateLog.OperateInfo = "菜单功能异常";
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
    }
}