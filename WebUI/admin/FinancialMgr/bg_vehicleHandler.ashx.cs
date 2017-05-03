/***********************
 @ author:zlong
 @ Date:2015-01-11
 @ Desc:车辆信息界面层
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
    /// bg_vehicleHandler 的摘要说明
    /// </summary>
    public class bg_vehicleHandler : IHttpHandler
    {
        DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
        VehicleBLL vehicleBll = new VehicleBLL();
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
                        SearchVehicle(context);
                        break;
                    case "add":
                        AddVehicle(userFromCookie, context);
                        break;
                    case "edit":
                        EditVehicle(userFromCookie, context);
                        break;
                    case "delete":
                        DelVehicle(userFromCookie, context);
                        break;
                    case "getVehicleDT":
                        context.Response.Write(vehicleBll.GetVehicleDT());
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
        /// 查询车辆信息
        /// </summary>
        /// <param name="context"></param>
        private void SearchVehicle(HttpContext context)
        {
            string sort = context.Request.Params["sort"];  //排序列
            string order = context.Request.Params["order"];  //排序方式 asc或者desc
            int pageindex = int.Parse(context.Request.Params["page"]);
            int pagesize = int.Parse(context.Request.Params["rows"]);

            string ui_vehicle_licencePlateNum = context.Request.Params["ui_vehicle_licencePlateNum"] ?? "";
            string ui_vehicle_brands = context.Request.Params["ui_vehicle_brands"] ?? "";
            string ui_vehicle_createStartDate = context.Request.Params["ui_vehicle_createStartDate"] ?? "";
            string ui_vehicle_createEndDate = context.Request.Params["ui_vehicle_createEndDate"] ?? "";

            int totalCount;   //输出参数
            string strJson = vehicleBll.GetPagerData(ui_vehicle_licencePlateNum, ui_vehicle_brands, ui_vehicle_createStartDate, ui_vehicle_createEndDate, sort + " " + order, pagesize, pageindex, out totalCount);
            context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

            userOperateLog.OperateInfo = "查询车辆信息";
            userOperateLog.IfSuccess = true;
            userOperateLog.Description = "排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 添加车辆信息
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void AddVehicle(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("vehicle", "add", userFromCookie.Id))
            {
                string ui_vehicle_LicencePlateNum_add = context.Request.Params["ui_vehicle_LicencePlateNum_add"] ?? "";
                string ui_vehicle_Owner_add = context.Request.Params["ui_vehicle_Owner_add"] ?? "";
                string ui_vehicle_Brands_add = context.Request.Params["ui_vehicle_Brands_add"] ?? "";
                string ui_vehicle_CarModel_add = context.Request.Params["ui_vehicle_CarModel_add"] ?? "";
                string ui_vehicle_BuyPrice_add = context.Request.Params["ui_vehicle_BuyPrice_add"] ?? "";
                string ui_vehicle_BuyDate_add = context.Request.Params["ui_vehicle_BuyDate_add"] ?? "";
                string ui_vehicle_Remark_add = context.Request.Params["ui_vehicle_Remark_add"] ?? "";
                string ui_vehicle_Status_add = context.Request.Params["ui_vehicle_Status_add"] ?? "";

                DriveMgr.Model.VehicleModel vehicleAdd = new Model.VehicleModel();
                vehicleAdd.LicencePlateNum = ui_vehicle_LicencePlateNum_add.Trim();
                vehicleAdd.Owner = ui_vehicle_Owner_add.Trim();
                vehicleAdd.Brands = ui_vehicle_Brands_add.Trim();
                vehicleAdd.CarModel = ui_vehicle_CarModel_add.Trim();
                if (!string.IsNullOrEmpty(ui_vehicle_BuyPrice_add.Trim()))
                {
                    vehicleAdd.BuyPrice = decimal.Parse(ui_vehicle_BuyPrice_add);
                }
                if (!string.IsNullOrEmpty(ui_vehicle_BuyDate_add.Trim()))
                {
                    vehicleAdd.BuyDate = DateTime.Parse(ui_vehicle_BuyDate_add);
                }                
                vehicleAdd.Remark = ui_vehicle_Remark_add.Trim();
                vehicleAdd.Status = Int32.Parse(ui_vehicle_Status_add);

                vehicleAdd.CreateDate = DateTime.Now;
                vehicleAdd.CreatePerson = userFromCookie.UserId;
                vehicleAdd.UpdatePerson = userFromCookie.UserId;
                vehicleAdd.UpdateDate = DateTime.Now;

                if (vehicleBll.AddVehicle(vehicleAdd))
                {
                    userOperateLog.OperateInfo = "添加车辆信息";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "添加成功，车辆：" + ui_vehicle_LicencePlateNum_add.Trim();
                    context.Response.Write("{\"msg\":\"添加成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "添加车辆信息";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "添加失败";
                    context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "添加车辆信息";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 编辑车辆
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void EditVehicle(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("vehicle", "edit", userFromCookie.Id))
            {
                int id = Convert.ToInt32(context.Request.Params["id"]);
                DriveMgr.Model.VehicleModel vehicleEdit = vehicleBll.GetVehicleModel(id);
                string ui_vehicle_LicencePlateNum_edit = context.Request.Params["ui_vehicle_LicencePlateNum_edit"] ?? "";
                string ui_vehicle_Owner_edit = context.Request.Params["ui_vehicle_Owner_edit"] ?? "";
                string ui_vehicle_Brands_edit = context.Request.Params["ui_vehicle_Brands_edit"] ?? "";
                string ui_vehicle_CarModel_edit = context.Request.Params["ui_vehicle_CarModel_edit"] ?? "";
                string ui_vehicle_BuyPrice_edit = context.Request.Params["ui_vehicle_BuyPrice_edit"] ?? "";
                string ui_vehicle_BuyDate_edit = context.Request.Params["ui_vehicle_BuyDate_edit"] ?? "";
                string ui_vehicle_Remark_edit = context.Request.Params["ui_vehicle_Remark_edit"] ?? "";
                string ui_vehicle_Status_edit = context.Request.Params["ui_vehicle_Status_edit"] ?? "";

                vehicleEdit.LicencePlateNum = ui_vehicle_LicencePlateNum_edit.Trim();
                vehicleEdit.Owner = ui_vehicle_Owner_edit.Trim();
                vehicleEdit.Brands = ui_vehicle_Brands_edit.Trim();
                vehicleEdit.CarModel = ui_vehicle_CarModel_edit.Trim();
                if (!string.IsNullOrEmpty(ui_vehicle_BuyPrice_edit.Trim()))
                {
                    vehicleEdit.BuyPrice = decimal.Parse(ui_vehicle_BuyPrice_edit);
                }
                else
                {
                    vehicleEdit.BuyPrice = null;
                }
                if (!string.IsNullOrEmpty(ui_vehicle_BuyDate_edit.Trim()))
                {
                    vehicleEdit.BuyDate = DateTime.Parse(ui_vehicle_BuyDate_edit);
                }
                else
                {
                    vehicleEdit.BuyDate = null;
                }
                vehicleEdit.Remark = ui_vehicle_Remark_edit.Trim();
                vehicleEdit.Status = Int32.Parse(ui_vehicle_Status_edit);

                vehicleEdit.UpdatePerson = userFromCookie.UserId;
                vehicleEdit.UpdateDate = DateTime.Now;

                if (vehicleBll.UpdateVehicle(vehicleEdit))
                {
                    userOperateLog.OperateInfo = "修改车辆信息";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "修改成功，车辆信息主键：" + vehicleEdit.Id;
                    context.Response.Write("{\"msg\":\"修改成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "修改车辆信息";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "修改失败";
                    context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "修改车辆信息";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 删除车辆
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void DelVehicle(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("vehicle", "delete", userFromCookie.Id))
            {
                string ids = context.Request.Params["id"].Trim(',');
                if (vehicleBll.DeleteVehicleList(ids))
                {
                    userOperateLog.OperateInfo = "删除车辆信息";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "删除成功，车辆信息主键：" + ids;
                    context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "删除车辆信息";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "删除失败";
                    context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "删除车辆信息";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }
    }
}