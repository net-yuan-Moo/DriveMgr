using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DriveMgr.IDAL;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace DriveMgr.SQLServerDAL
{
    public class VehicleDAL:IVehicleDAL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddVehicle(Model.VehicleModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_Vehicle(");
            strSql.Append("LicencePlateNum,Brands,CarModel,BuyPrice,BuyDate,Owner,Remark,Status,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark)");
            strSql.Append(" values (");
            strSql.Append("@LicencePlateNum,@Brands,@CarModel,@BuyPrice,@BuyDate,@Owner,@Remark,@Status,@CreatePerson,@CreateDate,@UpdatePerson,@UpdateDate,@DeleteMark)");

            SqlParameter[] parameters = {
					new SqlParameter("@LicencePlateNum", SqlDbType.VarChar,50),
					new SqlParameter("@Brands", SqlDbType.VarChar,50),
					new SqlParameter("@CarModel", SqlDbType.VarChar,50),
					new SqlParameter("@BuyPrice", SqlDbType.Money,8),
					new SqlParameter("@BuyDate", SqlDbType.DateTime),
					new SqlParameter("@Owner", SqlDbType.VarChar,50),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1)};
            parameters[0].Value = model.LicencePlateNum;
            parameters[1].Value = model.Brands;
            parameters[2].Value = model.CarModel;
            parameters[3].Value = model.BuyPrice;
            parameters[4].Value = model.BuyDate;
            parameters[5].Value = model.Owner;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.Status;
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
        public bool UpdateVehicle(Model.VehicleModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Vehicle set ");
            strSql.Append("LicencePlateNum=@LicencePlateNum,");
            strSql.Append("Brands=@Brands,");
            strSql.Append("CarModel=@CarModel,");
            strSql.Append("BuyPrice=@BuyPrice,");
            strSql.Append("BuyDate=@BuyDate,");
            strSql.Append("Owner=@Owner,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("Status=@Status,");
            strSql.Append("CreatePerson=@CreatePerson,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("UpdatePerson=@UpdatePerson,");
            strSql.Append("UpdateDate=@UpdateDate,");
            strSql.Append("DeleteMark=@DeleteMark");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@LicencePlateNum", SqlDbType.VarChar,50),
					new SqlParameter("@Brands", SqlDbType.VarChar,50),
					new SqlParameter("@CarModel", SqlDbType.VarChar,50),
					new SqlParameter("@BuyPrice", SqlDbType.Money,8),
					new SqlParameter("@BuyDate", SqlDbType.DateTime),
					new SqlParameter("@Owner", SqlDbType.VarChar,50),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@UpdatePerson", SqlDbType.VarChar,50),
					new SqlParameter("@UpdateDate", SqlDbType.DateTime),
					new SqlParameter("@DeleteMark", SqlDbType.Bit,1),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.LicencePlateNum;
            parameters[1].Value = model.Brands;
            parameters[2].Value = model.CarModel;
            parameters[3].Value = model.BuyPrice;
            parameters[4].Value = model.BuyDate;
            parameters[5].Value = model.Owner;
            parameters[6].Value = model.Remark;
            parameters[7].Value = model.Status;
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
        public bool DeleteVehicle(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_Vehicle Set DeleteMark = 1 ");
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
        public bool DeleteVehicleList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update tb_Vehicle Set DeleteMark = 1 ");
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
        public Model.VehicleModel GetVehicleModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,LicencePlateNum,Brands,CarModel,BuyPrice,BuyDate,Owner,Remark,Status,CreatePerson,CreateDate,UpdatePerson,UpdateDate,DeleteMark from tb_Vehicle ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            Model.VehicleModel model = new Model.VehicleModel();
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
        private Model.VehicleModel DataRowToModel(DataRow row)
        {
            Model.VehicleModel model = new Model.VehicleModel();
            if (row != null)
            {
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["Brands"] != null)
                {
                    model.Brands = row["Brands"].ToString();
                }
                if (row["LicencePlateNum"] != null)
                {
                    model.LicencePlateNum = row["LicencePlateNum"].ToString();
                }
                if (row["CarModel"] != null)
                {
                    model.CarModel = row["CarModel"].ToString();
                }
                if (row["BuyPrice"] != null && row["BuyPrice"].ToString() != "")
                {
                    model.BuyPrice = decimal.Parse(row["BuyPrice"].ToString());
                }                
                if (row["BuyDate"] != null && row["BuyDate"].ToString() != "")
                {
                    model.BuyDate = DateTime.Parse(row["BuyDate"].ToString());
                }
                if (row["Owner"] != null)
                {
                    model.Owner = row["Owner"].ToString();
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
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
        /// 获取车牌号集合
        /// </summary>
        /// <returns></returns>
        public string GetVehicleDT()
        {
            string strSql = "select Id,LicencePlateNum from tb_Vehicle Where Status = 0 and DeleteMark = 0 ";            
            DataTable dt = DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.Text, strSql);
            return DriveMgr.Common.JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,LicencePlateNum ");
            strSql.Append(" FROM tb_Vehicle ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.Text, strSql.ToString(), null);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM tb_Vehicle ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = Convert.ToInt32(DriveMgr.Common.SqlHelper.ExecuteScalar(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.Text, strSql.ToString(), null));
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 记录分配车辆情况
        /// </summary>
        /// <param name="vehicleID"></param>
        /// <param name="dtStudents"></param>
        /// <param name="operater"></param>
        /// <returns></returns>
        public bool AddDistributeVehicle(int vehicleID,int subjectId, DataTable dtStudents, string operater)
        {
            if (dtStudents == null)
            {
                return false;
            }
            Hashtable ht = new Hashtable();
            List<SqlParameter[]> listArr = new List<SqlParameter[]>();
            for (int i = 0; i < dtStudents.Rows.Count; i++)
            {
                SqlParameter[] parameters = {
					new SqlParameter("@VehicleID", SqlDbType.Int),
                    new SqlParameter("@SubjectID", SqlDbType.Int),
					new SqlParameter("@StudentsID", SqlDbType.BigInt),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
                                            new SqlParameter("@Operater", SqlDbType.VarChar,50)};
                parameters[0].Value = vehicleID;
                parameters[1].Value = subjectId;
                parameters[2].Value = Int32.Parse(dtStudents.Rows[i]["StudentsID"].ToString());  //取学员ID
                parameters[3].Value = DateTime.Now;
                parameters[4].Value = operater;
                listArr.Add(parameters);

                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"if not exists(select 1 from tb_DistributionVehicle where
                               StudentsID=@StudentsID and SubjectID=@SubjectID) insert into tb_DistributionVehicle(");
                strSql.Append("StudentsID,SubjectID,VehicleID,CreateTime,Operater,DistributeVihicleStatus)");
                strSql.Append(" values (");
                strSql.Append("@StudentsID,@SubjectID,@VehicleID,@CreateTime,@Operater,1)");

                strSql.Append(" else ");
                strSql.Append(@"update tb_DistributionVehicle set VehicleID=@VehicleID,CreateTime=@CreateTime,Operater=@Operater,DistributeVihicleStatus=1
                                where StudentsID=@StudentsID and SubjectID=@SubjectID");

                ht.Add(strSql, listArr[i]);
            }

            bool result = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.connStrDriveData, ht);
            return result;
        }

        /// <summary>
        /// 修改分配车辆信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditDistributeStudents(Model.DistributionVehicleModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_DistributionVehicle set ");
            strSql.Append("VehicleID=@VehicleID,CreateTime=@CreateTime,Operater=@Operater,DistributeVihicleStatus=@DistributeVihicleStatus");
            strSql.Append(" where Id=@Id and SubjectID=@SubjectID");
            SqlParameter[] parameters = {
					new SqlParameter("@VehicleID", SqlDbType.Int),
					new SqlParameter("@Id", SqlDbType.Int),
					new SqlParameter("@SubjectID", SqlDbType.Int),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@Operater", SqlDbType.VarChar,50),
                    new SqlParameter("@DistributeVihicleStatus",SqlDbType.Int)
					};
            parameters[0].Value = model.VehicleID;
            parameters[1].Value = model.ID;
            parameters[2].Value = model.SubjectID;
            parameters[3].Value = model.CreateTime;
            parameters[4].Value = model.Operater;
            parameters[5].Value = model.DistributeVihicleStatus;
            
            object obj = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), parameters);
            if (Convert.ToInt32(obj) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }   
        }
    }
}
