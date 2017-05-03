using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class EnterStoreHouseDetailsBLL
    {
        private static readonly DriveMgr.IDAL.IEnterStoreHouseDetailDAL enterStoreHouseDetailsDal = DriveMgr.DALFactory.Factory.GetEnterStoreHouseDetailDAL();
                

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddEnterStoreHouseDetails(Model.EnterStoreHouseDetailsModel model)
        {
            return enterStoreHouseDetailsDal.AddEnterStoreHouseDetails(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateEnterStoreHouseDetails(Model.EnterStoreHouseDetailsModel model)
        {
            return enterStoreHouseDetailsDal.UpdateEnterStoreHouseDetails(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteEnterStoreHouseDetails(int id)
        {
            return enterStoreHouseDetailsDal.DeleteEnterStoreHouseDetails(id);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteEnterStoreHouseDetailsList(string idlist)
        {
            return enterStoreHouseDetailsDal.DeleteEnterStoreHouseDetailsList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.EnterStoreHouseDetailsModel GetEnterStoreHouseDetailsModel(int id)
        {
            return enterStoreHouseDetailsDal.GetEnterStoreHouseDetailsModel(id);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="enterDetailsSN">入库明细编码</param>
        /// <param name="enterStoreHouseID">入库单编号</param>
        /// <param name="goodsID">物品编号</param>
        /// <param name="goodsCategoryID">物资类别编号</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public string GetPagerData(string enterDetailsSN, string enterStoreHouseID,string goodsID,string goodsCategoryID, string order, int pageSize, int pageIndex, out int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DeleteMark = 0 ");
            if (enterDetailsSN.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(enterDetailsSN))
            {
                strSql.Append(" and EnterDetailsSN like'%" + enterDetailsSN + "%' ");
            }
            if (enterStoreHouseID.Trim() != string.Empty)
            {
                strSql.Append(" and EnterStoreHouseID = " + enterStoreHouseID);
            }
            if (goodsID.Trim() != string.Empty)
            {
                strSql.Append(" and GoodsID = " + goodsID);
            }
            if (goodsCategoryID.Trim() != string.Empty)
            {
                strSql.Append(" and GoodsCategoryID = " + goodsCategoryID);
            }
            return enterStoreHouseDetailsDal.GetPagerData("V_EnterStoreHouseDetails", "Id,EnterDetailsSN,GoodsID,EnterQuantity,EnterStoreHouseID,EnterSN,GoodsName,CategoryName,GoodsCategoryID,DeleteMark", order, pageSize, pageIndex, strSql.ToString(), out totalCount);
        }

    }
}
