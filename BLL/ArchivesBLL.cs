﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class ArchivesBLL
    {
        private static readonly DriveMgr.IDAL.IArchivesDAL dal = DriveMgr.DALFactory.Factory.GetArchivesDAL();

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
        /// 更新一条数据
        /// </summary>
        public bool Update(DriveMgr.Model.ArchivesModel model)
        {
            return dal.Update(model);
        }
    }
}
