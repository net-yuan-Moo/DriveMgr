using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class ScoresBLL
    {
        private static readonly DriveMgr.IDAL.IScoresDAL dal = DriveMgr.DALFactory.Factory.GetScoresDAL();

        public string GetPager(string tableName, string columns, string order, int pageSize, int pageIndex, string where, out int totalCount)
        {
            return dal.GetPager(tableName, columns, order, pageSize, pageIndex, where, out totalCount);
        }

         /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DriveMgr.Model.ScoresModel model)
        {
            return dal.Update(model);
        }
    }
}
