using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DriveMgr.SQLServerDAL
{
    /// <summary>
    /// 按钮（SQL Server数据库实现）
    /// </summary>
    public class Button : DriveMgr.IDAL.IButton
    {
        /// <summary>
        /// 根据菜单标识码和用户id获取此用户拥有该菜单下的哪些按钮权限
        /// </summary>
        public DataTable GetButtonByMenuCodeAndUserId(string menuCode, int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct(b.Id) id,b.Code code,b.Name name,b.Icon icon,b.Sort sort from tbUser u");
            strSql.Append(" join tbUserRole ur on u.Id=ur.UserId");
            strSql.Append(" join tbRoleMenuButton rmb on ur.RoleId=rmb.RoleId");
            strSql.Append(" join tbMenu m on rmb.MenuId=m.Id");
            strSql.Append(" join tbButton b on rmb.ButtonId=b.Id");
            strSql.Append(" where u.Id=@Id and m.Code=@MenuCode order by b.Sort");
            SqlParameter[] paras = { 
                                   new SqlParameter("@Id",userId),
                                   new SqlParameter("@MenuCode",menuCode)
                                   };

            return DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras);
        }

        /// <summary>
        /// 根据菜单标识码和用户id获取按钮
        /// </summary>
        public DataTable GetAllButton()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id id,Name name,Code code from tbButton");
           
            return DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.connStr, CommandType.Text, strSql.ToString());
        }

        /// <summary>
        /// 根据菜单标识码和用户id获取按钮
        /// </summary>
        public DataTable GetButtonByMenu(int menuId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MenuId,ButtonId from tbMenuButton");
            if (menuId != -1)
            {
                strSql.Append("  WHERE MenuId=@Id");
            }

            SqlParameter[] paras = { 
                                   new SqlParameter("@Id",menuId)
                                   };

            return DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.connStr, CommandType.Text, strSql.ToString(), paras);
        }
        

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(DriveMgr.Model.Button model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbButton(");
            strSql.Append("Name,Code,Icon,Sort,AddDate,Description)");
            strSql.Append(" values (");
            strSql.Append("@Name,@Code,@Icon,@Sort,@AddDate,@Description)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.VarChar,50),
					new SqlParameter("@Code", SqlDbType.VarChar,50),
					new SqlParameter("@Icon", SqlDbType.VarChar,50),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@AddDate", SqlDbType.DateTime),
					new SqlParameter("@Description", SqlDbType.VarChar,100)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Code;
            parameters[2].Value = model.Icon;
            parameters[3].Value = model.Sort;
            parameters[4].Value = model.AddDate;
            parameters[5].Value = model.Description;

            int rows = Convert.ToInt32(DriveMgr.Common.SqlHelper.ExecuteScalar(DriveMgr.Common.SqlHelper.connStr, CommandType.Text, strSql.ToString(), parameters));
            if (rows>0)
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
        public bool Update(DriveMgr.Model.Button model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbButton set ");
            strSql.Append("Name=@Name,");
            strSql.Append("Code=@Code,");
            strSql.Append("Icon=@Icon,");
            strSql.Append("Sort=@Sort,");
            strSql.Append("AddDate=@AddDate,");
            strSql.Append("Description=@Description");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.VarChar,50),
					new SqlParameter("@Code", SqlDbType.VarChar,50),
					new SqlParameter("@Icon", SqlDbType.VarChar,50),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@AddDate", SqlDbType.DateTime),
					new SqlParameter("@Description", SqlDbType.VarChar,100),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Code;
            parameters[2].Value = model.Icon;
            parameters[3].Value = model.Sort;
            parameters[4].Value = model.AddDate;
            parameters[5].Value = model.Description;
            parameters[6].Value = model.Id;

            int rows = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.connStr, CommandType.Text, strSql.ToString(), parameters);
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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tbButton ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            int rows = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.connStr, CommandType.Text, strSql.ToString(), parameters);
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tbButton ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.connStr, CommandType.Text, strSql.ToString(), null);
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
