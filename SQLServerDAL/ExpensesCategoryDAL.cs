/***********************
 @ author:zlong
 @ Date:2015-01-08
 @ Desc:支出分类数据层
 * ********************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DriveMgr.IDAL;
using System.Data.SqlClient;
using System.Data;

namespace DriveMgr.SQLServerDAL
{
    public class ExpensesCategoryDAL:IExpensesCategoryDAL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddExpensesCategory(Model.ExpensesCategoryModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_ExpensesCategory(");
            strSql.Append("CategoryName,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark)");
            strSql.Append(" values (");
            strSql.Append("@CategoryName,@Remark,@CreatePerson,@CreateDate,@UpdatePerson,@UpdateDate,@DeleteMark)");

            SqlParameter[] parameters = {
					new SqlParameter("@CategoryName", SqlDbType.VarChar,50),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1)};
            parameters[0].Value = model.CategoryName;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.CreatePerson;
            parameters[3].Value = model.CreateDate;
            parameters[4].Value = model.UpdatePerson;
            parameters[5].Value = model.UpdateDate;
            parameters[6].Value = model.DeleteMark;

            int rows = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }     
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateExpensesCategory(Model.ExpensesCategoryModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_ExpensesCategory set ");
            strSql.Append("CategoryName=@CategoryName,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CreatePerson=@CreatePerson,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("UpdatePerson=@UpdatePerson,");
            strSql.Append("UpdateDate=@UpdateDate,");
            strSql.Append("DeleteMark=@DeleteMark");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryName", SqlDbType.VarChar,50),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.CategoryName;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.CreatePerson;
            parameters[3].Value = model.CreateDate;
            parameters[4].Value = model.UpdatePerson;
            parameters[5].Value = model.UpdateDate;
            parameters[6].Value = model.DeleteMark;
            parameters[7].Value = model.Id;

            object obj = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.Text, strSql.ToString(), parameters);
            if (Convert.ToInt32(obj) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }     
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteExpensesCategory(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_ExpensesCategory Set DeleteMark = 1 ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            object obj = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.Text, strSql.ToString(), parameters);
            if (Convert.ToInt32(obj) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }    
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteExpensesCategoryList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_ExpensesCategory Set DeleteMark = 1 ");
            strSql.Append(" where Id in (" + idlist + ")  ");
            try
            {
                DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.Text, strSql.ToString());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.ExpensesCategoryModel GetExpensesCategoryModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,CategoryName,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark from tb_ExpensesCategory ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            Model.ExpensesCategoryModel model = new Model.ExpensesCategoryModel();
            DataSet ds = DriveMgr.Common.SqlHelper.GetDataset(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        private Model.ExpensesCategoryModel DataRowToModel(DataRow row)
        {
            Model.ExpensesCategoryModel model = new Model.ExpensesCategoryModel();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["CategoryName"] != null)
                {
                    model.CategoryName = row["CategoryName"].ToString();
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["CreatePerson"] != null)
                {
                    model.CreatePerson = row["CreatePerson"].ToString();
                }
                if (row["CreateDate"] != null && row["CreateDate"].ToString() != "")
                {
                    model.CreateDate = DateTime.Parse(row["CreateDate"].ToString());
                }
                if (row["UpdatePerson"] != null)
                {
                    model.UpdatePerson = row["UpdatePerson"].ToString();
                }
                if (row["UpdateDate"] != null && row["UpdateDate"].ToString() != "")
                {
                    model.UpdateDate = DateTime.Parse(row["UpdateDate"].ToString());
                }
                if (row["DeleteMark"] != null && row["DeleteMark"].ToString() != "")
                {
                    if ((row["DeleteMark"].ToString() == "1") || (row["DeleteMark"].ToString().ToLower() == "true"))
                    {
                        model.DeleteMark = true;
                    }
                    else
                    {
                        model.DeleteMark = false;
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">要取的列名（逗号分开）</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="where">查询条件</param>
        /// <param name="totalCount">总记录数</param>
        public string GetPagerData(string tableName, string columns, string order, int pageSize, int pageIndex, string where, out int totalCount)
        {
            DataTable dt = DriveMgr.Common.SqlPagerHelper.GetPagerData(DriveMgr.Common.SqlHelper.financialMgrConn, tableName, columns, order, pageSize, pageIndex, where, out totalCount);
            return DriveMgr.Common.JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 得到出账统计的表名和字段名
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableFromExpenses()
        {
            string cmd = @"select Id,CategoryName,BelongTable,BelongDataBase,BelongMember,BelongTimeMember
                            FROM dbo.tb_ExpensesCategory
                            where DeleteMark=0";
            return DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.Text, cmd);
        }

        /// <summary>
        /// 获得一年的出账收入
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataSet GetExpensesOfOneYear(string year)
        {
            //存储过程的形式
            SqlParameter[] parameters = {
					new SqlParameter("@year", SqlDbType.VarChar,20)};
            parameters[0].Value = year;

            return DriveMgr.Common.SqlHelper.GetDataset(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.StoredProcedure, "up_expensesCategory", parameters);
        }

        /// <summary>
        /// 获得一年的出账收入
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataSet GetExpensesOfOneYear(DataTable dt, string year)
        {
            StringBuilder strSql = new StringBuilder();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string dataBase = dt.Rows[i]["BelongDataBase"].ToString();
                string table = dt.Rows[i]["BelongTable"].ToString();
                string member = dt.Rows[i]["BelongMember"].ToString();
                string timeMember = dt.Rows[i]["BelongTimeMember"].ToString();
                string categoryName = dt.Rows[i]["CategoryName"].ToString();

                strSql.Append(@" SELECT '" + categoryName + @"' as cateName, CASE WHEN DATEPART(MONTH," + dataBase + "." + table + "." + timeMember + @") = 1 THEN sum(" + dataBase + "." + table + "." + member + @") ELSE 0 END AS '一月',
                                       CASE WHEN DATEPART(MONTH," + dataBase + "." + table + "." + timeMember + @") = 2 THEN sum(" + dataBase + "." + table + "." + member + @") ELSE 0 END AS '二月',
                                       CASE WHEN DATEPART(MONTH," + dataBase + "." + table + "." + timeMember + @") = 3 THEN sum(" + dataBase + "." + table + "." + member + @") ELSE 0 END AS '三月',
                                       CASE WHEN DATEPART(MONTH," + dataBase + "." + table + "." + timeMember + @") = 4 THEN sum(" + dataBase + "." + table + "." + member + @") ELSE 0 END AS '四月',
                                       CASE WHEN DATEPART(MONTH," + dataBase + "." + table + "." + timeMember + @") = 5 THEN sum(" + dataBase + "." + table + "." + member + @") ELSE 0 END AS '五月',
                                       CASE WHEN DATEPART(MONTH," + dataBase + "." + table + "." + timeMember + @") = 6 THEN sum(" + dataBase + "." + table + "." + member + @") ELSE 0 END AS '六月',
                                       CASE WHEN DATEPART(MONTH," + dataBase + "." + table + "." + timeMember + @") = 7 THEN sum(" + dataBase + "." + table + "." + member + @") ELSE 0 END AS '七月',
                                       CASE WHEN DATEPART(MONTH," + dataBase + "." + table + "." + timeMember + @") = 8 THEN sum(" + dataBase + "." + table + "." + member + @") ELSE 0 END AS '八月',
                                       CASE WHEN DATEPART(MONTH," + dataBase + "." + table + "." + timeMember + @") = 9 THEN sum(" + dataBase + "." + table + "." + member + @") ELSE 0 END AS '九月',
                                       CASE WHEN DATEPART(MONTH," + dataBase + "." + table + "." + timeMember + @") = 10 THEN sum(" + dataBase + "." + table + "." + member + @") ELSE 0 END AS '十月',
                                       CASE WHEN DATEPART(MONTH," + dataBase + "." + table + "." + timeMember + @") = 11 THEN sum(" + dataBase + "." + table + "." + member + @") ELSE 0 END AS '十一月',
                                       CASE WHEN DATEPART(MONTH," + dataBase + "." + table + "." + timeMember + @") = 12 THEN sum(" + dataBase + "." + table + "." + member + @") ELSE 0 END AS '十二月'
                                       FROM " + dataBase + "." + table + @" where DATEPART(YEAR," + dataBase + "." + table + "." + timeMember + @")=" + year + @"
                                       group by(DATEPART(MONTH," + dataBase + "." + table + "." + timeMember + @")) ");
            }
            string strExSql = strSql.ToString();
            return DriveMgr.Common.SqlHelper.GetDataset(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.Text, strExSql);
        }
    }
}
