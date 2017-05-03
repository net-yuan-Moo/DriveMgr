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
    public class EnterStoreHouseDAL:IEnterStoreHouseDAL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddEnterStoreHouse(EnterStoreHouseModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_EnterStoreHouse(");
            strSql.Append("EnterSN,EnterDate,HandlePerson,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark)");
            strSql.Append(" values (");
            strSql.Append("@EnterSN,@EnterDate,@HandlePerson,@Remark,@CreatePerson,@CreateDate,@UpdatePerson,@UpdateDate,@DeleteMark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@EnterSN", SqlDbType.VarChar,50),
					new SqlParameter("@EnterDate", SqlDbType.DateTime),
					new SqlParameter("@HandlePerson", SqlDbType.VarChar,50),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1)};
            parameters[0].Value = model.EnterSN;
            parameters[1].Value = model.EnterDate;
            parameters[2].Value = model.HandlePerson;
            parameters[3].Value = model.Remark;
            parameters[4].Value = model.CreatePerson;
            parameters[5].Value = model.CreateDate;
            parameters[6].Value = model.UpdatePerson;
            parameters[7].Value = model.UpdateDate;
            parameters[8].Value = model.DeleteMark;

            return Convert.ToInt32(DriveMgr.Common.SqlHelper.ExecuteScalar(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.Text, strSql.ToString(), parameters));
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateEnterStoreHouse(EnterStoreHouseModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_EnterStoreHouse set ");
            strSql.Append("EnterSN=@EnterSN,");
            strSql.Append("EnterDate=@EnterDate,");
            strSql.Append("HandlePerson=@HandlePerson,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CreatePerson=@CreatePerson,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("UpdatePerson=@UpdatePerson,");
            strSql.Append("UpdateDate=@UpdateDate,");
            strSql.Append("DeleteMark=@DeleteMark");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@EnterSN", SqlDbType.VarChar,50),
					new SqlParameter("@EnterDate", SqlDbType.DateTime),
					new SqlParameter("@HandlePerson", SqlDbType.VarChar,50),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.EnterSN;
            parameters[1].Value = model.EnterDate;
            parameters[2].Value = model.HandlePerson;
            parameters[3].Value = model.Remark;
            parameters[4].Value = model.CreatePerson;
            parameters[5].Value = model.CreateDate;
            parameters[6].Value = model.UpdatePerson;
            parameters[7].Value = model.UpdateDate;
            parameters[8].Value = model.DeleteMark;
            parameters[9].Value = model.Id;

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
        public bool DeleteEnterStoreHouse(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_EnterStoreHouse Set DeleteMark = 1 ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            //List<string> sqlList = new List<string>();
            //sqlList.Add(@"Update tb_EnterStoreHouse Set DeleteMark = 1 Where Id = " + id);
            //sqlList.Add(@"Update tb_EnterStoreHouseDetails Set DeleteMark = 1 Where EnterStoreHouseID = " + id);
            //object obj = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.financialMgrConn, sqlList);

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
        public bool DeleteEnterStoreHouseList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_EnterStoreHouse Set DeleteMark = 1 ");
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
        public EnterStoreHouseModel GetEnterStoreHouseModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,EnterSN,EnterDate,HandlePerson,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark from tb_EnterStoreHouse ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            EnterStoreHouseModel model = new EnterStoreHouseModel();
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
        private EnterStoreHouseModel DataRowToModel(DataRow row)
        {
            EnterStoreHouseModel model = new EnterStoreHouseModel();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["EnterSN"] != null)
                {
                    model.EnterSN = row["EnterSN"].ToString();
                }
                if (row["EnterDate"] != null && row["EnterDate"].ToString() != "")
                {
                    model.EnterDate = DateTime.Parse(row["EnterDate"].ToString());
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
        /// 获取入库单集合
        /// </summary>
        /// <returns></returns>
        public string GetEnterStoreHouseDT()
        {
            string strSql = "select Id,EnterSN from tb_EnterStoreHouse Where DeleteMark = 0 ";
            DataTable dt = DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.Text, strSql);
            return DriveMgr.Common.JsonHelper.ToJson(dt);
        }

    }
}
