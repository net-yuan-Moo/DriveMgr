using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DriveMgr.IDAL;
using System.Data.SqlClient;
using System.Data;

namespace DriveMgr.SQLServerDAL
{
    public class TravelDAL:ITravelDAL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddTravel(Model.TravelModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_Travel(");
            strSql.Append("TravelAmount,TravelPerson,TravelUse,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark,TraveDate)");
            strSql.Append(" values (");
            strSql.Append("@TravelAmount,@TravelPerson,@TravelUse,@Remark,@CreatePerson,@CreateDate,@UpdatePerson,@UpdateDate,@DeleteMark,@TraveDate)");

            SqlParameter[] parameters = {
					new SqlParameter("@TravelAmount", SqlDbType.Money,8),
					new SqlParameter("@TravelPerson", SqlDbType.VarChar,50),
					new SqlParameter("@TravelUse", SqlDbType.VarChar,100),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1),
                    new SqlParameter("@TraveDate", SqlDbType.DateTime)};
            parameters[0].Value = model.TravelAmount;
            parameters[1].Value = model.TravelPerson;
            parameters[2].Value = model.TravelUse;
            parameters[3].Value = model.Remark;
            parameters[4].Value = model.CreatePerson;
            parameters[5].Value = model.CreateDate;
            parameters[6].Value = model.UpdatePerson;
            parameters[7].Value = model.UpdateDate;
            parameters[8].Value = model.DeleteMark;
            parameters[9].Value = model.TraveDate;

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
        public bool UpdateTravel(Model.TravelModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Travel set ");
            strSql.Append("TravelAmount=@TravelAmount,");
            strSql.Append("TravelPerson=@TravelPerson,");
            strSql.Append("TravelUse=@TravelUse,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CreatePerson=@CreatePerson,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("UpdatePerson=@UpdatePerson,");
            strSql.Append("UpdateDate=@UpdateDate,");
            strSql.Append("DeleteMark=@DeleteMark,");
            strSql.Append("TraveDate=@TraveDate");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@TravelAmount", SqlDbType.Money,8),
					new SqlParameter("@TravelPerson", SqlDbType.VarChar,50),
					new SqlParameter("@TravelUse", SqlDbType.VarChar,100),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1),
                    new SqlParameter("@TraveDate", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.TravelAmount;
            parameters[1].Value = model.TravelPerson;
            parameters[2].Value = model.TravelUse;
            parameters[3].Value = model.Remark;
            parameters[4].Value = model.CreatePerson;
            parameters[5].Value = model.CreateDate;
            parameters[6].Value = model.UpdatePerson;
            parameters[7].Value = model.UpdateDate;
            parameters[8].Value = model.DeleteMark;
            parameters[9].Value = model.TraveDate;
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
        public bool DeleteTravel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_Travel Set DeleteMark = 1 ");
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
        public bool DeleteTravelList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_Travel Set DeleteMark = 1 ");
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
        public Model.TravelModel GetTravelModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,TravelAmount,TravelPerson,TravelUse,TraveDate,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark from tb_Travel ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            Model.TravelModel model = new Model.TravelModel();
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
        private Model.TravelModel DataRowToModel(DataRow row)
        {
            Model.TravelModel model = new Model.TravelModel();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["TravelAmount"] != null && row["TravelAmount"].ToString() != "")
                {
                    model.TravelAmount = decimal.Parse(row["TravelAmount"].ToString());
                }
                if (row["TravelPerson"] != null)
                {
                    model.TravelPerson = row["TravelPerson"].ToString();
                }
                if (row["TravelUse"] != null)
                {
                    model.TravelUse = row["TravelUse"].ToString();
                }
                if (row["TraveDate"] != null && row["TraveDate"].ToString() != "")
                {
                    model.TraveDate = DateTime.Parse(row["TraveDate"].ToString());
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
