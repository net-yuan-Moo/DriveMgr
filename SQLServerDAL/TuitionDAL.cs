using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DriveMgr.IDAL;
using System.Data.SqlClient;
using System.Data;
using DriveMgr.Model;

namespace DriveMgr.SQLServerDAL
{
    public class TuitionDAL:ITuitionDAL
    {
        /// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool IsExistTuition(int localType)
		{
            string strSql = "select count(id) from tb_Tuition Where DeleteMark = 0 and LocalType=" + localType;
            object obj = DriveMgr.Common.SqlHelper.ExecuteScalar(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.Text, strSql);

            if (Int32.Parse(obj.ToString()) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }     
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public bool AddTuition(TuitionModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_Tuition(");
			strSql.Append("Costs,LocalType,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark)");
			strSql.Append(" values (");
			strSql.Append("@Costs,@LocalType,@Remark,@CreatePerson,@CreateDate,@UpdatePerson,@UpdateDate,@DeleteMark)");
			SqlParameter[] parameters = {
					new SqlParameter("@Costs", SqlDbType.Money,8),
					new SqlParameter("@LocalType", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.VarChar,500),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1)};
			parameters[0].Value = model.Costs;
			parameters[1].Value = model.LocalType;
			parameters[2].Value = model.Remark;
			parameters[3].Value = model.CreatePerson;
			parameters[4].Value = model.CreateDate;
			parameters[5].Value = model.UpdatePerson;
			parameters[6].Value = model.UpdateDate;
			parameters[7].Value = model.DeleteMark;

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
        public bool UpdateTuition(TuitionModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tb_Tuition set ");
			strSql.Append("Costs=@Costs,");
			strSql.Append("LocalType=@LocalType,");
			strSql.Append("Remark=@Remark,");
			strSql.Append("CreatePerson=@CreatePerson,");
			strSql.Append("CreateDate=@CreateDate,");
			strSql.Append("UpdatePerson=@UpdatePerson,");
			strSql.Append("UpdateDate=@UpdateDate,");
			strSql.Append("DeleteMark=@DeleteMark");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Costs", SqlDbType.Money,8),
					new SqlParameter("@LocalType", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.VarChar,500),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.Costs;
			parameters[1].Value = model.LocalType;
			parameters[2].Value = model.Remark;
			parameters[3].Value = model.CreatePerson;
			parameters[4].Value = model.CreateDate;
			parameters[5].Value = model.UpdatePerson;
			parameters[6].Value = model.UpdateDate;
			parameters[7].Value = model.DeleteMark;
			parameters[8].Value = model.Id;

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
        public bool DeleteTuition(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("Update tb_Tuition Set DeleteMark = 1 ");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = Id;

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
        public bool DeleteTuitionList(string Idlist)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("Update tb_Tuition Set DeleteMark = 1 ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
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
        public TuitionModel GetTuitionModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,Costs,LocalType,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark from tb_Tuition ");
			strSql.Append(" where Id=@Id ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)			};
			parameters[0].Value = Id;

			TuitionModel model=new TuitionModel();
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
		public TuitionModel DataRowToModel(DataRow row)
		{
			TuitionModel model=new TuitionModel();
			if (row != null)
			{
				if(row["Id"]!=null && row["Id"].ToString()!="")
				{
					model.Id=int.Parse(row["Id"].ToString());
				}
				if(row["Costs"]!=null && row["Costs"].ToString()!="")
				{
					model.Costs=decimal.Parse(row["Costs"].ToString());
				}
				if(row["LocalType"]!=null && row["LocalType"].ToString()!="")
				{
					model.LocalType=int.Parse(row["LocalType"].ToString());
				}
				if(row["Remark"]!=null)
				{
					model.Remark=row["Remark"].ToString();
				}
				if(row["CreatePerson"]!=null)
				{
					model.CreatePerson=row["CreatePerson"].ToString();
				}
				if(row["CreateDate"]!=null && row["CreateDate"].ToString()!="")
				{
					model.CreateDate=DateTime.Parse(row["CreateDate"].ToString());
				}
				if(row["UpdatePerson"]!=null)
				{
					model.UpdatePerson=row["UpdatePerson"].ToString();
				}
				if(row["UpdateDate"]!=null && row["UpdateDate"].ToString()!="")
				{
					model.UpdateDate=DateTime.Parse(row["UpdateDate"].ToString());
				}
				if(row["DeleteMark"]!=null && row["DeleteMark"].ToString()!="")
				{
					if((row["DeleteMark"].ToString()=="1")||(row["DeleteMark"].ToString().ToLower()=="true"))
					{
						model.DeleteMark=true;
					}
					else
					{
						model.DeleteMark=false;
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
