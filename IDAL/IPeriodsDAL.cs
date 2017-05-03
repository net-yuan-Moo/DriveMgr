using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DriveMgr.IDAL
{
    public interface IPeriodsDAL
    {
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        DriveMgr.Model.PeriodsModel GetModel(int ID);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(DriveMgr.Model.PeriodsModel model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(DriveMgr.Model.PeriodsModel model);

        /// <summary>
        /// 删除一条数据(伪删除)
        /// </summary>
        bool Delete(long ID);

        /// <summary>
        /// 批量删除数据(伪删除)
        /// </summary>
        bool DeleteList(string IDlist);


        /// <summary>
        /// 获取记录总数
        /// </summary>
        int GetRecordCount(string strWhere);

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
        string GetPager(string tableName, string columns, string order, int pageSize, int pageIndex, string where, out int totalCount);

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        DataTable GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        /// <summary>
        /// 设置为最新期数
        /// </summary>
        bool SetupToCurrent(int currentID);
    }
}
