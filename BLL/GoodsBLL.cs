using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class GoodsBLL
    {
        private static readonly DriveMgr.IDAL.IGoodsDAL goodsDal = DriveMgr.DALFactory.Factory.GetGoodsDAL();

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool IsExistGoods(string categoryName)
        {
            return goodsDal.IsExistGoods(categoryName);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddGoods(Model.GoodsModel model)
        {
            return goodsDal.AddGoods(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateGoods(Model.GoodsModel model)
        {
            return goodsDal.UpdateGoods(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteGoods(int id)
        {
            return goodsDal.DeleteGoods(id);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteGoodsList(string idlist)
        {
            return goodsDal.DeleteGoodsList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.GoodsModel GetGoodsModel(int id)
        {
            return goodsDal.GetGoodsModel(id);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public string GetPagerData(string order, int pageSize, int pageIndex, out int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DeleteMark = 0 ");
            return goodsDal.GetPagerData("V_Goods", "Id,GoodsName,MinQuantity,MaxQuantity,RealQuantity,Specification,HandlePerson,GoodsCategoryID,CategoryName,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate", order, pageSize, pageIndex, strSql.ToString(), out totalCount);
        }

        /// <summary>
        /// 获取物资类别集合
        /// </summary>
        /// <returns></returns>
        public string GetGoodsDT()
        {
            return goodsDal.GetGoodsDT();
        }
    }
}
