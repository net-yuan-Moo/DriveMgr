using DriveMgr.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DriveMgr.SQLServerDAL
{
    public class PeriodsDAL : IPeriodsDAL
    {
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DriveMgr.Model.PeriodsModel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,PeriodCode,Remark from tb_Periods ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            DriveMgr.Model.PeriodsModel model = new DriveMgr.Model.PeriodsModel();
            DataTable dt = DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), parameters);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["ID"] != null && dt.Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(dt.Rows[0]["ID"].ToString());
                }
                if (dt.Rows[0]["PeriodCode"] != null && dt.Rows[0]["PeriodCode"].ToString() != "")
                {
                    model.PeriodCode = dt.Rows[0]["PeriodCode"].ToString();
                }
                if (dt.Rows[0]["Remark"] != null && dt.Rows[0]["Remark"].ToString() != "")
                {
                    model.Remark = dt.Rows[0]["Remark"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(DriveMgr.Model.PeriodsModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_Periods(");
			strSql.Append("PeriodCode,Remark)");
			strSql.Append(" values (");
			strSql.Append("@PeriodCode,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@PeriodCode", SqlDbType.VarChar,20),
					new SqlParameter("@Remark", SqlDbType.VarChar,500)};
			parameters[0].Value = model.PeriodCode;
			parameters[1].Value = model.Remark;

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

        /// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DriveMgr.Model.PeriodsModel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tb_Periods set ");
			strSql.Append("PeriodCode=@PeriodCode,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@PeriodCode", SqlDbType.VarChar,20),
					new SqlParameter("@Remark", SqlDbType.VarChar,500),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.PeriodCode;
			parameters[1].Value = model.Remark;
			parameters[2].Value = model.ID;

            int rows = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), parameters);
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
        /// 删除一条数据(伪删除)
        /// </summary>
        public bool Delete(long ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_Periods ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            int rows = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), parameters);
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
        /// 批量删除数据(伪删除)
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Periods set flag=0 ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), null);
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
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM tb_Periods ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = Convert.ToInt32(DriveMgr.Common.SqlHelper.ExecuteScalar(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), null));
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
        /// 获取分页数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">要取的列名（逗号分开）</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="where">查询条件</param>
        /// <param name="totalCount">总记录数</param>
        public string GetPager(string tableName, string columns, string order, int pageSize, int pageIndex, string where, out int totalCount)
        {
            DataTable dt = DriveMgr.Common.SqlPagerHelper.GetPagerOfDrive(tableName, columns, order, pageSize, pageIndex, where, out totalCount);
            return DriveMgr.Common.JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataTable GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from tb_Periods T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), null);
        }

        /// <summary>
        /// 设置为最新期数
        /// </summary>
        public bool SetupToCurrent(int currentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"if exists(select 1 from tb_CurrentPeroid)
            update tb_CurrentPeroid set CurrentPeroidID=@CurrentPeroidID,flag=1
             else
             insert into tb_CurrentPeroid values(@CurrentPeroidID,1)
            ");
            SqlParameter[] parameters = {
					new SqlParameter("@CurrentPeroidID", SqlDbType.Int,4)
			};
            parameters[0].Value = currentID;

            int rows = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), parameters);
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
