/***********************
 @ author:zlong
 @ Date:2015-01-08
 @ Desc:支出分类逻辑层
 * ********************/

using DriveMgr.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class ExpensesCategoryBLL
    {
        private static readonly DriveMgr.IDAL.IExpensesCategoryDAL expensesCategoryDal = DriveMgr.DALFactory.Factory.GetExpensesCategoryDAL();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddExpensesCategory(Model.ExpensesCategoryModel model)
        {
            return expensesCategoryDal.AddExpensesCategory(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateExpensesCategory(Model.ExpensesCategoryModel model)
        {
            return expensesCategoryDal.UpdateExpensesCategory(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteExpensesCategory(int id)
        {
            return expensesCategoryDal.DeleteExpensesCategory(id);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteExpensesCategoryList(string idlist)
        {
            return expensesCategoryDal.DeleteExpensesCategoryList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.ExpensesCategoryModel GetExpensesCategoryModel(int id)
        {
            return expensesCategoryDal.GetExpensesCategoryModel(id);            
        }


        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="categoryname">类型</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public string GetPagerData(string categoryname, string startDate, string endDate, string order, int pageSize, int pageIndex, out int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DeleteMark = 0 ");
            if (categoryname.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(categoryname))
            {
                strSql.Append(" and CategoryName like '%" + categoryname + "%'");
            }
            if (startDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate > '" + startDate + "'");
            }
            if (endDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate < '" + endDate + "'");
            }
            return expensesCategoryDal.GetPagerData("tb_ExpensesCategory", "Id,CategoryName,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate", order, pageSize, pageIndex, strSql.ToString(), out totalCount);
        }

        /// <summary>
        /// 得到出账统计的表名和字段名
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableFromExpenses()
        {
            return expensesCategoryDal.GetTableFromExpenses();
        }

        /// <summary>
        /// 获得一年的出账收入
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataSet GetExpensesOfOneYear(string year)
        {
            return expensesCategoryDal.GetExpensesOfOneYear(year);
        }

        /// <summary>
        /// 获得一年的出账收入
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataSet GetExpensesOfOneYear(DataTable dt, string year)
        {
            return expensesCategoryDal.GetExpensesOfOneYear(dt, year);
        }

        /// <summary>
        /// 取数据，构造json数据，为画图做准备
        /// </summary>
        /// <param name="type"></param>
        /// <param name="checkedYear"></param>
        /// <returns></returns>
        public HighChartOptions GetExpensesHighchart(HighchartTypeEnum type, string checkedYear)
        {
            //DataTable dt = GetTableFromExpenses();
            DataSet ds = GetExpensesOfOneYear(checkedYear);
            List<Series> ser = new List<Series>();

            for (int i = 0; i < ds.Tables.Count; i++)
            {
                Series s = new Series();
                //s.name = ds.Tables[i].Rows[0]["cateName"].ToString();
                List<object> obj = new List<object>();
                for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                {
                    float tname = Single.Parse(ds.Tables[i].Rows[j][1].ToString());

                    obj.Add(tname);
                }
                s.data = obj;
                s.type = ChartTypeEnum.column.ToString();
                s.allowPointSelect = false;

                ser.Add(s);
            }
            ser[0].name = "还款";
            ser[1].name = "车辆维护费 ";
            ser[2].name = "业务招待费";
            ser[3].name = "差旅费";
            ser[4].name = "办工费用";
            HighChartOptions currentChart = new HighChartOptions();
            currentChart = new HighChartOptions()
            {
                //chart = new Chart()
                //{
                //    renderTo = "highChartDiv",
                //    type = ChartTypeEnum.area.ToString(),
                //    reflow=true
                //},
                title = new Title() { text = checkedYear + "年出账统计图" },
                //xAxis = new List<XAxis>() { 
                //    new XAxis(){
                //        categories = new List<string>() { "一月", "二月", "三月", "四月", "五月","六月", "七月", "八月", "九月", "十月","十一月","十二月" },
                //        reversed = false,
                //        opposite = false
                //    }
                //},
                yAxis = new YAxis() { title = new Title() { text = checkedYear + "出账费用(元)" } },
                //tooltip = new ToolTip() { crosshairs = new List<bool>() { true, false } },
                series = ser
            };
            return currentChart;
        }

        /// <summary>
        /// 得到出账表格数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetExpensesTableData(string checkedYear)
        {
            DataSet ds = GetExpensesOfOneYear(checkedYear);
            DataTable dt = new DataTable();
            DataColumn colType = new DataColumn();
            colType.ColumnName = "出账类型";
            colType.DataType = Type.GetType("System.String");
            dt.Columns.Add(colType);

            for (int i = 1; i <= 12; i++)
            {
                DataColumn col = new DataColumn();
                col.ColumnName = i+"月";
                col.DataType = Type.GetType("System.String");
                dt.Columns.Add(col);
            }
            for (int j = 0; j < ds.Tables.Count; j++)
            {
                DataRow dr = dt.NewRow();
                string incomeType = GetTypeofExpenses(j);
                dr[0] = incomeType;

                for (int i = 1; i <= 12; i++)
                {
                    dr[i] = ds.Tables[j].Rows[i-1][1];
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 得到出账类型
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private string GetTypeofExpenses(int i)
        {
            switch (i)
            {
                case 0:
                    return "还款";
                case 1:
                    return "车辆维护费";
                case 2:
                    return "业务招待费";
                case 3:
                    return "差旅费";
                case 4:
                    return "办工费用";
                default:
                    return "";

            }
        }
    }
}
