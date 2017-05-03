using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class AppointmentBLL
    {
        private static readonly DriveMgr.IDAL.IAppointmentDAL dal = DriveMgr.DALFactory.Factory.GetAppointmentDAL();

        public string GetPager(string tableName, string columns, string order, int pageSize, int pageIndex, string where, out int totalCount)
        {
            return dal.GetPager(tableName, columns, order, pageSize, pageIndex, where, out totalCount);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DriveMgr.Model.AppointmentModel model)
        {
            return dal.Update(model);
        }
    }
}
