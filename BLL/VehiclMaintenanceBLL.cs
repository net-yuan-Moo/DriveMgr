using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class VehiclMaintenanceBLL
    {
        private static readonly DriveMgr.IDAL.IVehiclMaintenanceDAL vehiclMaintenanceDal = DriveMgr.DALFactory.Factory.GetVehiclMaintenanceDAL();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddVehiclMaintenance(Model.VehiclMaintenanceModel model)
        {
            return vehiclMaintenanceDal.AddVehiclMaintenance(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateVehiclMaintenance(Model.VehiclMaintenanceModel model)
        {
            return vehiclMaintenanceDal.UpdateVehiclMaintenance(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteVehiclMaintenance(int id)
        {
            return vehiclMaintenanceDal.DeleteVehiclMaintenance(id);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteVehiclMaintenanceList(string idlist)
        {
            return vehiclMaintenanceDal.DeleteVehiclMaintenanceList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.VehiclMaintenanceModel GetVehiclMaintenanceModel(int id)
        {
            return vehiclMaintenanceDal.GetVehiclMaintenanceModel(id);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="vehicleId">车辆编号</param>
        /// <param name="maintenanceType">费用类别</param>
        /// <param name="createStartDate">开始时间</param>
        /// <param name="createEndDate">结束时间</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public string GetPagerData(string vehicleId, string maintenanceType, string createStartDate, string createEndDate, string order, int pageSize, int pageIndex, out int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DeleteMark = 0 ");
            if (vehicleId.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(vehicleId))
            {
                strSql.Append(" and VehicleId = " + vehicleId);
            }
            if (maintenanceType.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(maintenanceType))
            {
                strSql.Append(" and MaintenanceType = " + maintenanceType);
            }
            if (createStartDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate > '" + createStartDate + "'");
            }
            if (createEndDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate < '" + createEndDate + "'");
            }
            return vehiclMaintenanceDal.GetPagerData("V_VehiclMaintenance", "Id,VehicleId,MaintenCosts,MaintenanceType,MaintenanceTypeName,LicencePlateNum,MaintenDate,MaintenPerson,Remark,CreateDate,CreatePerson,UpdateDate,UpdatePerson,DeleteMark", order, pageSize, pageIndex, strSql.ToString(), out totalCount);
        }

    }
}
