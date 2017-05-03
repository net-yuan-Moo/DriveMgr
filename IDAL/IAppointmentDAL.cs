using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.IDAL
{
    public interface IAppointmentDAL
    {
        string GetPager(string tableName, string columns, string order, int pageSize, int pageIndex, string where, out int totalCount);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(DriveMgr.Model.AppointmentModel model);
    }
}
