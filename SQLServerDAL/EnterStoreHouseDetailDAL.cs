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
    public class EnterStoreHouseDetailDAL:IEnterStoreHouseDetailDAL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddEnterStoreHouseDetails(EnterStoreHouseDetailsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_EnterStoreHouseDetails(");
            strSql.Append("EnterDetailsSN,GoodsID,EnterQuantity,EnterStoreHouseID,DeleteMark)");
            strSql.Append(" values (");
            strSql.Append("@EnterDetailsSN,@GoodsID,@EnterQuantity,@EnterStoreHouseID,@DeleteMark)");
            
            SqlParameter[] parameters = {
					new SqlParameter("@EnterDetailsSN", SqlDbType.VarChar,50),
					new SqlParameter("@GoodsID", SqlDbType.Int,4),
					new SqlParameter("@EnterQuantity", SqlDbType.Int,4),
					new SqlParameter("@EnterStoreHouseID", SqlDbType.Int,4),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1)};
            parameters[0].Value = model.EnterDetailsSN;
            parameters[1].Value = model.GoodsID;
            parameters[2].Value = model.EnterQuantity;
            parameters[3].Value = model.EnterStoreHouseID;
            parameters[4].Value = model.DeleteMark;

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
        public bool UpdateEnterStoreHouseDetails(EnterStoreHouseDetailsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_EnterStoreHouseDetails set ");
            strSql.Append("EnterDetailsSN=@EnterDetailsSN,");
            strSql.Append("GoodsID=@GoodsID,");
            strSql.Append("EnterQuantity=@EnterQuantity,");
            strSql.Append("EnterStoreHouseID=@EnterStoreHouseID,");
            strSql.Append("DeleteMark=@DeleteMark");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@EnterDetailsSN", SqlDbType.VarChar,50),
					new SqlParameter("@GoodsID", SqlDbType.Int,4),
					new SqlParameter("@EnterQuantity", SqlDbType.Int,4),
					new SqlParameter("@EnterStoreHouseID", SqlDbType.Int,4),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.EnterDetailsSN;
            parameters[1].Value = model.GoodsID;
            parameters[2].Value = model.EnterQuantity;
            parameters[3].Value = model.EnterStoreHouseID;
            parameters[4].Value = model.DeleteMark;
            parameters[5].Value = model.Id;

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
        public bool DeleteEnterStoreHouseDetails(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_EnterStoreHouseDetails Set DeleteMark = 1 ");
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
        public bool DeleteEnterStoreHouseDetailsList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_EnterStoreHouseDetails Set DeleteMark = 1 ");
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
        public EnterStoreHouseDetailsModel GetEnterStoreHouseDetailsModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,EnterDetailsSN,GoodsID,EnterQuantity,EnterStoreHouseID,DeleteMark from tb_EnterStoreHouseDetails ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            EnterStoreHouseDetailsModel model = new EnterStoreHouseDetailsModel();
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
        private EnterStoreHouseDetailsModel DataRowToModel(DataRow row)
        {
            EnterStoreHouseDetailsModel model = new EnterStoreHouseDetailsModel();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["EnterDetailsSN"] != null)
                {
                    model.EnterDetailsSN = row["EnterDetailsSN"].ToString();
                }
                if (row["GoodsID"] != null && row["GoodsID"].ToString() != "")
                {
                    model.GoodsID = int.Parse(row["GoodsID"].ToString());
                }
                if (row["EnterQuantity"] != null && row["EnterQuantity"].ToString() != "")
                {
                    model.EnterQuantity = int.Parse(row["EnterQuantity"].ToString());
                }
                if (row["EnterStoreHouseID"] != null && row["EnterStoreHouseID"].ToString() != "")
                {
                    model.EnterStoreHouseID = int.Parse(row["EnterStoreHouseID"].ToString());
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
