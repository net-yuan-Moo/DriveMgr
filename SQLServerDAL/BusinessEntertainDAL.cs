/***********************
 @ author:zlong
 @ Date:2015-01-08
 @ Desc:业务招待管理数据层
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
    public class BusinessEntertainDAL:IBusinessEntertainDAL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddBusinessEntertain(Model.BusinessEntertainModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_BusinessEntertain(");
            strSql.Append("EntertainAmount,EntertainObject,Transactor,TransactDate,EntertainUse,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark)");
            strSql.Append(" values (");
            strSql.Append("@EntertainAmount,@EntertainObject,@Transactor,@TransactDate,@EntertainUse,@Remark,@CreatePerson,@CreateDate,@UpdatePerson,@UpdateDate,@DeleteMark)");

            SqlParameter[] parameters = {
					new SqlParameter("@EntertainAmount", SqlDbType.Money,8),
					new SqlParameter("@EntertainObject", SqlDbType.VarChar,100),
					new SqlParameter("@Transactor", SqlDbType.VarChar,50),
					new SqlParameter("@TransactDate", SqlDbType.DateTime),
					new SqlParameter("@EntertainUse", SqlDbType.VarChar,100),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1)};
            parameters[0].Value = model.EntertainAmount;
            parameters[1].Value = model.EntertainObject;
            parameters[2].Value = model.Transactor;
            parameters[3].Value = model.TransactDate;
            parameters[4].Value = model.EntertainUse;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.CreatePerson;
            parameters[7].Value = model.CreateDate;
            parameters[8].Value = model.UpdatePerson;
            parameters[9].Value = model.UpdateDate;
            parameters[10].Value = model.DeleteMark;

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
        public bool UpdateBusinessEntertain(Model.BusinessEntertainModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_BusinessEntertain set ");
            strSql.Append("EntertainAmount=@EntertainAmount,");
            strSql.Append("EntertainObject=@EntertainObject,");
            strSql.Append("Transactor=@Transactor,");
            strSql.Append("TransactDate=@TransactDate,");
            strSql.Append("EntertainUse=@EntertainUse,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CreatePerson=@CreatePerson,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("UpdatePerson=@UpdatePerson,");
            strSql.Append("UpdateDate=@UpdateDate,");
            strSql.Append("DeleteMark=@DeleteMark");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@EntertainAmount", SqlDbType.Money,8),
					new SqlParameter("@EntertainObject", SqlDbType.VarChar,100),
					new SqlParameter("@Transactor", SqlDbType.VarChar,50),
					new SqlParameter("@TransactDate", SqlDbType.DateTime),
					new SqlParameter("@EntertainUse", SqlDbType.VarChar,100),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.EntertainAmount;
            parameters[1].Value = model.EntertainObject;
            parameters[2].Value = model.Transactor;
            parameters[3].Value = model.TransactDate;
            parameters[4].Value = model.EntertainUse;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.CreatePerson;
            parameters[7].Value = model.CreateDate;
            parameters[8].Value = model.UpdatePerson;
            parameters[9].Value = model.UpdateDate;
            parameters[10].Value = model.DeleteMark;
            parameters[11].Value = model.Id;

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
        public bool DeleteBusinessEntertain(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_BusinessEntertain Set DeleteMark = 1 ");
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
        public bool DeleteBusinessEntertainList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_BusinessEntertain Set DeleteMark = 1 ");
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
        public Model.BusinessEntertainModel GetBusinessEntertainModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,EntertainAmount,EntertainObject,Transactor,TransactDate,EntertainUse,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark from tb_BusinessEntertain ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            Model.BusinessEntertainModel model = new Model.BusinessEntertainModel();
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
        private Model.BusinessEntertainModel DataRowToModel(DataRow row)
        {
            Model.BusinessEntertainModel model = new Model.BusinessEntertainModel();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["EntertainAmount"] != null && row["EntertainAmount"].ToString() != "")
                {
                    model.EntertainAmount = decimal.Parse(row["EntertainAmount"].ToString());
                }
                if (row["EntertainObject"] != null)
                {
                    model.EntertainObject = row["EntertainObject"].ToString();
                }
                if (row["Transactor"] != null)
                {
                    model.Transactor = row["Transactor"].ToString();
                }
                if (row["TransactDate"] != null && row["TransactDate"].ToString() != "")
                {
                    model.TransactDate = DateTime.Parse(row["TransactDate"].ToString());
                }
                if (row["EntertainUse"] != null)
                {
                    model.EntertainUse = row["EntertainUse"].ToString();
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
