using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class SiteRentalBLL
    {
        private static readonly DriveMgr.IDAL.ISiteRentalDAL siteRentalDal = DriveMgr.DALFactory.Factory.GetSiteRentalDAL();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddSiteRental(Model.SiteRentalModel model)
        {
            return siteRentalDal.AddSiteRental(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateSiteRental(Model.SiteRentalModel model)
        {
            return siteRentalDal.UpdateSiteRental(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteSiteRental(int id)
        {
            return siteRentalDal.DeleteSiteRental(id);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteSiteRentalList(string idlist)
        {
            return siteRentalDal.DeleteSiteRentalList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.SiteRentalModel GetSiteRentalModel(int id)
        {
            return siteRentalDal.GetSiteRentalModel(id);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="rentObject">出租对象</param>
        /// <param name="vehicleId">车辆编号</param>
        /// <param name="priceConfigID">计价类别</param>
        /// <param name="createStartDate">开始时间</param>
        /// <param name="createEndDate">结束时间</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public string GetPagerData(string rentObject, string vehicleId, string priceConfigID, string createStartDate, string createEndDate, string order, int pageSize, int pageIndex, out int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DeleteMark = 0 ");
            if (rentObject.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(rentObject))
            {
                strSql.Append(" and RentObject like'%" + rentObject + "%' ");
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
            return siteRentalDal.GetPagerData("V_SiteRental", "Id,VehicleId,PriceConfigID,RentDate,RentObject,Longer,TotalPrice,PriceTypeName,Price,LicencePlateNum,Remark,CreateDate,CreatePerson,UpdateDate,UpdatePerson,DeleteMark", order, pageSize, pageIndex, strSql.ToString(), out totalCount);
        }

    }
}
