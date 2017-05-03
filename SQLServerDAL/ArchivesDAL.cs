using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DriveMgr.IDAL;
using System.Data.SqlClient;

namespace DriveMgr.SQLServerDAL
{
    public class ArchivesDAL : IArchivesDAL
    {
        public string GetPager(string tableName, string columns, string order, int pageSize, int pageIndex, string where, out int totalCount)
        {
            DataTable dt = DriveMgr.Common.SqlPagerHelper.GetPagerOfDrive(tableName, columns, order, pageSize, pageIndex, where, out totalCount);
            return DriveMgr.Common.JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DriveMgr.Model.ArchivesModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Archives set ");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@Remark", SqlDbType.VarChar,500),
					new SqlParameter("@ID", SqlDbType.BigInt,8)};

            parameters[0].Value = model.Remark;
            parameters[1].Value = model.ID;

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
