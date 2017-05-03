using DriveMgr.BLL;
using DriveMgr.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DriveMgr.WebUI.admin.Statistics
{
    /// <summary>
    /// bg_ExpensesStatisticHandler 的摘要说明
    /// </summary>
    public class bg_ExpensesStatisticHandler : IHttpHandler
    {
        DriveMgr.Model.UserOperateLog userOperateLog = null;   //操作日志对象
        ExpensesCategoryBLL bll = new ExpensesCategoryBLL();
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
                        GetExpensesOfOneYear(context);
                        break;
                    case "getIncomeTableData":
                        GetExpensesTableData(context);
                        break;
                    default:
                        context.Response.Write("{\"msg\":\"参数错误！\",\"success\":false}");
                        break;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("{\"msg\":\"" + DriveMgr.Common.JsonHelper.StringFilter(ex.Message) + "\",\"success\":false}");
                userOperateLog.OperateInfo = "出账统计功能异常";
                userOperateLog.IfSuccess = false;
                userOperateLog.Description = DriveMgr.Common.JsonHelper.StringFilter(ex.Message);
                DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
            }
        }

        /// <summary>
        /// 查询一年的出账情况
        /// </summary>
        /// <param name="context"></param>
        private void GetExpensesOfOneYear(HttpContext context)
        {
            string checkedYear = context.Request.Params["checkedYear"];
            int chartType = (int)HighchartTypeEnum.柱状图;
            HighchartTypeEnum type = (HighchartTypeEnum)Enum.Parse(typeof(HighchartTypeEnum), chartType.ToString());

            HighChartOptions chart = bll.GetExpensesHighchart(type, checkedYear);
            var tmd = new { value = chart, label = type.ToString() };

            context.Response.Write(tmd.ToJson());

            userOperateLog.OperateInfo = "查询出账费用情况";
            userOperateLog.IfSuccess = true;
            userOperateLog.Description = "";
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
        }

        /// <summary>
        /// 得到入账表格数据
        /// </summary>
        /// <param name="context"></param>
        private void GetExpensesTableData(HttpContext context)
        {
            string checkedYear = context.Request.Params["checkedYear"];
            DataTable dt = bll.GetExpensesTableData(checkedYear);
            context.Response.Write(dt.ToJson());

            userOperateLog.OperateInfo = "得到出账表格数据";
            userOperateLog.IfSuccess = true;
            userOperateLog.Description = "";
            DriveMgr.BLL.UserOperateLog.InsertOperateInfo(userOperateLog);
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