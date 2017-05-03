using System.Data;

namespace DriveMgr.IDAL
{
    /// <summary>
    /// 按钮接口（不同的数据库访问类实现接口达到多数据库的支持）
    /// </summary>
    public interface IButton
    {
        /// <summary>
        /// 根据菜单标识码和用户id获取此用户拥有该菜单下的哪些按钮权限
        /// </summary>
        DataTable GetButtonByMenuCodeAndUserId(string menuCode, int userId);

        DataTable GetAllButton();

        /// <summary>
        /// 根据菜单标识码和用户id获取按钮
        /// </summary>
        DataTable GetButtonByMenu(int menuId);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(DriveMgr.Model.Button model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(DriveMgr.Model.Button model);

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
