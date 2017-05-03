using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using DriveMgr.IDAL;

namespace DriveMgr.SQLServerDAL
{
    /// <summary>
    /// 菜单按钮（SQL Server数据库实现）
    /// </summary>
    public class MenuButton : IMenuButton
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(int menuId, string[] buttonlist)
        {
            using (SqlConnection conn = new SqlConnection(DriveMgr.Common.SqlHelper.connStr))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        StringBuilder strSqlDel = new StringBuilder();
                        strSqlDel.Append("delete from tbMenuButton where MenuId=@MenuId ");
                        SqlParameter[] parametersDel = {
					new SqlParameter("@MenuId", SqlDbType.Int,4)};
                        parametersDel[0].Value = menuId;
                        DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.connStr, CommandType.Text, strSqlDel.ToString(), parametersDel);
                        cmd.Parameters.Clear();
                        for (int i = 0; i < buttonlist.Length; i++)
                        {
                            StringBuilder strSql = new StringBuilder();
                            strSql.Append("if not exists(select 1 from tbMenuButton where MenuId=@MenuId and ButtonId=@ButtonId) ");
                            strSql.Append("insert into tbMenuButton(");
                            strSql.Append("MenuId,ButtonId)");
                            strSql.Append(" values (");
                            strSql.Append("@MenuId,@ButtonId)");
                            SqlParameter[] parameters = {
					new SqlParameter("@MenuId", SqlDbType.Int,4),
					new SqlParameter("@ButtonId", SqlDbType.Int,4)};
                            parameters[0].Value = menuId;
                            parameters[1].Value = Int32.Parse(buttonlist[i]);

                            DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.connStr, CommandType.Text, strSql.ToString(), parameters);
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                        return true;
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                        throw;
                    }
                }
            }
        }

    }
}
