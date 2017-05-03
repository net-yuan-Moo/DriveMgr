using Aspose.Words;
using Aspose.Words.Drawing;
using DriveMgr.Common;
using DriveMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace DriveMgr.WebUI.admin.StudentsMgr
{
    /// <summary>
    /// bg_registrationHandler 的摘要说明
    /// </summary>
    public class bg_registrationHandler : IHttpHandler
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


                        string ui_students_name = context.Request.Params["ui_students_name"] ?? "";
                        string ui_students_code = context.Request.Params["ui_students_code"] ?? "";
                        string ui_students_cardnum = context.Request.Params["ui_students_cardnum"] ?? "";
                        string ui_students_status = context.Request.Params["ui_students_status"] ?? "";
                        string ui_pay_status = context.Request.Params["ui_pay_status"] ?? "";
                        string ui_students_period = context.Request.Params["ui_students_period"] ?? "";


                        //string ui_user_userid = context.Request.Params["ui_user_userid"] ?? "";
                        //string ui_user_username = context.Request.Params["ui_user_username"] ?? "";
                        //string ui_user_isable = context.Request.Params["ui_user_isable"] ?? "";
                        //string ui_user_ifchangepwd = context.Request.Params["ui_user_ifchangepwd"] ?? "";
                        //string ui_user_description = context.Request.Params["ui_user_description"] ?? "";
                        //string ui_user_adddatestart = context.Request.Params["ui_user_adddatestart"] ?? "";
                        //string ui_user_adddateend = context.Request.Params["ui_user_adddateend"] ?? "";
                        strWhere += " and flag=1";
                        if (ui_students_name.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_students_name))   //防止sql注入
                            strWhere += string.Format(" and StudentsName like '%{0}%'", ui_students_name.Trim());
                        if (ui_students_code.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_students_code))
                            strWhere += string.Format(" and StudentCode like '%{0}%'", ui_students_code.Trim());
                        if (ui_students_cardnum.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_students_cardnum))
                            strWhere += string.Format(" and CardNum like '%{0}%'", ui_students_cardnum.Trim());
                        if (ui_students_status.Trim() != "select" && ui_students_status.Trim() != "")
                            strWhere += " and Status = '" + ui_students_status.Trim() + "'";
                        if (ui_pay_status.Trim() != "select" && ui_pay_status.Trim() != "")
                            strWhere += " and PayStatus = '" + ui_pay_status.Trim() + "'";
                        if (ui_students_period.Trim() != "select" && ui_students_period.Trim() != "")
                            strWhere += " and PeriodsID = '" + ui_students_period.Trim() + "'";


                        int totalCount;   //输出参数
                        string strJson = new DriveMgr.BLL.RegistrationBLL().GetPager("V_StudentsBaseData", "*", sort + " " + order, pagesize, pageindex, strWhere, out totalCount);
                        context.Response.Write("{\"total\": " + totalCount.ToString() + ",\"rows\":" + strJson + "}");

                        userOperateLog.OperateInfo = "查询学员";
                        userOperateLog.IfSuccess = true;
                        userOperateLog.Description = "查询条件：" + strWhere + " 排序：" + sort + " " + order + " 页码/每页大小：" + pageindex + " " + pagesize;
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "add":
                        if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("registration", "add", userFromCookie.Id))
                        {
                            string ui_registration_name_add = context.Request.Params["ui_registration_name_add"] ?? "";
                            string ui_registration_cardnum_add = context.Request.Params["ui_registration_cardnum_add"] ?? "";
                            int ui_registration_age_add = Int32.Parse(context.Request.Params["ui_registration_age_add"] ?? "-1");
                            bool ui_registration_sex_add = context.Request.Params["ui_registration_sex_add"] == "0" ? false : true;
                            bool ui_registration_islocal_add = context.Request.Params["ui_registration_islocal_add"] == "0" ? false : true;
                            int ui_registration_period_add = Int32.Parse(context.Request.Params["ui_registration_period_add"] ?? "-1");
                            string ui_registration_address_add = context.Request.Params["ui_registration_address_add"] ?? "";
                            string ui_registration_remark_add = context.Request.Params["ui_registration_remark_add"] ?? "";
                            string fupPic_add = context.Request.Params["fupPic_add"] ?? "";
                            string ui_registration_phoneNum_add = context.Request.Params["ui_registration_phoneNum_add"] ?? "";

                            DriveMgr.Model.RegistrationModel registrationAdd = new Model.RegistrationModel();
                            registrationAdd.StudentsName = ui_registration_name_add;
                            registrationAdd.CardNum = ui_registration_cardnum_add;
                            registrationAdd.Age = ui_registration_age_add;
                            registrationAdd.Sex = ui_registration_sex_add;
                            registrationAdd.IsLocal = ui_registration_islocal_add;
                            registrationAdd.PeriodsID = ui_registration_period_add;
                            registrationAdd.Address = ui_registration_address_add;
                            registrationAdd.Remark = ui_registration_remark_add;
                            registrationAdd.Status = 0; //【0：在学 1：毕业 2：退学】
                            if (fupPic_add.Trim() == "")
                            {
                                registrationAdd.PicPath = "";
                            }
                            else
                            {
                                registrationAdd.PicPath = "/images/Pictures/" + fupPic_add;
                            }
                            registrationAdd.PhoneNum = ui_registration_phoneNum_add;

                            FormsIdentity id = (FormsIdentity)context.User.Identity;
                            FormsAuthenticationTicket tickets = id.Ticket;

                            //获取票证里序列化的用户对象（反序列化）
                            DriveMgr.Model.User userCheck = new JavaScriptSerializer().Deserialize<DriveMgr.Model.User>(tickets.UserData);

                            registrationAdd.Operater = userCheck.UserName;
                            DriveMgr.BLL.RegistrationBLL bll = new DriveMgr.BLL.RegistrationBLL();
                            bool result = bll.Add(registrationAdd);
                            if (result)
                            {
                                //RegistrationModel mod = bll.GetModel(result);
                                //string changeMsg = String.Empty;
                                //try
                                //{
                                //    string srcPath = context.Server.MapPath(mod.PicPath);
                                //    string extension = Path.GetFileNameWithoutExtension(srcPath);
                                //    string desPath = context.Server.MapPath("/admin/images/Pictures/" + mod.CardNum + ".jpg");
                                //    bool changeResult = FileHelpercs.ChangeFileName(srcPath, desPath);

                                //    if (changeResult)
                                //    {
                                //        changeMsg = "改变照片名称成功！从" + srcPath + "到" + desPath;
                                //    }
                                //    else
                                //    {
                                //        changeMsg = "改变照片名称失败！从" + srcPath + "到" + desPath;
                                //    }
                                //}
                                //catch
                                //{
                                //    changeMsg = "改变照片名称失败！";
                                //}

                                userOperateLog.OperateInfo = "添加学员";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "添加成功，学员" + ui_registration_name_add;
                                context.Response.Write("{\"msg\":\"添加学员成功！" + "\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "添加学员";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "添加失败";
                                context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "添加学员";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "edit":
                        if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("registration", "edit", userFromCookie.Id))
                        {
                            int id = Convert.ToInt32(context.Request.Params["id"]);
                            string ui_registration_name_edit = context.Request.Params["ui_registration_name_edit"] ?? "";
                            string ui_registration_cardnum_edit = context.Request.Params["ui_registration_cardnum_edit"] ?? "";
                            int ui_registration_age_edit = Int32.Parse(context.Request.Params["ui_registration_age_edit"] ?? "-1");
                            bool ui_registration_sex_edit = context.Request.Params["ui_registration_sex_edit"] == "0" ? false : true;
                            bool ui_registration_islocal_edit = context.Request.Params["ui_registration_islocal_edit"] == "0" ? false : true;
                            int ui_registration_period_edit = Int32.Parse(context.Request.Params["ui_registration_period_edit"] ?? "-1");
                            string ui_registration_address_edit = context.Request.Params["ui_registration_address_edit"] ?? "";
                            string ui_registration_remark_edit = context.Request.Params["ui_registration_remark_edit"] ?? "";
                            string ui_registration_phoneNum_edit = context.Request.Params["ui_registration_phoneNum_edit"] ?? "";

                            DriveMgr.Model.RegistrationModel registrationedit = new Model.RegistrationModel();
                            registrationedit.ID = id;
                            registrationedit.StudentsName = ui_registration_name_edit;
                            registrationedit.CardNum = ui_registration_cardnum_edit;
                            registrationedit.Age = ui_registration_age_edit;
                            registrationedit.Sex = ui_registration_sex_edit;
                            registrationedit.IsLocal = ui_registration_islocal_edit;
                            registrationedit.PeriodsID = ui_registration_period_edit;
                            registrationedit.Address = ui_registration_address_edit;
                            registrationedit.Remark = ui_registration_remark_edit;
                            //registrationedit.Status = 0; //【0：在学 1：毕业 2：退学】
                            registrationedit.PhoneNum = ui_registration_phoneNum_edit;  //手机号码

                            FormsIdentity iid = (FormsIdentity)context.User.Identity;
                            FormsAuthenticationTicket tickets = iid.Ticket;

                            //获取票证里序列化的用户对象（反序列化）
                            DriveMgr.Model.User userCheck = new JavaScriptSerializer().Deserialize<DriveMgr.Model.User>(tickets.UserData);

                            registrationedit.Operater = userCheck.UserName;

                            if (new DriveMgr.BLL.RegistrationBLL().Update(registrationedit))
                            {
                                userOperateLog.OperateInfo = "修改学员";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "修改成功，学员主键：" + registrationedit.ID;
                                context.Response.Write("{\"msg\":\"修改成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "修改学员";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "修改失败";
                                context.Response.Write("{\"msg\":\"修改失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "修改学员";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "delete":
                        if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("registration", "delete", userFromCookie.Id))
                        {
                            string ids = context.Request.Params["id"].Trim(',');
                            if (new DriveMgr.BLL.RegistrationBLL().DeleteList(ids))
                            {
                                userOperateLog.OperateInfo = "删除学员";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "删除成功，学员主键：" + ids;
                                context.Response.Write("{\"msg\":\"删除成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "删除学员";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "删除失败";
                                context.Response.Write("{\"msg\":\"删除失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "删除学员";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "bindPeroid":
                        DataTable dt = new DriveMgr.BLL.RegistrationBLL().BindPeroid();
                        string peroidResult = NewtonJsonHelper.ToJson(dt);
                        context.Response.Write(peroidResult);
                        break;
                    case "pay":
                        if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("registration", "paytuition", userFromCookie.Id))
                        {
                            bool payResult = DoTuition(context);
                            if (payResult)
                            {
                                userOperateLog.OperateInfo = "缴纳学费";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "交费成功。";
                                context.Response.Write("{\"msg\":\"交费成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "缴纳学费";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "交费失败";
                                context.Response.Write("{\"msg\":\"交费失败！\",\"success\":false}");
                            }
                        }
                        break;
                    case "payExam":
                        if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("registration", "payExam", userFromCookie.Id))
                        {
                            bool payResult = PayExam(context);
                            if (payResult)
                            {
                                userOperateLog.OperateInfo = "缴纳考试费";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "交费成功。";
                                context.Response.Write("{\"msg\":\"交考试费成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "缴纳考试费";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "交考试费失败";
                                context.Response.Write("{\"msg\":\"交考试费失败！\",\"success\":false}");
                            }
                        }
                        break;
                    case "exit":
                        if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("registration", "exit", userFromCookie.Id))
                        {
                            bool exitResult = DropOut(context);
                            if (exitResult)
                            {
                                userOperateLog.OperateInfo = "退学";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "退学成功。";
                                context.Response.Write("{\"msg\":\"退学成功！\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "退学";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "退学失败";
                                context.Response.Write("{\"msg\":\"退学失败！\",\"success\":false}");
                            }
                        }
                        break;
                    case "getInfoByCard":
                        string infoResult = GetInfoByCard(context);
                        context.Response.Write(infoResult);
                        break;
                    case "uploadPic":
                        string uploadResult = UploadPic(context);
                        context.Response.Write(uploadResult);
                        break;
                    case "printApplyTable":
                        string printResult = PrintApplyTable(context);
                        context.Response.Write(printResult);
                        break;
                    case "exportStudents":
                        string exportResult = DownloadExcel(context);
                        context.Response.Write(exportResult);
                        break;
                    case "addByCard":
                        if (userFromCookie != null && new DriveMgr.BLL.Authority().IfAuthority("registration", "byCard", userFromCookie.Id))
                        {
                            string ui_registration_name_byCard = context.Request.Params["ui_registration_name_byCard"] ?? "";
                            string ui_registration_cardnum_byCard = context.Request.Params["ui_registration_cardnum_byCard"] ?? "";
                            int ui_registration_age_byCard = Int32.Parse(context.Request.Params["ui_registration_age_byCard"] ?? "-1");
                            bool ui_registration_sex_byCard = context.Request.Params["ui_registration_sex_byCard"] == "0" ? false : true;
                            bool ui_registration_islocal_byCard = context.Request.Params["ui_registration_islocal_byCard"] == "0" ? false : true;
                            int ui_registration_period_byCard = Int32.Parse(context.Request.Params["ui_registration_period_byCard"] ?? "-1");
                            string ui_registration_address_byCard = context.Request.Params["ui_registration_address_byCard"] ?? "";
                            string ui_registration_remark_byCard = context.Request.Params["ui_registration_remark_byCard"] ?? "";
                            string ui_registration_phoneNum_byCard = context.Request.Params["ui_registration_phoneNum_byCard"] ?? "";

                            string fupPic_byCard = context.Request.Params["picPath"] ?? "";

                            DriveMgr.Model.RegistrationModel registrationAdd = new Model.RegistrationModel();
                            registrationAdd.StudentsName = ui_registration_name_byCard;
                            registrationAdd.CardNum = ui_registration_cardnum_byCard;
                            registrationAdd.Age = ui_registration_age_byCard;
                            registrationAdd.Sex = ui_registration_sex_byCard;
                            registrationAdd.IsLocal = ui_registration_islocal_byCard;
                            registrationAdd.PeriodsID = ui_registration_period_byCard;
                            registrationAdd.Address = ui_registration_address_byCard;
                            registrationAdd.Remark = ui_registration_remark_byCard;
                            registrationAdd.Status = 0; //【0：在学 1：毕业 2：退学】
                            if (fupPic_byCard.Replace("/images/Pictures/", "").Trim() == "")
                            {
                                registrationAdd.PicPath = "";
                            }
                            else
                            {
                                registrationAdd.PicPath = fupPic_byCard;
                            }
                            registrationAdd.PhoneNum = ui_registration_phoneNum_byCard;

                            FormsIdentity id = (FormsIdentity)context.User.Identity;
                            FormsAuthenticationTicket tickets = id.Ticket;

                            //获取票证里序列化的用户对象（反序列化）
                            DriveMgr.Model.User userCheck = new JavaScriptSerializer().Deserialize<DriveMgr.Model.User>(tickets.UserData);

                            registrationAdd.Operater = userCheck.UserName;
                            DriveMgr.BLL.RegistrationBLL bll = new DriveMgr.BLL.RegistrationBLL();
                            bool result = bll.Add(registrationAdd);
                            if (result)
                            {
                                userOperateLog.OperateInfo = "刷卡添加学员";
                                userOperateLog.IfSuccess = true;
                                userOperateLog.Description = "添加成功，学员" + ui_registration_name_byCard;
                                context.Response.Write("{\"msg\":\"添加学员成功！" + "\",\"success\":true}");
                            }
                            else
                            {
                                userOperateLog.OperateInfo = "刷卡添加学员";
                                userOperateLog.IfSuccess = false;
                                userOperateLog.Description = "添加失败";
                                context.Response.Write("{\"msg\":\"添加失败！\",\"success\":false}");
                            }
                        }
                        else
                        {
                            userOperateLog.OperateInfo = "刷卡添加学员";
                            userOperateLog.IfSuccess = false;
                            userOperateLog.Description = "无权限，请联系管理员";
                            context.Response.Write("{\"msg\":\"无权限，请联系管理员！\",\"success\":false}");
                        }
                        DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
                        break;
                    case "uploadPicByCard":
                        string uploadPicByCard = SavePicToServer(context);
                        context.Response.Write(uploadPicByCard);
                        break;
                    default:
                        context.Response.Write("{\"msg\":\"参数错误！\",\"success\":false}");
                        break;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"msg\":\"" + DriveMgr.Common.JsonHelper.StringFilter(ex.Message) + "\",\"success\":false}");
                userOperateLog.OperateInfo = "学员功能异常";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = DriveMgr.Common.JsonHelper.StringFilter(ex.Message);
                DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
            }
        }

        /// <summary>
        /// 缴费
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool DoTuition(HttpContext context)
        {
            PayModel model = new PayModel();
            int id = Convert.ToInt32(context.Request.Params["pay_id"]);
            decimal realpay_pay = Decimal.Parse(context.Request.Params["ui_registration_realpay_pay"] ?? "0");
            string remark_pay = context.Request.Params["ui_registration_remark_pay"] ?? "";
            bool isPayAll = context.Request.Params["ui_registration_status_pay"] == "true" ? true : false;

            bool isLocal = context.Request.Params["islocal_pay"] == "0" ? false : true;

            decimal? shoudPay = 0;
            string strShoudPay = new DriveMgr.BLL.RegistrationBLL().GetShoudPayByIsLocal(isLocal);

            if (strShoudPay != "")
            {
                shoudPay = Decimal.Parse(strShoudPay);
            }
            else
            {
                shoudPay = 0;
            }

            model.ShoudPay = shoudPay;

            if ((shoudPay - realpay_pay) > 0)
            {
                if (isPayAll)
                {
                    model.Status = 2; //完全交费
                    model.SalePay = shoudPay - realpay_pay; //优惠金额
                }
                else
                {
                    model.Status = 1; //未完全交费
                }
            }
            else
            {
                model.Status = 2; //完全交费
            }

            model.StudentsID = id;
            model.RealPay = realpay_pay;
            FormsIdentity iid = (FormsIdentity)context.User.Identity;
            FormsAuthenticationTicket tickets = iid.Ticket;

            //获取票证里序列化的用户对象（反序列化）
            DriveMgr.Model.User userCheck = new JavaScriptSerializer().Deserialize<DriveMgr.Model.User>(tickets.UserData);

            model.Operater = userCheck.UserName;

            model.Remark = remark_pay;

            bool result = new DriveMgr.BLL.RegistrationBLL().DoTuition(model);
            return result;
        }

        /// <summary>
        /// 缴考试费
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool PayExam(HttpContext context)
        {
            PayExamModel model = new PayExamModel();
            int id = Convert.ToInt32(context.Request.Params["payExam_id"]);
            decimal realpay_pay = Decimal.Parse(context.Request.Params["ui_registration_realpay_payExam"] ?? "0");
            string remark_pay = context.Request.Params["ui_registration_remark_payExam"] ?? "";
            
            model.StudentsID = id;
            model.RealPay = realpay_pay;
            FormsIdentity iid = (FormsIdentity)context.User.Identity;
            FormsAuthenticationTicket tickets = iid.Ticket;

            //获取票证里序列化的用户对象（反序列化）
            DriveMgr.Model.User userCheck = new JavaScriptSerializer().Deserialize<DriveMgr.Model.User>(tickets.UserData);

            model.Operater = userCheck.UserName;

            model.Remark = remark_pay;

            bool result = new DriveMgr.BLL.RegistrationBLL().PayExam(model);
            return result;
        }
        

        /// <summary>
        /// 退学
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool DropOut(HttpContext context)
        {
            int id = Convert.ToInt32(context.Request.Params["exit_id"]);
            decimal exitpay_exit = Decimal.Parse(context.Request.Params["ui_registration_exitpay_exit"] ?? "0");
            string exit_remark = context.Request.Params["exit_remark"] ?? "";
            string ui_registration_remark_exit = context.Request.Params["ui_registration_remark_exit"] ?? "";
            decimal exit_realPay = Decimal.Parse(context.Request.Params["exit_realPay"] ?? "0");


            string remark = exit_remark + ";" + ui_registration_remark_exit;
            decimal leaveMoney = exit_realPay - exitpay_exit;

            bool result = new DriveMgr.BLL.RegistrationBLL().DropOut(id, leaveMoney, remark);
            return result;
        }

        /// <summary>
        /// 刷卡得到学员信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetInfoByCard(HttpContext context)
        {
            try
            {
                DriveMgr.BLL.RegistrationBLL bll = new DriveMgr.BLL.RegistrationBLL();
                string errorMsg = string.Empty;

                RegistrationByCardModel model = bll.GetInfoByCard(out errorMsg);
                if (model != null)
                {
                    if (model.Born != "" && model.Born != null)
                    {
                        int bornYear = Int32.Parse(model.Born.Substring(0, 4));
                        int thisYear = DateTime.Now.Year;
                        model.Age = thisYear - bornYear;
                        string picPath = Common.AppString.picPath;
                        model.PhotoFileName = model.PhotoFileName.Replace(picPath, "").Replace(@"\", "");
                    }
                }
                return model.ToJson();
                //return "{\"msg\":\"" + errorMsg + "\",\"success\":" + model.ToJson() + "}";
            }
            catch(Exception ex)
            {
                return ex.ToJson();
                //return "{\"msg\":\"" + "错误" + "\",\"success\":" + "错误" + "}";
            }

        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string UploadPic(HttpContext context)
        {
            string res = "";
            HttpFileCollection files = context.Request.Files;
            string msg = string.Empty;
            string error = string.Empty;
            string imgurl;
            if (files.Count > 0)
            {
                files[0].SaveAs(context.Server.MapPath("/admin/images/Pictures/") + System.IO.Path.GetFileName(files[0].FileName));
                msg = " 成功! 文件大小为:" + files[0].ContentLength;
                imgurl = "/admin/images/Pictures/" + files[0].FileName;
                //res = "{ error:'" + error + "', msg:'" + msg + "',imgurl:'" + imgurl + "'}";
                res = "{\"error\":\"" + error + "\",\"msg\":\"" + msg + "\",\"imgurl\":" + "\"" + imgurl + "\"}";
            }
            return res;
        }

        /// <summary>
        /// 上传本地图片到服务器
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string SavePicToServer(HttpContext context)
        {
            try
            {
                string filePath = context.Request.Form["srcPath"];
                
                if (String.IsNullOrEmpty(filePath))
                {
                    return "Fail".ToJson();
                }
                else
                {
                    WebClient MyWebClient = new WebClient();
                    string address = context.Server.MapPath("~/admin/images/Pictures/") + filePath;
                    string srcPath = Common.AppString.picPath + filePath;
                    MyWebClient.UploadFile(address, "PUT", srcPath);
                    return "OK".ToJson();
                }
            }
            catch(Exception ex)
            {
                return (ex.Message).ToJson();
            }
        }

        /// <summary>
        /// @author: anyang
        /// @datetime: 2014-06-15
        /// @description: 导出申请表
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string PrintApplyTable(HttpContext context)
        {
            string ui_students_name = context.Request.Params["ui_students_name"] ?? "";
            string ui_students_code = context.Request.Params["ui_students_code"] ?? "";
            string ui_students_cardnum = context.Request.Params["ui_students_cardnum"] ?? "";
            string ui_students_status = context.Request.Params["ui_students_status"] ?? "";
            string ui_pay_status = context.Request.Params["ui_pay_status"] ?? "";
            string ui_students_period = context.Request.Params["ui_students_period"] ?? "";
            string strWhere = " 1=1 and flag=1";
            if (ui_students_name.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_students_name))   //防止sql注入
                strWhere += string.Format(" and StudentsName like '%{0}%'", ui_students_name.Trim());
            if (ui_students_code.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_students_code))
                strWhere += string.Format(" and StudentCode like '%{0}%'", ui_students_code.Trim());
            if (ui_students_cardnum.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_students_cardnum))
                strWhere += string.Format(" and CardNum like '%{0}%'", ui_students_cardnum.Trim());
            if (ui_students_status.Trim() != "select" && ui_students_status.Trim() != "")
                strWhere += " and Status = '" + ui_students_status.Trim() + "'";
            if (ui_pay_status.Trim() != "select" && ui_pay_status.Trim() != "")
                strWhere += " and PayStatus = '" + ui_pay_status.Trim() + "'";
            if (ui_students_period.Trim() != "select" && ui_students_period.Trim() != "")
                strWhere += " and PeriodsID = '" + ui_students_period.Trim() + "'";

            DataTable dtStudents = new DriveMgr.BLL.RegistrationBLL().GetStudentsList(strWhere);
            StringBuilder builer = new StringBuilder();
            int m = 0;
            for (int i = 0; i < dtStudents.Rows.Count; i++)
            {
                try
                {
                    long id = Int64.Parse(dtStudents.Rows[i]["ID"].ToString());

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append(@" 1=1 and flag=1 AND ID=" + id);

                    DataTable dt = new BLL.RegistrationBLL().GetList(strSql.ToString());


                    var path = context.Server.MapPath("~/admin/Documents/机动车驾驶证申领表.doc");
                    Document doc = new Document(path);
                    DocumentBuilder builder = new DocumentBuilder(doc);

                    string studentsNum = dt.Rows[0]["StudentCode"].ToString();
                    string studentsArchivesId = dt.Rows[0]["ArchivesCode"].ToString();
                    string studentsName = dt.Rows[0]["StudentsName"].ToString();
                    string studentsSex = dt.Rows[0]["Sex"].ToString();
                    if (studentsSex == "True")
                    {
                        studentsSex = "男";
                    }
                    else
                    {
                        studentsSex = "女";
                    }
                    string studentsAddress = dt.Rows[0]["Address"].ToString();
                    string studentsPhone = dt.Rows[0]["PhoneNum"].ToString(); //手机号码
                    string studentsCardId = dt.Rows[0]["CardNum"].ToString();
                    string picPath = dt.Rows[0]["PicPath"].ToString() ?? "";


                    doc.Range.Replace("{Archives}", studentsArchivesId, false, false);
                    doc.Range.Replace("{name}", studentsName, false, false);
                    doc.Range.Replace("{sex}", studentsSex, false, false);
                    doc.Range.Replace("{address}", studentsAddress, false, false);
                    doc.Range.Replace("{phone}", studentsPhone, false, false);
                    //doc.Range.Replace("{sex}", "男", false, false);
                    //string str = "61042719881011361X";
                    char[] mychar = studentsCardId.ToCharArray();
                    for (int j = 0; j < mychar.Length; j++)
                    {
                        doc.Range.Replace("{" + j + "}", mychar[j].ToString(), false, false);
                    }
                    if (mychar.Length < 18)
                    {
                        for (int j = mychar.Length; j < 18; j++)
                        {
                            doc.Range.Replace("{" + j + "}", "", false, false);
                        }
                    }
                    string birth = string.Empty;
                    if (mychar.Length >= 14)
                    {
                        birth = mychar[6].ToString() + mychar[7].ToString() + mychar[8].ToString() + mychar[9].ToString()
                            + "-" + mychar[10].ToString() + mychar[11].ToString() + "-" + mychar[12].ToString() + mychar[13].ToString();
                    }
                    else
                    {
                        birth = "";
                    }
                    doc.Range.Replace("{birth}", birth, false, false);
                    builder.MoveToBookmark("照片");
                    string savePath = context.Server.MapPath("~/admin/StudentsApplyTable/");

                    if (!String.IsNullOrEmpty(picPath))
                    {
                        string realPic = context.Server.MapPath("~/admin/");
                        realPic += picPath;
                        Shape sp = builder.InsertImage(realPic);
                        sp.Width = 50;
                        sp.Height = 60;
                    }

                    var fileName = "ReplaceFlag(" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ").doc";
                    //doc.Save(fileName, SaveFormat.Doc, SaveType.OpenInBrowser, context.Response);
                    //直接保存
                    doc.Save(savePath + studentsNum + ".doc", SaveFormat.Doc);
                    doc.Print(); //是否打印
                }
                catch(Exception ex)
                {
                    m++;
                    builer.Append(dtStudents.Rows[i]["StudentCode"] + "打印出错！</br>");
                    continue;
                }
            }
            string strResult = "打印成功！</br>" + "共打印" + dtStudents.Rows.Count + "</br>其中" + m + "个出错！";
            return strResult.ToJson();
        }

        /// <summary>
        /// 导出学员信息
        /// </summary>
        /// <param name="context"></param>
        public string DownloadExcel(HttpContext context)
        {
            string ui_students_name = context.Request.Params["ui_students_name"] ?? "";
            string ui_students_code = context.Request.Params["ui_students_code"] ?? "";
            string ui_students_cardnum = context.Request.Params["ui_students_cardnum"] ?? "";
            string ui_students_status = context.Request.Params["ui_students_status"] ?? "";
            string ui_pay_status = context.Request.Params["ui_pay_status"] ?? "";
            string ui_students_period = context.Request.Params["ui_students_period"] ?? "";
            string strWhere = " 1=1 and flag=1";
            if (ui_students_name.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_students_name))   //防止sql注入
                strWhere += string.Format(" and StudentsName like '%{0}%'", ui_students_name.Trim());
            if (ui_students_code.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_students_code))
                strWhere += string.Format(" and StudentCode like '%{0}%'", ui_students_code.Trim());
            if (ui_students_cardnum.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_students_cardnum))
                strWhere += string.Format(" and CardNum like '%{0}%'", ui_students_cardnum.Trim());
            if (ui_students_status.Trim() != "select" && ui_students_status.Trim() != "")
                strWhere += " and Status = '" + ui_students_status.Trim() + "'";
            if (ui_pay_status.Trim() != "select" && ui_pay_status.Trim() != "")
                strWhere += " and PayStatus = '" + ui_pay_status.Trim() + "'";
            if (ui_students_period.Trim() != "select" && ui_students_period.Trim() != "")
                strWhere += " and PeriodsID = '" + ui_students_period.Trim() + "'";

            DataTable dt = new DriveMgr.BLL.RegistrationBLL().GetExportStudentsList(strWhere);

            string reportName = "~/admin/FileUploader/学员信息表.xlsx";
            reportName = context.Server.MapPath(reportName);
            bool result = NPOIHelper.TableToExcelForXLSX(dt, reportName);
            if (result)
            {
                return "OK".ToJson();
            }
            else
            {
                return "Fail".ToJson();
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