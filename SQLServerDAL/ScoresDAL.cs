using DriveMgr.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DriveMgr.SQLServerDAL
{
    public class ScoresDAL : IScoresDAL
    {
        public string GetPager(string tableName, string columns, string order, int pageSize, int pageIndex, string where, out int totalCount)
        {
            DataTable dt = DriveMgr.Common.SqlPagerHelper.GetPagerOfDrive(tableName, columns, order, pageSize, pageIndex, where, out totalCount);
            return DriveMgr.Common.JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DriveMgr.Model.ScoresModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Scores set ");
            strSql.Append("ScoreOne=@ScoreOne,");
            strSql.Append("OneStatus=@OneStatus,");
            strSql.Append("ScoreTwo=@ScoreTwo,");
            strSql.Append("TwoStatus=@TwoStatus,");
            strSql.Append("SocreThree=@SocreThree,");
            strSql.Append("ThreeStatus=@ThreeStatus,");
            strSql.Append("ScoreFour=@ScoreFour,");
            strSql.Append("FourStatus=@FourStatus,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ScoreOne", SqlDbType.Float,8),
					new SqlParameter("@OneStatus", SqlDbType.Int,4),
					new SqlParameter("@ScoreTwo", SqlDbType.Float,8),
					new SqlParameter("@TwoStatus", SqlDbType.Int,4),
					new SqlParameter("@SocreThree", SqlDbType.Float,8),
					new SqlParameter("@ThreeStatus", SqlDbType.Int,4),
					new SqlParameter("@ScoreFour", SqlDbType.Float,8),
					new SqlParameter("@FourStatus", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@ID", SqlDbType.BigInt,8)};

            parameters[0].Value = model.ScoreOne;
            parameters[1].Value = model.OneStatus;
            parameters[2].Value = model.ScoreTwo;
            parameters[3].Value = model.TwoStatus;
            parameters[4].Value = model.SocreThree;
            parameters[5].Value = model.ThreeStatus;
            parameters[6].Value = model.ScoreFour;
            parameters[7].Value = model.FourStatus;
            parameters[8].Value = model.Remark;
            parameters[9].Value = model.ID;

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
