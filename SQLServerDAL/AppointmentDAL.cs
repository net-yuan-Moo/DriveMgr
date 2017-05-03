using DriveMgr.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DriveMgr.SQLServerDAL
{
    public class AppointmentDAL : IAppointmentDAL
    {
        public string GetPager(string tableName, string columns, string order, int pageSize, int pageIndex, string where, out int totalCount)
        {
            DataTable dt = DriveMgr.Common.SqlPagerHelper.GetPagerOfDrive(tableName, columns, order, pageSize, pageIndex, where, out totalCount);
            return DriveMgr.Common.JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DriveMgr.Model.AppointmentModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Appointment set ");
            strSql.Append("AppointDate=@AppointDate,");
            strSql.Append("AppointStatus=@AppointStatus,");
            strSql.Append("Operater=@Operater,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@SubjectID", SqlDbType.Int,4),
					new SqlParameter("@AppointDate", SqlDbType.DateTime),
					new SqlParameter("@AppointStatus", SqlDbType.Int,4),
					new SqlParameter("@Operater", SqlDbType.VarChar,50),
                    new SqlParameter("@Remark", SqlDbType.VarChar,50),
					new SqlParameter("@ID", SqlDbType.BigInt,8)};

            parameters[0].Value = model.SubjectID;
            parameters[1].Value = model.AppointDate;
            parameters[2].Value = model.AppointStatus;
            parameters[3].Value = model.Operater;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.ID;

            int rows = Convert.ToInt32(DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), parameters));
            if (rows > 0)
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
