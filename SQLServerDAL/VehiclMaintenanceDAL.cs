using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DriveMgr.IDAL;
using System.Data.SqlClient;
using System.Data;

namespace DriveMgr.SQLServerDAL
{
    public class VehiclMaintenanceDAL:IVehiclMaintenanceDAL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddVehiclMaintenance(Model.VehiclMaintenanceModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_VehiclMaintenance(");
            strSql.Append("VehicleId,MaintenanceType,MaintenCosts,MaintenPerson,MaintenDate,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark)");
            strSql.Append(" values (");
            strSql.Append("@VehicleId,@MaintenanceType,@MaintenCosts,@MaintenPerson,@MaintenDate,@Remark,@CreatePerson,@CreateDate,@UpdatePerson,@UpdateDate,@DeleteMark)");

            SqlParameter[] parameters = {
					new SqlParameter("@VehicleId", SqlDbType.Int,4),
					new SqlParameter("@MaintenanceType", SqlDbType.Int,4),
					new SqlParameter("@MaintenCosts", SqlDbType.Money,8),
					new SqlParameter("@MaintenPerson", SqlDbType.VarChar,50),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1),
                    new SqlParameter("@MaintenDate", SqlDbType.DateTime)};
            parameters[0].Value = model.VehicleId;
            parameters[1].Value = model.MaintenanceType;
            parameters[2].Value = model.MaintenCosts;
            parameters[3].Value = model.MaintenPerson;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.CreatePerson;
            parameters[6].Value = model.CreateDate;
            parameters[7].Value = model.UpdatePerson;
            parameters[8].Value = model.UpdateDate;
            parameters[9].Value = model.DeleteMark;
            parameters[10].Value = model.MaintenDate;

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
        public bool UpdateVehiclMaintenance(Model.VehiclMaintenanceModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_VehiclMaintenance set ");
            strSql.Append("VehicleId=@VehicleId,");
            strSql.Append("MaintenanceType=@MaintenanceType,");
            strSql.Append("MaintenCosts=@MaintenCosts,");
            strSql.Append("MaintenPerson=@MaintenPerson,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CreatePerson=@CreatePerson,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("UpdatePerson=@UpdatePerson,");
            strSql.Append("UpdateDate=@UpdateDate,");
            strSql.Append("DeleteMark=@DeleteMark, ");
            strSql.Append("MaintenDate=@MaintenDate ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@VehicleId", SqlDbType.Int,4),
					new SqlParameter("@MaintenanceType", SqlDbType.Int,4),
					new SqlParameter("@MaintenCosts", SqlDbType.Money,8),
					new SqlParameter("@MaintenPerson", SqlDbType.VarChar,50),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1),
                    new SqlParameter("@MaintenDate", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.VehicleId;
            parameters[1].Value = model.MaintenanceType;
            parameters[2].Value = model.MaintenCosts;
            parameters[3].Value = model.MaintenPerson;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.CreatePerson;
            parameters[6].Value = model.CreateDate;
            parameters[7].Value = model.UpdatePerson;
            parameters[8].Value = model.UpdateDate;
            parameters[9].Value = model.DeleteMark;
            parameters[10].Value = model.MaintenDate;
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
        public bool DeleteVehiclMaintenance(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_VehiclMaintenance Set DeleteMark = 1 ");
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
        public bool DeleteVehiclMaintenanceList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_VehiclMaintenance Set DeleteMark = 1 ");
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
        public Model.VehiclMaintenanceModel GetVehiclMaintenanceModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,VehicleId,MaintenanceType,MaintenCosts,MaintenDate,MaintenPerson,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark from tb_VehiclMaintenance ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            Model.VehiclMaintenanceModel model = new Model.VehiclMaintenanceModel();
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
        private Model.VehiclMaintenanceModel DataRowToModel(DataRow row)
        {
            Model.VehiclMaintenanceModel model = new Model.VehiclMaintenanceModel();
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
                if (row["MaintenanceType"] != null && row["MaintenanceType"].ToString() != "")
                {
                    model.MaintenanceType = int.Parse(row["MaintenanceType"].ToString());
                }
                if (row["MaintenCosts"] != null && row["MaintenCosts"].ToString() != "")
                {
                    model.MaintenCosts = decimal.Parse(row["MaintenCosts"].ToString());
                }
                if (row["MaintenPerson"] != null)
                {
                    model.MaintenPerson = row["MaintenPerson"].ToString();
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
                if (row["MaintenDate"] != null && row["MaintenDate"].ToString() != "")
                {
                    model.MaintenDate = DateTime.Parse(row["MaintenDate"].ToString());
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
