using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class EnterStoreHouseBLL
    {
        private static readonly DriveMgr.IDAL.IEnterStoreHouseDAL enterStoreHouseDal = DriveMgr.DALFactory.Factory.GetEnterStoreHouseDAL();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddEnterStoreHouse(Model.EnterStoreHouseModel model)
        {
            return enterStoreHouseDal.AddEnterStoreHouse(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateEnterStoreHouse(Model.EnterStoreHouseModel model)
        {
            return enterStoreHouseDal.UpdateEnterStoreHouse(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteEnterStoreHouse(int id)
        {
            return enterStoreHouseDal.DeleteEnterStoreHouse(id);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteEnterStoreHouseList(string idlist)
        {
            return enterStoreHouseDal.DeleteEnterStoreHouseList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.EnterStoreHouseModel GetEnterStoreHouseModel(int id)
        {
            return enterStoreHouseDal.GetEnterStoreHouseModel(id);
        }
       

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="enterSN">入库单</param>
        /// <param name="createStartDate">开始时间</param>
        /// <param name="createEndDate">结束时间</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public string GetPagerData(string enterSN, string createStartDate, string createEndDate, string order, int pageSize, int pageIndex, out int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DeleteMark = 0 ");
            if (enterSN.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(enterSN))
            {
                strSql.Append(" and EnterSN like'%" + enterSN + "%' ");
            }            
            if (createStartDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate > '" + createStartDate + "'");
            }
            if (createEndDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate < '" + createEndDate + "'");
            }
            return enterStoreHouseDal.GetPagerData("tb_EnterStoreHouse", "Id,EnterSN,EnterDate,HandlePerson,Remark,CreateDate,CreatePerson,UpdateDate,UpdatePerson,DeleteMark", order, pageSize, pageIndex, strSql.ToString(), out totalCount);
        }


        /// <summary>
        /// 获取入库单集合
        /// </summary>
        /// <returns></returns>
        public string GetEnterStoreHouseDT()
        {
            return enterStoreHouseDal.GetEnterStoreHouseDT();
        }

    }
}
