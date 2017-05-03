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
    public class OutStoreHouseDetailsDAL:IOutStoreHouseDetailsDAL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddOutStoreHouseDetails(OutStoreHouseDetailsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_OutStoreHouseDetails(");
            strSql.Append("OutDetailsSN,GoodsID,OutStoreHouseID,OutQuantity,DeleteMark)");
            strSql.Append(" values (");
            strSql.Append("@OutDetailsSN,@GoodsID,@OutStoreHouseID,@OutQuantity,@DeleteMark)");
            
            SqlParameter[] parameters = {
					new SqlParameter("@OutDetailsSN", SqlDbType.VarChar,50),
					new SqlParameter("@GoodsID", SqlDbType.Int,4),
					new SqlParameter("@OutStoreHouseID", SqlDbType.Int,4),
					new SqlParameter("@OutQuantity", SqlDbType.Int,4),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1)};
            parameters[0].Value = model.OutDetailsSN;
            parameters[1].Value = model.GoodsID;
            parameters[2].Value = model.OutStoreHouseID;
            parameters[3].Value = model.OutQuantity;
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
        public bool UpdateOutStoreHouseDetails(OutStoreHouseDetailsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_OutStoreHouseDetails set ");
            strSql.Append("OutDetailsSN=@OutDetailsSN,");
            strSql.Append("GoodsID=@GoodsID,");
            strSql.Append("OutStoreHouseID=@OutStoreHouseID,");
            strSql.Append("OutQuantity=@OutQuantity,");
            strSql.Append("DeleteMark=@DeleteMark");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@OutDetailsSN", SqlDbType.VarChar,50),
					new SqlParameter("@GoodsID", SqlDbType.Int,4),
					new SqlParameter("@OutStoreHouseID", SqlDbType.Int,4),
					new SqlParameter("@OutQuantity", SqlDbType.Int,4),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.OutDetailsSN;
            parameters[1].Value = model.GoodsID;
            parameters[2].Value = model.OutStoreHouseID;
            parameters[3].Value = model.OutQuantity;
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
        public bool DeleteOutStoreHouseDetails(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_OutStoreHouseDetails Set DeleteMark = 1 ");
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
        public bool DeleteOutStoreHouseDetailsList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_OutStoreHouseDetails Set DeleteMark = 1 ");
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
        public OutStoreHouseDetailsModel GetOutStoreHouseDetailsModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,OutDetailsSN,GoodsID,OutStoreHouseID,OutQuantity,DeleteMark from tb_OutStoreHouseDetails ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            OutStoreHouseDetailsModel model = new OutStoreHouseDetailsModel();
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
        private OutStoreHouseDetailsModel DataRowToModel(DataRow row)
        {
            OutStoreHouseDetailsModel model = new OutStoreHouseDetailsModel();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["OutDetailsSN"] != null)
                {
                    model.OutDetailsSN = row["OutDetailsSN"].ToString();
                }
                if (row["GoodsID"] != null && row["GoodsID"].ToString() != "")
                {
                    model.GoodsID = int.Parse(row["GoodsID"].ToString());
                }
                if (row["OutStoreHouseID"] != null && row["OutStoreHouseID"].ToString() != "")
                {
                    model.OutStoreHouseID = int.Parse(row["OutStoreHouseID"].ToString());
                }
                if (row["OutQuantity"] != null && row["OutQuantity"].ToString() != "")
                {
                    model.OutQuantity = int.Parse(row["OutQuantity"].ToString());
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
