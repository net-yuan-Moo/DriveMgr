/***********************
 @ author:zlong
 @ Date:2015-01-11
 @ Desc:车辆维护信息界面层
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
    /// bg_vehiclMaintenanceHandler 的摘要说明
    /// </summary>
    public class bg_vehiclMaintenanceHandler : IHttpHandler
    {
        DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
        VehiclMaintenanceBLL vehiclMaintenanceBll = new VehiclMaintenanceBLL();
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
                        SearchVehiclMaintenance(context);
                        break;
                    case "add":
                        AddVehiclMaintenance(userFromCookie, context);
                        break;
                    case "edit":
                        EditVehiclMaintenance(userFromCookie, context);
                        break;
                    case "delete":
                        DelVehiclMaintenance(userFromCookie, context);
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
        /// 查询车辆维护信息
        /// </summary>
        /// <param name="context"></param>
        private void SearchVehiclMaintenance(HttpContext context)
        {
            string sort = context.Request.Params["sort"];  //排序列
            string order = context.Request.Params["order"];  //排序方式 asc或者desc
            int pageindex = int.Parse(context.Request.Params["page"]);
            int pagesize = int.Parse(context.Request.Params["rows"]);

            string ui_vehiclMaintenance_LicencePlateNum = context.Request.Params["ui_vehiclMaintenance_LicencePlateNum"] ?? "";
            string ui_vehiclMaintenance_MaintenanceType = context.Request.Params["ui_vehiclMaintenance_MaintenanceType"] ?? "";
            string ui_vehiclMaintenance_createStartDate = context.Request.Params["ui_vehiclMaintenance_createStartDate"] ?? "";
            string ui_vehiclMaintenance_createEndDate = context.Request.Params["ui_vehiclMaintenance_createEndDate"] ?? "";

            int totalCount;   //输出参数
            string strJson = vehiclMaintenanceBll.GetPagerData(ui_vehiclMaintenance_LicencePlateNum, ui_vehiclMaintenance_MaintenanceType, ui_vehiclMaintenance_createStartDate, ui_vehiclMaintenance_createEndDate, sort + " " + order, pagesize, pageindex, out totalCount);
            context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

            userOperateLog.OperateInfo = "查询车辆维护信息";
            userOperateLog.IfSuccess = true;
            userOperateLog.Description = "排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 添加车辆维护信息
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void AddVehiclMaintenance(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("vehiclMaintenance", "add", userFromCookie.Id))
            {
                string ui_vehiclMaintenance_LicencePlateNum_add = context.Request.Params["ui_vehiclMaintenance_LicencePlateNum_add"] ?? "";
                string ui_vehiclMaintenance_MaintenanceType_add = context.Request.Params["ui_vehiclMaintenance_MaintenanceType_add"] ?? "";
                string ui_vehiclMaintenance_MaintenPerson_add = context.Request.Params["ui_vehiclMaintenance_MaintenPerson_add"] ?? "";
                string ui_vehiclMaintenance_MaintenCosts_add = context.Request.Params["ui_vehiclMaintenance_MaintenCosts_add"] ?? "";
                string ui_vehiclMaintenance_MaintenDate_add = context.Request.Params["ui_vehiclMaintenance_MaintenDate_add"] ?? "";
                string ui_vehiclMaintenance_Remark_add = context.Request.Params["ui_vehiclMaintenance_Remark_add"] ?? "";

                DriveMgr.Model.VehiclMaintenanceModel vehiclMaintenanceAdd = new Model.VehiclMaintenanceModel();
                vehiclMaintenanceAdd.VehicleId = Int32.Parse(ui_vehiclMaintenance_LicencePlateNum_add.Trim());
                vehiclMaintenanceAdd.MaintenanceType = Int32.Parse(ui_vehiclMaintenance_MaintenanceType_add.Trim());
                vehiclMaintenanceAdd.MaintenPerson = ui_vehiclMaintenance_MaintenPerson_add.Trim();
                vehiclMaintenanceAdd.MaintenCosts = decimal.Parse(ui_vehiclMaintenance_MaintenCosts_add);
                vehiclMaintenanceAdd.MaintenDate = DateTime.Parse(ui_vehiclMaintenance_MaintenDate_add);
                vehiclMaintenanceAdd.Remark = ui_vehiclMaintenance_Remark_add.Trim();

                vehiclMaintenanceAdd.CreateDate = DateTime.Now;
                vehiclMaintenanceAdd.CreatePerson = userFromCookie.UserId;
                vehiclMaintenanceAdd.UpdatePerson = userFromCookie.UserId;
                vehiclMaintenanceAdd.UpdateDate = DateTime.Now;

                if (vehiclMaintenanceBll.AddVehiclMaintenance(vehiclMaintenanceAdd))
                {
                    userOperateLog.OperateInfo = "添加车辆维护信息";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "添加成功，车辆维护信息：" + ui_vehiclMaintenance_LicencePlateNum_add.Trim();
                    context.Response.Write("{\"msg\":\"添加成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "添加车辆维护信息";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "添加失败";
                    context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "添加车辆维护信息";
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
        private void EditVehiclMaintenance(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("vehiclMaintenance", "edit", userFromCookie.Id))
            {
                int id = Convert.ToInt32(context.Request.Params["id"]);
                DriveMgr.Model.VehiclMaintenanceModel vehiclMaintenanceEdit = vehiclMaintenanceBll.GetVehiclMaintenanceModel(id);
                string ui_vehiclMaintenance_LicencePlateNum_edit = context.Request.Params["ui_vehiclMaintenance_LicencePlateNum_edit"] ?? "";
                string ui_vehiclMaintenance_MaintenanceType_edit = context.Request.Params["ui_vehiclMaintenance_MaintenanceType_edit"] ?? "";
                string ui_vehiclMaintenance_MaintenPerson_edit = context.Request.Params["ui_vehiclMaintenance_MaintenPerson_edit"] ?? "";
                string ui_vehiclMaintenance_MaintenCosts_edit = context.Request.Params["ui_vehiclMaintenance_MaintenCosts_edit"] ?? "";
                string ui_vehiclMaintenance_MaintenDate_edit = context.Request.Params["ui_vehiclMaintenance_MaintenDate_edit"] ?? "";
                string ui_vehiclMaintenance_Remark_edit = context.Request.Params["ui_vehiclMaintenance_Remark_edit"] ?? "";

                vehiclMaintenanceEdit.VehicleId = Int32.Parse(ui_vehiclMaintenance_LicencePlateNum_edit.Trim());
                vehiclMaintenanceEdit.MaintenanceType = Int32.Parse(ui_vehiclMaintenance_MaintenanceType_edit.Trim());
                vehiclMaintenanceEdit.MaintenPerson = ui_vehiclMaintenance_MaintenPerson_edit.Trim();
                vehiclMaintenanceEdit.MaintenCosts = decimal.Parse(ui_vehiclMaintenance_MaintenCosts_edit);
                vehiclMaintenanceEdit.MaintenDate = DateTime.Parse(ui_vehiclMaintenance_MaintenDate_edit);
                vehiclMaintenanceEdit.Remark = ui_vehiclMaintenance_Remark_edit.Trim();

                vehiclMaintenanceEdit.UpdatePerson = userFromCookie.UserId;
                vehiclMaintenanceEdit.UpdateDate = DateTime.Now;

                if (vehiclMaintenanceBll.UpdateVehiclMaintenance(vehiclMaintenanceEdit))
                {
                    userOperateLog.OperateInfo = "修改车辆维护信息";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "修改成功，车辆维护信息主键：" + vehiclMaintenanceEdit.Id;
                    context.Response.Write("{\"msg\":\"修改成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "修改车辆维护信息";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "修改失败";
                    context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "修改车辆维护信息";
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
        private void DelVehiclMaintenance(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("vehiclMaintenance", "delete", userFromCookie.Id))
            {
                string ids = context.Request.Params["id"].Trim(',');
                if (vehiclMaintenanceBll.DeleteVehiclMaintenanceList(ids))
                {
                    userOperateLog.OperateInfo = "删除车辆维护信息";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "删除成功，车辆维护信息主键：" + ids;
                    context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "删除车辆维护信息";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "删除失败";
                    context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "删除车辆维护信息";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }
    }
}