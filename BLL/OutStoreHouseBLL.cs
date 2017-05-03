using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class OutStoreHouseBLL
    {
        private static readonly DriveMgr.IDAL.IOutStoreHouseDAL outStoreHouseDal = DriveMgr.DALFactory.Factory.GetOutStoreHouseDAL();
               
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddOutStoreHouse(Model.OutStoreHouseModel model)
        {
            return outStoreHouseDal.AddOutStoreHouse(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateOutStoreHouse(Model.OutStoreHouseModel model)
        {
            return outStoreHouseDal.UpdateOutStoreHouse(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteOutStoreHouse(int id)
        {
            return outStoreHouseDal.DeleteOutStoreHouse(id);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteOutStoreHouseList(string idlist)
        {
            return outStoreHouseDal.DeleteOutStoreHouseList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.OutStoreHouseModel GetOutStoreHouseModel(int id)
        {
            return outStoreHouseDal.GetOutStoreHouseModel(id);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="outSN">出库单</param>
        /// <param name="createStartDate">开始时间</param>
        /// <param name="createEndDate">结束时间</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public string GetPagerData(string outSN, string createStartDate, string createEndDate, string order, int pageSize, int pageIndex, out int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DeleteMark = 0 ");
            if (outSN.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(outSN))
            {
                strSql.Append(" and OutSN like'%" + outSN + "%' ");
            }
            if (createStartDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate > '" + createStartDate + "'");
            }
            if (createEndDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate < '" + createEndDate + "'");
            }
            return outStoreHouseDal.GetPagerData("tb_OutStoreHouse", "Id,OutSN,OutDate,HandlePerson,Remark,CreateDate,CreatePerson,UpdateDate,UpdatePerson,DeleteMark", order, pageSize, pageIndex, strSql.ToString(), out totalCount);
        }

        /// <summary>
        /// 获取出库单集合
        /// </summary>
        /// <returns></returns>
        public string GetOutStoreHouseDT()
        {
            return outStoreHouseDal.GetOutStoreHouseDT();
        }

    }
}
