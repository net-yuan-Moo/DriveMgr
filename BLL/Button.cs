using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DriveMgr.BLL
{
    /// <summary>
    /// 按钮（BLL）
    /// </summary>
    public class Button
    {
        private static readonly DriveMgr.IDAL.IButton dal = DriveMgr.DALFactory.Factory.GetButtonDAL();

        /// <summary>
        /// 根据菜单标识码和用户id获取此用户拥有该菜单下的哪些按钮权限
        /// </summary>
        public DataTable GetButtonByMenuCodeAndUserId(string menuCode, int userId)
        {
            return dal.GetButtonByMenuCodeAndUserId(menuCode, userId);
        }
        /// <summary>
        /// 根据菜单标识码和用户id获取按钮
        /// </summary>
        public string GetAllButton()
        {
            DataTable dt = dal.GetAllButton();
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            sb.Append("{\"id\":\"" + "-1" + "\",\"text\":\"" + "所有按钮" + "\",\"children\":[");
            for (int j = 0; j < dt.Rows.Count; j++)
            {

                //这里不能取表里的roleid，表里的roleid有些是null，后面的赋权限无法进行
                //sb.Append("{\"id\":\"" + r_listButton[k]["roleid"].ToString() + "\",\"text\":\"" + r_listButton[k]["buttonname"].ToString() + "\",\"checked\":" + r_listButton[k]["checked"].ToString() + ",\"attributes\":{\"rolemenubuttonid\":\"" + r_listButton[k]["rolemenubuttonid"].ToString() + "\",\"menuid\":\"" + r_listButton[k]["menuid"].ToString() + "\",\"buttonid\":\"" + r_listButton[k]["buttonid"].ToString() + "\"}},");
                sb.Append("{\"id\":\"" + dt.Rows[j]["id"].ToString() + "\",\"text\":\"" + dt.Rows[j]["name"].ToString() + "\",\"checked\":" + "false" + ",\"attributes\":{\"menuid\":\"" + dt.Rows[j]["id"].ToString() + "\",\"buttonid\":\"" + dt.Rows[j]["id"].ToString() + "\"}},");

                sb.Remove(sb.Length - 1, 1);
                sb.Append(",");

            }
            sb.Remove(sb.Length - 1, 1);
            //sb.Append("]},");


           // sb.Remove(sb.Length - 1, 1);
            sb.Append("]}]");


            return sb.ToString();
        }

        /// <summary>
        /// 根据菜单标识码和用户id获取按钮
        /// </summary>
        public DataTable GetButtonByMenu(int menuId)
        {
            return dal.GetButtonByMenu(menuId);
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
            DataTable dt = DriveMgr.Common.SqlPagerHelper.GetPager(tableName, columns, order, pageSize, pageIndex, where, out totalCount);
            return DriveMgr.Common.JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(DriveMgr.Model.Button model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DriveMgr.Model.Button model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            return dal.Delete(Id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            return dal.DeleteList(Idlist);
        }
    }
}
