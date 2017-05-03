using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class TravelBLL
    {
        private static readonly DriveMgr.IDAL.ITravelDAL travelDal = DriveMgr.DALFactory.Factory.GetTravelDAL();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddTravel(Model.TravelModel model)
        {
            return travelDal.AddTravel(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateTravel(Model.TravelModel model)
        {
            return travelDal.UpdateTravel(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteTravel(int id)
        {
            return travelDal.DeleteTravel(id);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteTravelList(string idlist)
        {
            return travelDal.DeleteTravelList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.TravelModel GetTravelModel(int id)
        {
            return travelDal.GetTravelModel(id);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="travelUse">用途</param>
        /// <param name="travelPerson">经办人</param>
        /// <param name="createStartDate">开始时间</param>
        /// <param name="createEndDate">结束时间</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public string GetPagerData(string travelUse, string travelPerson, string createStartDate, string createEndDate, string order, int pageSize, int pageIndex, out int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DeleteMark = 0 ");
            if (travelUse.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(travelUse))
            {
                strSql.Append(" and TravelUse like '%" + travelUse + "%'");
            }
            if (travelPerson.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(travelPerson))
            {
                strSql.Append(" and TravelPerson like '%" + travelPerson + "%'");
            }
            if (createStartDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate > '" + createStartDate + "'");
            }
            if (createEndDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate < '" + createEndDate + "'");
            }
            return travelDal.GetPagerData("tb_Travel", "Id,TravelUse,TravelPerson,TraveDate,TravelAmount,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate", order, pageSize, pageIndex, strSql.ToString(), out totalCount);
        }

    }
}
