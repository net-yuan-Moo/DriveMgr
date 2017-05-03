using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DriveMgr.SQLServerDAL
{
    /// <summary>
    /// 菜单（SQL Server数据库实现）
    /// </summary>
    public class Menu : DriveMgr.IDAL.IMenu
    {
        /// <summary>
        /// 根据用户主键id查询用户可以访问的菜单
        /// </summary>
        public DataTable GetUserMenu(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct(m.Name) menuname,m.Id menuid,m.Icon icon,u.Id userid,u.UserId username,m.ParentId menuparentid,m.Sort menusort,m.LinkAddress linkaddress from tbUser u");
            strSql.Append(" join tbUserRole ur on u.Id=ur.UserId");
            strSql.Append(" join tbRoleMenuButton rmb on ur.RoleId=rmb.RoleId");
            strSql.Append(" join tbMenu m on rmb.MenuId=m.Id");
            strSql.Append(" where u.Id=@Id and MenuStatus=1 order by m.ParentId,m.Sort");

            return DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.connStr, CommandType.Text, strSql.ToString(), new SqlParameter("@Id", id));
        }

        /// <summary>
        /// 根据角色id获取此角色可以访问的菜单和菜单下的按钮（编辑角色-菜单使用）
        /// </summary>
        public DataTable GetAllMenu(int roleId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select m.Id menuid,m.Name menuname,m.ParentId parentid,m.Icon menuicon,mb.ButtonId buttonid,b.Name buttonname,b.Icon buttonicon,rmb.RoleId roleid,case when isnull(rmb.ButtonId , 0) = 0 then 'false' else 'true' end checked");
            sqlStr.Append(" from tbMenu m");
            sqlStr.Append(" left join tbMenuButton mb on m.Id=mb.MenuId");
            sqlStr.Append(" left join tbButton b on mb.ButtonId=b.Id");
            sqlStr.Append(" left join tbRoleMenuButton rmb on(mb.MenuId=rmb.MenuId and mb.ButtonId=rmb.ButtonId and rmb.RoleId = @RoleId)");
            sqlStr.Append(" order by m.ParentId,m.Sort,b.Sort");
            return DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.connStr, CommandType.Text, sqlStr.ToString(), new SqlParameter("@RoleId", roleId));
        }

        /// <summary>
        /// 根据用户主键id查询用户拥有的权限（后台首页“我的权限”）
        /// </summary>
        public DataTable GetMyAuthority(int id)
        {
            return DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.connStr, CommandType.StoredProcedure, "sp_GetAuthorityByUserId", new SqlParameter("@userId", id));
        }

        /// <summary>
        /// 根据条件获取菜单
        /// </summary>
        public DataTable GetAllMenuByCondition(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from tbMenu");
            if (!string.IsNullOrEmpty(where))
            {
                strSql.Append(" where " + where);
            }
            strSql.Append(" order by Id");
            return DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.connStr, CommandType.Text, strSql.ToString(), null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(DriveMgr.Model.Menu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbMenu(");
            strSql.Append("Name,ParentId,Code,LinkAddress,Icon,Sort,AddDate)");
            strSql.Append(" values (");
            strSql.Append("@Name,@ParentId,@Code,@LinkAddress,@Icon,@Sort,@AddDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.VarChar,50),
					new SqlParameter("@ParentId", SqlDbType.Int,4),
					new SqlParameter("@Code", SqlDbType.VarChar,50),
					new SqlParameter("@LinkAddress", SqlDbType.VarChar,100),
					new SqlParameter("@Icon", SqlDbType.VarChar,50),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@AddDate", SqlDbType.DateTime)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.ParentId;
            parameters[2].Value = model.Code;
            parameters[3].Value = model.LinkAddress;
            parameters[4].Value = model.Icon;
            parameters[5].Value = model.Sort;
            parameters[6].Value = model.AddDate;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(DriveMgr.Model.Menu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbMenu set ");
            strSql.Append("[Name]=@Name,");
            //strSql.Append("ParentId=@ParentId,");
            strSql.Append("[Code]=@Code,");
            strSql.Append("LinkAddress=@LinkAddress,");
            strSql.Append("Icon=@Icon,");
            strSql.Append("[Sort]=@Sort,");
            strSql.Append("[AddDate]=@AddDate");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.VarChar,50),
					//new SqlParameter("@ParentId", SqlDbType.Int,4),
					new SqlParameter("@Code", SqlDbType.VarChar,50),
					new SqlParameter("@LinkAddress", SqlDbType.VarChar,100),
					new SqlParameter("@Icon", SqlDbType.VarChar,50),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@AddDate", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            //parameters[1].Value = model.ParentId;
            parameters[1].Value = model.Code;
            parameters[2].Value = model.LinkAddress;
            parameters[3].Value = model.Icon;
            parameters[4].Value = model.Sort;
            parameters[5].Value = model.AddDate;
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
            strSql.Append("delete from tbMenu ");
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
            strSql.Append("delete from tbMenu ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.connStr, CommandType.Text, strSql.ToString());
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
