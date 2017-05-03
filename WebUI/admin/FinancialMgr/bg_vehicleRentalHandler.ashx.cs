/***********************
 @ author:zlong
 @ Date:2015-01-14
 @ Desc:车辆出租信息界面层
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
    /// bg_vehicleRentalHandler 的摘要说明
    /// </summary>
    public class bg_vehicleRentalHandler : IHttpHandler
    {
        DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
        VehicleRentalBLL vehicleRentalBll = new VehicleRentalBLL();
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
                        SearchVehicleRental(context);
                        break;
                    case "add":
                        AddVehicleRental(userFromCookie, context);
                        break;
                    case "edit":
                        EditVehicleRental(userFromCookie, context);
                        break;
                    case "delete":
                        DelVehicleRental(userFromCookie, context);
                        break;
                    case "getPriceConfigDT":
                        PriceConfigBLL priceConfigBll = new PriceConfigBLL();
                        context.Response.Write(priceConfigBll.GetPriceConfigDT(0));
                        break;
                    case "getStudentDT":                        
                        context.Response.Write(vehicleRentalBll.GetStudentDT());
                        break;
                    case "getCoachDT":
                        context.Response.Write(vehicleRentalBll.GetCoachDT());
                        break;
                    case "setTotalPrice":
                        string priceConfigID = context.Request.Params["priceConfigID"] ?? "";
                        string longer = context.Request.Params["longer"] ?? "";
                        if (priceConfigID != string.Empty)
                        {
                            PriceConfigBLL configBll = new PriceConfigBLL();
                            Model.PriceConfigModel model = configBll.GetPriceConfigModel(Int32.Parse(priceConfigID));
                            try
                            {
                                decimal aaa = model.Price.Value * decimal.Parse(longer);
                                context.Response.Write("{\"totalPrice\":" + model.Price.Value * decimal.Parse(longer) + "}");
                            }
                            catch (System.Exception ex)
                            {
                                context.Response.Write("{\"totalPrice\":\"0\"}");
                            }                            
                        }
                        else
                        {
                            context.Response.Write("{\"totalPrice\":\"0\"}");
                        }
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
        /// 查询车辆出租信息
        /// </summary>
        /// <param name="context"></param>
        private void SearchVehicleRental(HttpContext context)
        {
            string sort = context.Request.Params["sort"];  //排序列
            string order = context.Request.Params["order"];  //排序方式 asc或者desc
            int pageindex = int.Parse(context.Request.Params["page"]);
            int pagesize = int.Parse(context.Request.Params["rows"]);

            string ui_vehicleRental_studentName = context.Request.Params["ui_vehicleRental_studentName"] ?? "";
            //string ui_vehicleRental_coachName = context.Request.Params["ui_vehicleRental_coachName"] ?? "";
            string ui_vehicleRental_licencePlateNum = context.Request.Params["ui_vehicleRental_licencePlateNum"] ?? "";
            string ui_vehicleRental_priceConfig = context.Request.Params["ui_vehicleRental_priceConfig"] ?? "";
            string ui_vehicleRental_createStartDate = context.Request.Params["ui_vehicleRental_createStartDate"] ?? "";
            string ui_vehicleRental_createEndDate = context.Request.Params["ui_vehicleRental_createEndDate"] ?? "";

            int totalCount;   //输出参数
            string strJson = vehicleRentalBll.GetPagerData(ui_vehicleRental_studentName, ui_vehicleRental_licencePlateNum, ui_vehicleRental_priceConfig, ui_vehicleRental_createStartDate, ui_vehicleRental_createEndDate, sort + " " + order, pagesize, pageindex, out totalCount);
            context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

            userOperateLog.OperateInfo = "查询车辆出租信息";
            userOperateLog.IfSuccess = true;
            userOperateLog.Description = "排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 添加车辆出租信息
        /// </summary>
        /// <param name="userFromCookie"></param>
        /// <param name="context"></param>
        private void AddVehicleRental(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("vehicleRental", "add", userFromCookie.Id))
            {
                string ui_vehicleRental_StudentName_add = context.Request.Params["ui_vehicleRental_StudentName_add"] ?? "";
                //string ui_vehicleRental_StudentCode_add = context.Request.Params["ui_vehicleRental_StudentCode_add"] ?? "";
                //string ui_vehicleRental_CoachName_add = context.Request.Params["ui_vehicleRental_CoachName_add"] ?? "";
                string ui_vehicleRental_RentDate_add = context.Request.Params["ui_vehicleRental_RentDate_add"] ?? "";
                string ui_vehicleRental_LicencePlateNum_add = context.Request.Params["ui_vehicleRental_LicencePlateNum_add"] ?? "";
                string ui_vehicleRental_PriceConfig_add = context.Request.Params["ui_vehicleRental_PriceConfig_add"] ?? "";
                string ui_vehicleRental_Longer_add = context.Request.Params["ui_vehicleRental_Longer_add"] ?? "";
                string ui_vehicleRental_TotalPrice_add = context.Request.Params["ui_vehicleRental_TotalPrice_add"] ?? "";
                string ui_vehicleRental_Remark_add = context.Request.Params["ui_vehicleRental_Remark_add"] ?? "";

                DriveMgr.Model.VehicleRentalModel vehicleRentalAdd = new Model.VehicleRentalModel();
                //vehicleRentalAdd.StudentCode = ui_vehicleRental_StudentName_add.Trim();
                vehicleRentalAdd.StudentName = ui_vehicleRental_StudentName_add.Trim();
                //vehicleRentalAdd.CoachName = ui_vehicleRental_CoachName_add.Trim();
                vehicleRentalAdd.VehicleId = Int32.Parse(ui_vehicleRental_LicencePlateNum_add.Trim());
                vehicleRentalAdd.PriceConfigID = Int32.Parse(ui_vehicleRental_PriceConfig_add.Trim());
                vehicleRentalAdd.Longer = decimal.Parse(ui_vehicleRental_Longer_add.Trim());
                vehicleRentalAdd.TotalPrice = decimal.Parse(ui_vehicleRental_TotalPrice_add.Trim());
                vehicleRentalAdd.RentDate = DateTime.Parse(ui_vehicleRental_RentDate_add.Trim());
                vehicleRentalAdd.Remark = ui_vehicleRental_Remark_add.Trim();

                vehicleRentalAdd.CreateDate = DateTime.Now;
                vehicleRentalAdd.CreatePerson = userFromCookie.UserId;
                vehicleRentalAdd.UpdatePerson = userFromCookie.UserId;
                vehicleRentalAdd.UpdateDate = DateTime.Now;

                if (vehicleRentalBll.AddVehicleRental(vehicleRentalAdd))
                {
                    userOperateLog.OperateInfo = "添加车辆出租信息";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "添加成功，车辆出租信息：" + ui_vehicleRental_LicencePlateNum_add.Trim();
                    context.Response.Write("{\"msg\":\"添加成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "添加车辆出租信息";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "添加失败";
                    context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "添加车辆出租信息";
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
        private void EditVehicleRental(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("vehicleRental", "edit", userFromCookie.Id))
            {
                int id = Convert.ToInt32(context.Request.Params["id"]);
                DriveMgr.Model.VehicleRentalModel vehicleRentalEdit = vehicleRentalBll.GetVehicleRentalModel(id);
                string ui_vehicleRental_StudentName_edit = context.Request.Params["ui_vehicleRental_StudentName_edit"] ?? "";
                //string ui_vehicleRental_StudentCode_edit = context.Request.Params["ui_vehicleRental_StudentCode_edit"] ?? "";
                //string ui_vehicleRental_CoachName_edit = context.Request.Params["ui_vehicleRental_CoachName_edit"] ?? "";
                string ui_vehicleRental_RentDate_edit = context.Request.Params["ui_vehicleRental_RentDate_edit"] ?? "";
                string ui_vehicleRental_LicencePlateNum_edit = context.Request.Params["ui_vehicleRental_LicencePlateNum_edit"] ?? "";
                string ui_vehicleRental_PriceConfig_edit = context.Request.Params["ui_vehicleRental_PriceConfig_edit"] ?? "";
                string ui_vehicleRental_Longer_edit = context.Request.Params["ui_vehicleRental_Longer_edit"] ?? "";
                string ui_vehicleRental_TotalPrice_edit = context.Request.Params["ui_vehicleRental_TotalPrice_edit"] ?? "";
                string ui_vehicleRental_Remark_edit = context.Request.Params["ui_vehicleRental_Remark_edit"] ?? "";

                vehicleRentalEdit.StudentName = ui_vehicleRental_StudentName_edit.Trim();
                //vehicleRentalEdit.StudentCode = ui_vehicleRental_StudentName_edit.Trim();
                //vehicleRentalEdit.CoachName = ui_vehicleRental_CoachName_edit.Trim();                
                vehicleRentalEdit.VehicleId = Int32.Parse(ui_vehicleRental_LicencePlateNum_edit.Trim());
                vehicleRentalEdit.PriceConfigID = Int32.Parse(ui_vehicleRental_PriceConfig_edit.Trim());
                vehicleRentalEdit.Longer = decimal.Parse(ui_vehicleRental_Longer_edit.Trim());
                vehicleRentalEdit.TotalPrice = decimal.Parse(ui_vehicleRental_TotalPrice_edit.Trim());
                vehicleRentalEdit.RentDate = DateTime.Parse(ui_vehicleRental_RentDate_edit.Trim());
                vehicleRentalEdit.Remark = ui_vehicleRental_Remark_edit.Trim();

                vehicleRentalEdit.UpdatePerson = userFromCookie.UserId;
                vehicleRentalEdit.UpdateDate = DateTime.Now;

                if (vehicleRentalBll.UpdateVehicleRental(vehicleRentalEdit))
                {
                    userOperateLog.OperateInfo = "修改车辆出租信息";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "修改成功，车辆出租信息主键：" + vehicleRentalEdit.Id;
                    context.Response.Write("{\"msg\":\"修改成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "修改车辆出租信息";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "修改失败";
                    context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "修改车辆出租信息";
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
        private void DelVehicleRental(DriveMgr.Model.User userFromCookie, HttpContext context)
        {
            if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("vehicleRental", "delete", userFromCookie.Id))
            {
                string ids = context.Request.Params["id"].Trim(',');
                if (vehicleRentalBll.DeleteVehicleRentalList(ids))
                {
                    userOperateLog.OperateInfo = "删除车辆出租信息";
                    userOperateLog.IfSuccess = true;
                    userOperateLog.Description = "删除成功，车辆出租信息主键：" + ids;
                    context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                }
                else
                {
                    userOperateLog.OperateInfo = "删除车辆出租信息";
                    userOperateLog.IfSuccess = false;
                    userOperateLog.Description = "删除失败";
                    context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                }
            }
            else
            {
                userOperateLog.OperateInfo = "删除车辆出租信息";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = "无权限，请联系管理员";
                context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
            }
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }
    }
}