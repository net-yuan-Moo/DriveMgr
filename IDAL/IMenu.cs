using System.Collections.Generic;
using System.Data;

namespace DriveMgr.IDAL
{
    /// <summary>
    /// 菜单接口（不同的数据库访问类实现接口达到多数据库的支持）
    /// </summary>
    public interface IMenu
    {
        /// <summary>
        /// 根据用户主键id查询用户可以访问的菜单
        /// </summary>
        DataTable GetUserMenu(int id);

        /// <summary>
        /// 根据角色id获取此角色可以访问的菜单和菜单下的按钮（编辑角色-菜单使用）
        /// </summary>
        DataTable GetAllMenu(int roleId);

        /// <summary>
        /// 根据用户主键id查询用户拥有的权限（后台首页“我的权限”）
        /// </summary>
        DataTable GetMyAuthority(int id);

        /// <summary>
        /// 根据条件获取菜单
        /// </summary>
        DataTable GetAllMenuByCondition(string where);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(DriveMgr.Model.Menu model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(DriveMgr.Model.Menu model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int Id);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool DeleteList(string Idlist);
    }
}
