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
    public class SiteRentalLocalDAL : ISiteRentalLocalDAL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddSiteRental(SiteRentalLocalModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_SiteRental_local(");
            strSql.Append("VehicleId,StudentsID,PriceConfigID,Longer,TotalPrice,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark,RentDate)");
            strSql.Append(" values (");
            strSql.Append("@VehicleId,@StudentsID,@PriceConfigID,@Longer,@TotalPrice,@Remark,@CreatePerson,@CreateDate,@UpdatePerson,@UpdateDate,@DeleteMark,@RentDate)");

            SqlParameter[] parameters = {
					new SqlParameter("@VehicleId", SqlDbType.Int,4),
					new SqlParameter("@StudentsID", SqlDbType.Int,4),
					new SqlParameter("@PriceConfigID", SqlDbType.Int,4),
					new SqlParameter("@Longer", SqlDbType.Float,8),
					new SqlParameter("@TotalPrice", SqlDbType.Money,8),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
                    new SqlParameter("@RentDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1)};
            parameters[0].Value = model.VehicleId;
            parameters[1].Value = model.StudentsID;
            parameters[2].Value = model.PriceConfigID;
            parameters[3].Value = model.Longer;
            parameters[4].Value = model.TotalPrice;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.CreatePerson;
            parameters[7].Value = model.CreateDate;
            parameters[8].Value = model.UpdatePerson;
            parameters[9].Value = model.UpdateDate;
            parameters[10].Value = model.RentDate;
            parameters[11].Value = model.DeleteMark;

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
        public bool UpdateSiteRental(SiteRentalLocalModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_SiteRental_local set ");
            strSql.Append("VehicleId=@VehicleId,");
            strSql.Append("StudentsID=@StudentsID,");
            strSql.Append("PriceConfigID=@PriceConfigID,");
            strSql.Append("Longer=@Longer,");
            strSql.Append("TotalPrice=@TotalPrice,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CreatePerson=@CreatePerson,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("UpdatePerson=@UpdatePerson,");
            strSql.Append("UpdateDate=@UpdateDate,");
            strSql.Append("RentDate=@UpdateDate, ");
            strSql.Append("DeleteMark=@DeleteMark");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@VehicleId", SqlDbType.Int,4),
					new SqlParameter("@StudentsID", SqlDbType.Int,4),
					new SqlParameter("@PriceConfigID", SqlDbType.Int,4),
					new SqlParameter("@Longer", SqlDbType.Float,8),
					new SqlParameter("@TotalPrice", SqlDbType.Money,8),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
                    new SqlParameter("@RentDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.VehicleId;
            parameters[1].Value = model.StudentsID;
            parameters[2].Value = model.PriceConfigID;
            parameters[3].Value = model.Longer;
            parameters[4].Value = model.TotalPrice;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.CreatePerson;
            parameters[7].Value = model.CreateDate;
            parameters[8].Value = model.UpdatePerson;
            parameters[9].Value = model.UpdateDate;
            parameters[10].Value = model.RentDate;
            parameters[11].Value = model.DeleteMark;
            parameters[12].Value = model.Id;

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
        public bool DeleteSiteRental(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_SiteRental_local Set DeleteMark = 1 ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
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
        public bool DeleteSiteRentalList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_SiteRental_local Set DeleteMark = 1 ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
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
        public SiteRentalLocalModel GetSiteRentalModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,VehicleId,StudentsID,RentDate,PriceConfigID,Longer,TotalPrice,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark from tb_SiteRental_local ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            SiteRentalLocalModel model = new SiteRentalLocalModel();
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
        private SiteRentalLocalModel DataRowToModel(DataRow row)
        {
            SiteRentalLocalModel model = new SiteRentalLocalModel();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["VehicleId"] != null && row["VehicleId"].ToString() != "")
                {
                    model.VehicleId = int.Parse(row["VehicleId"].ToString());
                }
                if (row["StudentsID"] != null)
                {
                    model.StudentsID = Int32.Parse(row["StudentsID"].ToString());
                }
                if (row["PriceConfigID"] != null && row["PriceConfigID"].ToString() != "")
                {
                    model.PriceConfigID = int.Parse(row["PriceConfigID"].ToString());
                }
                if (row["Longer"] != null && row["Longer"].ToString() != "")
                {
                    model.Longer = decimal.Parse(row["Longer"].ToString());
                }
                if (row["TotalPrice"] != null && row["TotalPrice"].ToString() != "")
                {
                    model.TotalPrice = decimal.Parse(row["TotalPrice"].ToString());
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
                if (row["RentDate"] != null && row["RentDate"].ToString() != "")
                {
                    model.RentDate = DateTime.Parse(row["RentDate"].ToString());
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
