/***********************
 @ author:zlong
 @ Date:2015-01-08
 @ Desc:办公费用管理数据层
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
    public class OfficeDAL:IOfficeDAL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddOffice(Model.OfficeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_Office(");
            strSql.Append("OfficeAmount,TagPerson,OfficeUse,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark,UseDate)");
            strSql.Append(" values (");
            strSql.Append("@OfficeAmount,@TagPerson,@OfficeUse,@Remark,@CreatePerson,@CreateDate,@UpdatePerson,@UpdateDate,@DeleteMark,@UseDate)");

            SqlParameter[] parameters = {
					new SqlParameter("@OfficeAmount", SqlDbType.Money,8),
					new SqlParameter("@TagPerson", SqlDbType.VarChar,50),
					new SqlParameter("@OfficeUse", SqlDbType.VarChar,100),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1),
                    new SqlParameter("@UseDate", SqlDbType.DateTime)};
            parameters[0].Value = model.OfficeAmount;
            parameters[1].Value = model.TagPerson;
            parameters[2].Value = model.OfficeUse;
            parameters[3].Value = model.Remark;
            parameters[4].Value = model.CreatePerson;
            parameters[5].Value = model.CreateDate;
            parameters[6].Value = model.UpdatePerson;
            parameters[7].Value = model.UpdateDate;
            parameters[8].Value = model.DeleteMark;
            parameters[9].Value = model.UseDate;

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
        public bool UpdateOffice(Model.OfficeModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Office set ");
            strSql.Append("OfficeAmount=@OfficeAmount,");
            strSql.Append("TagPerson=@TagPerson,");
            strSql.Append("OfficeUse=@OfficeUse,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CreatePerson=@CreatePerson,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("UpdatePerson=@UpdatePerson,");
            strSql.Append("UpdateDate=@UpdateDate,");
            strSql.Append("DeleteMark=@DeleteMark,");
            strSql.Append("UseDate=@UseDate");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@OfficeAmount", SqlDbType.Money,8),
					new SqlParameter("@TagPerson", SqlDbType.VarChar,50),
					new SqlParameter("@OfficeUse", SqlDbType.VarChar,100),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1),
                    new SqlParameter("@UseDate", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.OfficeAmount;
            parameters[1].Value = model.TagPerson;
            parameters[2].Value = model.OfficeUse;
            parameters[3].Value = model.Remark;
            parameters[4].Value = model.CreatePerson;
            parameters[5].Value = model.CreateDate;
            parameters[6].Value = model.UpdatePerson;
            parameters[7].Value = model.UpdateDate;
            parameters[8].Value = model.DeleteMark;
            parameters[9].Value = model.UseDate;
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
        public bool DeleteOffice(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_Office Set DeleteMark = 1 ");
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
        public bool DeleteOfficeList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_Office Set DeleteMark = 1 ");
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
        public Model.OfficeModel GetOfficeModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,OfficeAmount,TagPerson,OfficeUse,UseDate,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark from tb_Office ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            Model.OfficeModel model = new Model.OfficeModel();
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
        private Model.OfficeModel DataRowToModel(DataRow row)
        {
            Model.OfficeModel model = new Model.OfficeModel();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["OfficeAmount"] != null && row["OfficeAmount"].ToString() != "")
                {
                    model.OfficeAmount = decimal.Parse(row["OfficeAmount"].ToString());
                }
                if (row["TagPerson"] != null)
                {
                    model.TagPerson = row["TagPerson"].ToString();
                }
                if (row["OfficeUse"] != null)
                {
                    model.OfficeUse = row["OfficeUse"].ToString();
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
                if (row["UseDate"] != null && row["UseDate"].ToString() != "")
                {
                    model.UseDate = DateTime.Parse(row["UseDate"].ToString());
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
