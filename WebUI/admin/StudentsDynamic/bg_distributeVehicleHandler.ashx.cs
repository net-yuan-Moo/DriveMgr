using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DriveMgr.WebUI.admin.StudentsDynamic
{
    /// <summary>
    /// bg_distributeVehicleHandler 的摘要说明
    /// </summary>
    public class bg_distributeVehicleHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = context.Request.Params["action"];
            DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
            try
            {
                DriveMgr.Model.User userFromCookie = DriveMgr.Common.UserHelper.GetUser(context);   //获取cookie里的用户对象
                userOperateLog = new Model.UserOperateLog();
                userOperateLog.UserIp = context.Request.UserHostAddress;
                userOperateLog.UserName = userFromCookie.UserId;
                switch (action)
                {
                    case "search":
                        string strWhere = "1=1";
                        string sort = context.Request.Params["sort"];  //排序列
                        string order = context.Request.Params["order"];  //排序方式 asc或者desc
                        int pageindex = int.Parse(context.Request.Params["page"]);
                        int pagesize = int.Parse(context.Request.Params["rows"]);

                        string ui_distributeVehicle_name = context.Request.Params["ui_distributeVehicle_name"] ?? "";
                        string ui_distributeVehicle_code = context.Request.Params["ui_distributeVehicle_code"] ?? "";
                        string ui_distributeVehicle_vehicle = context.Request.Params["ui_distributeVehicle_vehicle"] ?? "";

                        string subjectID = context.Request.Params["subject"]?? "";

                        strWhere += " and flag=1 and PeriodsID = (SELECT TOP 1 CurrentPeroidID FROM tb_CurrentPeroid)";
                        if (subjectID.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(subjectID))   //防止sql注入
                            strWhere += string.Format(" and SubjectID = '{0}'", subjectID.Trim());
                        if (ui_distributeVehicle_name.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_distributeVehicle_name))   //防止sql注入
                            strWhere += string.Format(" and StudentsName like '%{0}%'", ui_distributeVehicle_name.Trim());
                        if (ui_distributeVehicle_code.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_distributeVehicle_code))
                            strWhere += string.Format(" and StudentCode like '%{0}%'", ui_distributeVehicle_code.Trim());
                        if (ui_distributeVehicle_vehicle.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_distributeVehicle_vehicle))
                            strWhere += string.Format(" and VehicleID={0}", ui_distributeVehicle_vehicle.Trim());



                        int totalCount;   //输出参数
                        string strJson = new DriveMgr.BLL.ScoresBLL().GetPager("V_DistributeVehicle", "*", sort + " " + order, pagesize, pageindex, strWhere, out totalCount);
                        context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

                        userOperateLog.OperateInfo = "查询分配车辆";
                        userOperateLog.IfSuccess = true;
                        userOperateLog.Description = "查询条件：" + strWhere + " 排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "autoDistributeVehicle": //自动分配车辆
                        if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("distributeVehicle", "autoDistributeVehicle", userFromCookie.Id))
                        {
                            int subjectId = Int32.Parse(context.Request.Params["subject"]);
                            string distributeVehicle = new DriveMgr.BLL.VehicleBLL().AddDistributeVehicle(subjectId,userFromCookie.UserName);
                            userOperateLog.OperateInfo = "科目"+subjectId+"自动分配车辆";
                            userOperateLog.IfSuccess = true;
                            userOperateLog.Description = distributeVehicle;

                            context.Response.Write("{\"msg\":\"" + distributeVehicle + "\",\"success\":true}");
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "自动分配车辆";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "edit":
                        if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("distributeVehicle", "edit", userFromCookie.Id))
                        {
                            int id = Convert.ToInt32(context.Request.Params["ui_distributeVehicleid_edit"]);
                            int ui_distributeVehicle_vehicle_edit = Int32.Parse(context.Request.Params["ui_distributeVehicle_vehicle_edit"] ?? "0");
                            int subjectId = Int32.Parse(context.Request.Params["subject"]);

                            DriveMgr.Model.DistributionVehicleModel distributeVehicleEdit = new Model.DistributionVehicleModel();
                            distributeVehicleEdit.ID = id;
                            distributeVehicleEdit.VehicleID = ui_distributeVehicle_vehicle_edit;
                            distributeVehicleEdit.SubjectID = subjectId;
                            distributeVehicleEdit.CreateTime = DateTime.Now;
                            distributeVehicleEdit.Operater = userFromCookie.UserName;
                            distributeVehicleEdit.DistributeVihicleStatus = 1; //已分配
                            

                            if (new DriveMgr.BLL.VehicleBLL().EditDistributeStudents(distributeVehicleEdit))
                            {
                                userOperateLog.OperateInfo = "修改分配车辆信息";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "修改成功，分配车辆主键：" + distributeVehicleEdit.ID;
                                context.Response.Write("{\"msg\":\"修改分配车辆信息成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "修改分配车辆信息";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "修改失败";
                                context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "修改分配车辆信息";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    default:
                        context.Response.Write("{\"msg\":\"参数错误！\",\"success\":false}");
                        break;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"msg\":\"" + DriveMgr.Common.JsonHelper.StringFilter(ex.Message) + "\",\"success\":false}");
                userOperateLog.OperateInfo = "分配车辆功能异常";
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