/***********************
 @ author:zlong
 @ Date:2015-01-06
 @ Desc:收入分类逻辑层实现
 * ********************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DriveMgr.Common;

namespace DriveMgr.BLL
{
    public class IncomeCategoryBLL
    {
        private static readonly DriveMgr.IDAL.IIncomeCategoryDAL incomeCategoryDal = DriveMgr.DALFactory.Factory.GetIncomeCategoryDAL();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddIncomeCategory(Model.IncomeCategoryModel model)
        {
            return incomeCategoryDal.AddIncomeCategory(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateIncomeCategory(Model.IncomeCategoryModel model)
        {
            return incomeCategoryDal.UpdateIncomeCategory(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteIncomeCategory(int id)
        {
            return incomeCategoryDal.DeleteIncomeCategory(id);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteIncomeCategoryList(string idlist)
        {
            return incomeCategoryDal.DeleteIncomeCategoryList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.IncomeCategoryModel GetIncomeCategoryModel(int Id)
        {
            return incomeCategoryDal.GetIncomeCategoryModel(Id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return incomeCategoryDal.GetList(strWhere);
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
                strSql.Append(" and CreateTime > '" + startDate + "'");
            }
            if (endDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateTime < '" + endDate + "'");
            }
            return incomeCategoryDal.GetPagerData("tb_IncomeCategory", "Id,CategoryName,Remark,CreatePerson,CreateTime", order, pageSize, pageIndex, strSql.ToString(), out totalCount);
        }

        /// <summary>
        /// 得到入账统计的表名和字段名
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableFromIncome()
        {
            return incomeCategoryDal.GetTableFromIncome();
        }

        /// <summary>
        /// 获得一年的入账收入
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataSet GetIncomeOfOneYear(string year)
        {
            //DataTable dt = GetTableFromIncome();
            return incomeCategoryDal.GetIncomeOfOneYear(year);
        }

        /// <summary>
        /// 取数据，构造json数据，为画图做准备
        /// </summary>
        /// <param name="type"></param>
        /// <param name="checkedYear"></param>
        /// <returns></returns>
        public HighChartOptions GetHighchart(HighchartTypeEnum type, string checkedYear)
        {
            DataSet ds = GetIncomeOfOneYear(checkedYear);
            List<Series> ser = new List<Series>();

            for (int i = 0; i < ds.Tables.Count; i++) //每一个入账类型的12个月的费用
            {
                Series s = new Series();
                //s.name = ds.Tables[i].Rows[0]["cateName"].ToString();
                List<object> obj = new List<object>();
                for(int j=0;j<ds.Tables[i].Rows.Count;j++)
                {
                    float tname = Single.Parse(ds.Tables[i].Rows[j][1].ToString());  //入账费用

                    obj.Add(tname);
                }
                s.data = obj;
                s.type= ChartTypeEnum.column.ToString();
                s.allowPointSelect =false;
               
                ser.Add(s);
            }
            ser[0].name = "贷款";
            ser[1].name = "学费 ";
            ser[2].name = "考试费 ";
            ser[3].name = "外校包车费用";
            ser[4].name = "本校包车费用";
            ser[5].name = "外校场地租用费用";
            ser[6].name = "本校场地租用费用";
            HighChartOptions currentChart = new HighChartOptions();
            currentChart = new HighChartOptions()
            {
                //chart = new Chart()
                //{
                //    renderTo = "highChartDiv",
                //    type = ChartTypeEnum.area.ToString(),
                //    reflow=true
                //},
                title = new Title() { text = checkedYear+"年入账统计图" },
                //xAxis = new List<XAxis>() { 
                //    new XAxis(){
                //        categories = new List<string>() { "一月", "二月", "三月", "四月", "五月","六月", "七月", "八月", "九月", "十月","十一月","十二月" },
                //        reversed = false,
                //        opposite = false
                //    }
                //},
                yAxis = new YAxis() { title = new Title() { text = checkedYear+"入账费用(元)" } },
                //tooltip = new ToolTip() { crosshairs = new List<bool>() { true, false } },
                series = ser
            };
            return currentChart;
        }

        /// <summary>
        /// 得到入账表格数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetIncomeTableData(string checkedYear)
        {
            DataSet ds = GetIncomeOfOneYear(checkedYear);
            DataTable dt = new DataTable();
            DataColumn colType = new DataColumn();
            colType.ColumnName = "入账类型";
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
                string incomeType = GetTypeofIncome(j);
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
        /// 得到入账类型
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private string GetTypeofIncome(int i)
        {
            switch(i)
            {
                case 0:
                    return "贷款";
                case 1:
                    return "学费";
                case 2:
                    return "考试费";
                case 3:
                    return "外校包车费用";
                case 4:
                    return "本校包车费用";
                case 5:
                    return "外校场地租用费用";
                case 6:
                    return "本校场地租用费用";
                default :
                    return "";

            }
        }
    }
}
