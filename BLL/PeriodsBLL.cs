using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class PeriodsBLL
    {
        private static readonly DriveMgr.IDAL.IPeriodsDAL dal = DriveMgr.DALFactory.Factory.GetPeriodsDAL();

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DriveMgr.Model.PeriodsModel GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(DriveMgr.Model.PeriodsModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DriveMgr.Model.PeriodsModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据(伪删除)
        /// </summary>
        public bool Delete(long ID)
        {
            return dal.Delete(ID);
        }

        /// <summary>
        /// 批量删除数据(伪删除)
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }


        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
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
            return dal.GetPager(tableName, columns, order, pageSize, pageIndex, where, out totalCount);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataTable GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        /// <summary>
        /// 设置为最新期数
        /// </summary>
        public bool SetupToCurrent(int currentID)
        {
            return dal.SetupToCurrent(currentID);
        }
    }
}
