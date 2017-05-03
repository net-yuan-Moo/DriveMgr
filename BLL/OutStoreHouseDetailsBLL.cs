using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class OutStoreHouseDetailsBLL
    {
        private static readonly DriveMgr.IDAL.IOutStoreHouseDetailsDAL outStoreHouseDetailsDal = DriveMgr.DALFactory.Factory.GetOutStoreHouseDetailsDAL();
                

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddOutStoreHouseDetails(Model.OutStoreHouseDetailsModel model)
        {
            return outStoreHouseDetailsDal.AddOutStoreHouseDetails(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateOutStoreHouseDetails(Model.OutStoreHouseDetailsModel model)
        {
            return outStoreHouseDetailsDal.UpdateOutStoreHouseDetails(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteOutStoreHouseDetails(int id)
        {
            return outStoreHouseDetailsDal.DeleteOutStoreHouseDetails(id);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteOutStoreHouseDetailsList(string idlist)
        {
            return outStoreHouseDetailsDal.DeleteOutStoreHouseDetailsList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.OutStoreHouseDetailsModel GetOutStoreHouseDetailsModel(int id)
        {
            return outStoreHouseDetailsDal.GetOutStoreHouseDetailsModel(id);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="outDetailsSN">出库明细编码</param>
        /// <param name="outStoreHouseID">出库单编号</param>
        /// <param name="goodsID">物品编号</param>
        /// <param name="goodsCategoryID">物资类别编号</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public string GetPagerData(string outDetailsSN, string outStoreHouseID, string goodsID, string goodsCategoryID, string order, int pageSize, int pageIndex, out int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DeleteMark = 0 ");
            if (outDetailsSN.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(outDetailsSN))
            {
                strSql.Append(" and OutDetailsSN like'%" + outDetailsSN + "%' ");
            }
            if (outStoreHouseID.Trim() != string.Empty)
            {
                strSql.Append(" and OutStoreHouseID = " + outStoreHouseID);
            }
            if (goodsID.Trim() != string.Empty)
            {
                strSql.Append(" and GoodsID = " + goodsID);
            }
            if (goodsCategoryID.Trim() != string.Empty)
            {
                strSql.Append(" and GoodsCategoryID = " + goodsCategoryID);
            }
            return outStoreHouseDetailsDal.GetPagerData("V_OutStoreHouseDetails", "Id,OutDetailsSN,GoodsID,OutQuantity,OutStoreHouseID,OutSN,GoodsName,CategoryName,GoodsCategoryID,DeleteMark", order, pageSize, pageIndex, strSql.ToString(), out totalCount);
        }

    }
}
