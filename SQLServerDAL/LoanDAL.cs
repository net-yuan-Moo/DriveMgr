/***********************
 @ author:zlong
 @ Date:2015-01-08
 @ Desc:贷款管理数据层
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
    public class LoanDAL:ILoanDAL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddLoan(Model.LoanModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_Loan(");
            strSql.Append("LoanAmount,Bank,Lenders,LoanDate,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark)");
            strSql.Append(" values (");
            strSql.Append("@LoanAmount,@Bank,@Lenders,@LoanDate,@Remark,@CreatePerson,@CreateDate,@UpdatePerson,@UpdateDate,@DeleteMark)");

            SqlParameter[] parameters = {
					new SqlParameter("@LoanAmount", SqlDbType.Money,8),
					new SqlParameter("@Bank", SqlDbType.VarChar,50),
					new SqlParameter("@Lenders", SqlDbType.VarChar,50),
					new SqlParameter("@LoanDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1)};
            parameters[0].Value = model.LoanAmount;
            parameters[1].Value = model.Bank;
            parameters[2].Value = model.Lenders;
            parameters[3].Value = model.LoanDate;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.CreatePerson;
            parameters[6].Value = model.CreateDate;
            parameters[7].Value = model.UpdatePerson;
            parameters[8].Value = model.UpdateDate;
            parameters[9].Value = model.DeleteMark;

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
        public bool UpdateLoan(Model.LoanModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Loan set ");
            strSql.Append("LoanAmount=@LoanAmount,");
            strSql.Append("Bank=@Bank,");
            strSql.Append("Lenders=@Lenders,");
            strSql.Append("LoanDate=@LoanDate,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CreatePerson=@CreatePerson,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("UpdatePerson=@UpdatePerson,");
            strSql.Append("UpdateDate=@UpdateDate,");
            strSql.Append("DeleteMark=@DeleteMark");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@LoanAmount", SqlDbType.Money,8),
					new SqlParameter("@Bank", SqlDbType.VarChar,50),
					new SqlParameter("@Lenders", SqlDbType.VarChar,50),
					new SqlParameter("@LoanDate", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.LoanAmount;
            parameters[1].Value = model.Bank;
            parameters[2].Value = model.Lenders;
            parameters[3].Value = model.LoanDate;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.CreatePerson;
            parameters[6].Value = model.CreateDate;
            parameters[7].Value = model.UpdatePerson;
            parameters[8].Value = model.UpdateDate;
            parameters[9].Value = model.DeleteMark;
            parameters[10].Value = model.Id;

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
        public bool DeleteLoan(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_Loan Set DeleteMark = 1 ");
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
        public bool DeleteLoanList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_Loan Set DeleteMark = 1 ");
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
        public Model.LoanModel GetLoanModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,LoanAmount,Bank,Lenders,LoanDate,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark from tb_Loan ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            Model.LoanModel model = new Model.LoanModel();
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
        private Model.LoanModel DataRowToModel(DataRow row)
        {
            Model.LoanModel model = new Model.LoanModel();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["LoanAmount"] != null && row["LoanAmount"].ToString() != "")
                {
                    model.LoanAmount = decimal.Parse(row["LoanAmount"].ToString());
                }
                if (row["Bank"] != null)
                {
                    model.Bank = row["Bank"].ToString();
                }
                if (row["Lenders"] != null)
                {
                    model.Lenders = row["Lenders"].ToString();
                }
                if (row["LoanDate"] != null && row["LoanDate"].ToString() != "")
                {
                    model.LoanDate = DateTime.Parse(row["LoanDate"].ToString());
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

    }
}
