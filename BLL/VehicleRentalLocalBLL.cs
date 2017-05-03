using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class VehicleRentalLocalBLL
    {
        private static readonly DriveMgr.IDAL.IVehicleRentalLocalDAL vehicleRentalDal = DriveMgr.DALFactory.Factory.GetVehicleRentalLocalDAL();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddVehicleRental(Model.VehicleRentalLocalModel model)
        {
            return vehicleRentalDal.AddVehicleRental(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateVehicleRental(Model.VehicleRentalLocalModel model)
        {
            return vehicleRentalDal.UpdateVehicleRental(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteVehicleRental(int id)
        {
            return vehicleRentalDal.DeleteVehicleRental(id);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteVehicleRentalList(string idlist)
        {
            return vehicleRentalDal.DeleteVehicleRentalList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.VehicleRentalLocalModel GetVehicleRentalModel(int id)
        {
            return vehicleRentalDal.GetVehicleRentalModel(id);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="studentName">学员</param>
        /// <param name="coachName">教练</param>
        /// <param name="vehicleId">车辆编号</param>
        /// <param name="priceConfigID">计价类别</param>
        /// <param name="createStartDate">开始时间</param>
        /// <param name="createEndDate">结束时间</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public string GetPagerData(string studentName, string vehicleId, string priceConfigID, string createStartDate, string createEndDate, string order, int pageSize, int pageIndex, out int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DeleteMark = 0 ");
            if (studentName.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(studentName))
            {
                strSql.Append(" and StudentName like'%" + studentName + "%' ");
            }

            if (vehicleId.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(vehicleId))
            {
                strSql.Append(" and VehicleId = " + vehicleId);
            }
            if (priceConfigID.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(priceConfigID))
            {
                strSql.Append(" and PriceConfigID = " + priceConfigID);
            }
            if (createStartDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate > '" + createStartDate + "'");
            }
            if (createEndDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate < '" + createEndDate + "'");
            }
            return vehicleRentalDal.GetPagerData("V_VehicleRental", "Id,VehicleId,PriceConfigID,RentDate,StudentName,StudentCode,Longer,TotalPrice,PriceTypeName,Price,LicencePlateNum,Remark,CreateDate,CreatePerson,UpdateDate,UpdatePerson,DeleteMark", order, pageSize, pageIndex, strSql.ToString(), out totalCount);
        }

        /// <summary>
        /// 获取学生集合
        /// </summary>
        /// <returns></returns>
        public string GetStudentDT()
        {
            return vehicleRentalDal.GetStudentDT();
        }

        /// <summary>
        /// 获取教练集合
        /// </summary>
        /// <returns></returns>
        public string GetCoachDT()
        {
            return vehicleRentalDal.GetCoachDT();
        }
    }
}
