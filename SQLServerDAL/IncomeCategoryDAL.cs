/***********************
 @ author:zlong
 @ Date:2015-01-06
 @ Desc:收入分类数据处理层
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
    public class IncomeCategoryDAL : IIncomeCategoryDAL
    {        

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddIncomeCategory(Model.IncomeCategoryModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_IncomeCategory(");
            strSql.Append("CategoryName,Remark,CreatePerson,CreateTime,UpdatePerson,UpdateTime,DeleteMark)");
            strSql.Append(" values (");
            strSql.Append("@CategoryName,@Remark,@CreatePerson,@CreateTime,@UpdatePerson,@UpdateTime,@DeleteMark)");
            //strSql.Append(";select IDENT_CURRENT('tb_IncomeCategory')");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryName", SqlDbType.VarChar,50),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,15),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,15),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
                    new SqlParameter("@DeleteMark", SqlDbType.Bit,1),};
            parameters[0].Value = model.CategoryName;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.CreatePerson;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.UpdatePerson;
            parameters[5].Value = model.UpdateTime;
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
        public bool UpdateIncomeCategory(Model.IncomeCategoryModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_IncomeCategory set ");
            strSql.Append("CategoryName=@CategoryName,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CreatePerson=@CreatePerson,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdatePerson=@UpdatePerson,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("DeleteMark=@DeleteMark");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryName", SqlDbType.VarChar,50),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,15),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,15),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
                    new SqlParameter("@DeleteMark", SqlDbType.Bit,1),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.CategoryName;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.CreatePerson;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.UpdatePerson;
            parameters[5].Value = model.UpdateTime;
            parameters[6].Value = model.DeleteMark;
            parameters[7].Value = model.Id;

            object obj = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.Text, strSql.ToString(), parameters);
            if (Convert.ToInt32(obj) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteIncomeCategory(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_IncomeCategory Set DeleteMark = 1 ");
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
        public bool DeleteIncomeCategoryList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_IncomeCategory Set DeleteMark = 1 ");
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
        public Model.IncomeCategoryModel GetIncomeCategoryModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,CategoryName,DeleteMark,Remark,CreatePerson,CreateTime,UpdatePerson,UpdateTime from tb_IncomeCategory ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            Model.IncomeCategoryModel model = new Model.IncomeCategoryModel();
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
        private Model.IncomeCategoryModel DataRowToModel(DataRow row)
        {
            Model.IncomeCategoryModel model = new Model.IncomeCategoryModel();
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
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }                
                if (row["CreatePerson"] != null)
                {
                    model.CreatePerson = row["CreatePerson"].ToString();
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }                
                if (row["UpdatePerson"] != null)
                {
                    model.UpdatePerson = row["UpdatePerson"].ToString();
                }
                if (row["UpdateTime"] != null && row["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(row["UpdateTime"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,CategoryName,DeleteMark,Remark,CreatePerson,CreateTime,UpdatePerson,UpdateTime ");
            strSql.Append(" FROM tb_IncomeCategory ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DriveMgr.Common.SqlHelper.GetDataset(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.Text, strSql.ToString());
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
        /// 得到入账统计的表名和字段名
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableFromIncome()
        {
            string cmd = @"select Id,CategoryName,BelongTable,BelongDataBase,BelongMember,BelongTimeMember
                            FROM dbo.tb_IncomeCategory
                            where DeleteMark=0";
            return DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.Text, cmd);
        }

        /// <summary>
        /// 获得一年的入账收入
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataSet GetIncomeOfOneYear(string year)
        {
            //存储过程的形式
            SqlParameter[] parameters = {
					new SqlParameter("@year", SqlDbType.VarChar,20)};
            parameters[0].Value = year;

            return DriveMgr.Common.SqlHelper.GetDataset(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.StoredProcedure, "up_incomeCategary", parameters);
        }

        /// <summary>
        /// 获得一年的入账收入
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataSet GetIncomeOfOneYear(DataTable dt, string year)
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
            return DriveMgr.Common.SqlHelper.GetDataset(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.Text, strSql.ToString());
        }
    }
}
