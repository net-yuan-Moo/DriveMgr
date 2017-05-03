using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DriveMgr.IDAL;
using DriveMgr.Model;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DriveMgr.SQLServerDAL
{
    public class GoodsDAL:IGoodsDAL
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool IsExistGoods(string goodsName)
        {
            string strSql = "select id from tb_Goods Where DeleteMark = 0 and GoodsName='" + goodsName + "'";
            DataTable dt = DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.Text, strSql);

            if (dt.Rows.Count > 0)
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
        public bool AddGoods(GoodsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_Goods(");
            strSql.Append("GoodsName,GoodsCategoryID,MinQuantity,MaxQuantity,RealQuantity,Specification,HandlePerson,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark)");
            strSql.Append(" values (");
            strSql.Append("@GoodsName,@GoodsCategoryID,@MinQuantity,@MaxQuantity,@RealQuantity,@Specification,@HandlePerson,@Remark,@CreatePerson,@CreateDate,@UpdatePerson,@UpdateDate,@DeleteMark)");
            
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsName", SqlDbType.VarChar,50),
					new SqlParameter("@GoodsCategoryID", SqlDbType.Int,4),
					new SqlParameter("@MinQuantity", SqlDbType.Int,4),
					new SqlParameter("@MaxQuantity", SqlDbType.Int,4),
					new SqlParameter("@RealQuantity", SqlDbType.Int,4),
					new SqlParameter("@Specification", SqlDbType.VarChar,50),
					new SqlParameter("@HandlePerson", SqlDbType.VarChar,50),
					new SqlParameter("@Remark", SqlDbType.VarChar,50),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1)};
            parameters[0].Value = model.GoodsName;
            parameters[1].Value = model.GoodsCategoryID;
            parameters[2].Value = model.MinQuantity;
            parameters[3].Value = model.MaxQuantity;
            parameters[4].Value = model.RealQuantity;
            parameters[5].Value = model.Specification;
            parameters[6].Value = model.HandlePerson;
            parameters[7].Value = model.Remark;
            parameters[8].Value = model.CreatePerson;
            parameters[9].Value = model.CreateDate;
            parameters[10].Value = model.UpdatePerson;
            parameters[11].Value = model.UpdateDate;
            parameters[12].Value = model.DeleteMark;

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
        public bool UpdateGoods(GoodsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Goods set ");
            strSql.Append("GoodsName=@GoodsName,");
            strSql.Append("GoodsCategoryID=@GoodsCategoryID,");
            strSql.Append("MinQuantity=@MinQuantity,");
            strSql.Append("MaxQuantity=@MaxQuantity,");
            strSql.Append("RealQuantity=@RealQuantity,");
            strSql.Append("Specification=@Specification,");
            strSql.Append("HandlePerson=@HandlePerson,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CreatePerson=@CreatePerson,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("UpdatePerson=@UpdatePerson,");
            strSql.Append("UpdateDate=@UpdateDate,");
            strSql.Append("DeleteMark=@DeleteMark");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@GoodsName", SqlDbType.VarChar,50),
					new SqlParameter("@GoodsCategoryID", SqlDbType.Int,4),
					new SqlParameter("@MinQuantity", SqlDbType.Int,4),
					new SqlParameter("@MaxQuantity", SqlDbType.Int,4),
					new SqlParameter("@RealQuantity", SqlDbType.Int,4),
					new SqlParameter("@Specification", SqlDbType.VarChar,50),
					new SqlParameter("@HandlePerson", SqlDbType.VarChar,50),
					new SqlParameter("@Remark", SqlDbType.VarChar,50),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.GoodsName;
            parameters[1].Value = model.GoodsCategoryID;
            parameters[2].Value = model.MinQuantity;
            parameters[3].Value = model.MaxQuantity;
            parameters[4].Value = model.RealQuantity;
            parameters[5].Value = model.Specification;
            parameters[6].Value = model.HandlePerson;
            parameters[7].Value = model.Remark;
            parameters[8].Value = model.CreatePerson;
            parameters[9].Value = model.CreateDate;
            parameters[10].Value = model.UpdatePerson;
            parameters[11].Value = model.UpdateDate;
            parameters[12].Value = model.DeleteMark;
            parameters[13].Value = model.Id;

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
        public bool DeleteGoods(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_Goods Set DeleteMark = 1 ");
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
        public bool DeleteGoodsList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_Goods Set DeleteMark = 1 ");
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
        public GoodsModel GetGoodsModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,GoodsName,GoodsCategoryID,MinQuantity,MaxQuantity,RealQuantity,Specification,HandlePerson,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark from tb_Goods ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            GoodsModel model = new GoodsModel();
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
        private GoodsModel DataRowToModel(DataRow row)
        {
            GoodsModel model = new GoodsModel();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["GoodsName"] != null)
                {
                    model.GoodsName = row["GoodsName"].ToString();
                }
                if (row["GoodsCategoryID"] != null && row["GoodsCategoryID"].ToString() != "")
                {
                    model.GoodsCategoryID = int.Parse(row["GoodsCategoryID"].ToString());
                }
                if (row["MinQuantity"] != null && row["MinQuantity"].ToString() != "")
                {
                    model.MinQuantity = int.Parse(row["MinQuantity"].ToString());
                }
                if (row["MaxQuantity"] != null && row["MaxQuantity"].ToString() != "")
                {
                    model.MaxQuantity = int.Parse(row["MaxQuantity"].ToString());
                }
                if (row["RealQuantity"] != null && row["RealQuantity"].ToString() != "")
                {
                    model.RealQuantity = int.Parse(row["RealQuantity"].ToString());
                }
                if (row["Specification"] != null)
                {
                    model.Specification = row["Specification"].ToString();
                }
                if (row["HandlePerson"] != null)
                {
                    model.HandlePerson = row["HandlePerson"].ToString();
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
        /// 获取物资类别集合
        /// </summary>
        /// <returns></returns>
        public string GetGoodsDT()
        {
            string strSql = "select Id,GoodsName from tb_Goods Where DeleteMark = 0 ";
            DataTable dt = DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.Text, strSql);
            return DriveMgr.Common.JsonHelper.ToJson(dt);
        }
    }
}
