using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.IDAL
{
    public interface IVehicleRentalLocalDAL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool AddVehicleRental(Model.VehicleRentalLocalModel model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool UpdateVehicleRental(Model.VehicleRentalLocalModel model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool DeleteVehicleRental(int id);

        /// <summary>
        /// 批量删除数据
        /// </summary>
        bool DeleteVehicleRentalList(string idlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.VehicleRentalLocalModel GetVehicleRentalModel(int id);

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
        string GetPagerData(string tableName, string columns, string order, int pageSize, int pageIndex, string where, out int totalCount);

        /// <summary>
        /// 获取学生集合
        /// </summary>
        /// <returns></returns>
        string GetStudentDT();

        /// <summary>
        /// 获取教练集合
        /// </summary>
        /// <returns></returns>
        string GetCoachDT();
    }
}
