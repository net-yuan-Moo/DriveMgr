using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class GoodsCategoryBLL
    {
        private static readonly DriveMgr.IDAL.IGoodsCategoryDAL goodsCategoryDal = DriveMgr.DALFactory.Factory.GetGoodsCategoryDAL();

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool IsExistGoodsCategory(string categoryName)
        {
            return goodsCategoryDal.IsExistGoodsCategory(categoryName);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddGoodsCategory(Model.GoodsCategoryModel model)
        {
            return goodsCategoryDal.AddGoodsCategory(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateGoodsCategory(Model.GoodsCategoryModel model)
        {
            return goodsCategoryDal.UpdateGoodsCategory(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteGoodsCategory(int id)
        {
            return goodsCategoryDal.DeleteGoodsCategory(id);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteGoodsCategoryList(string idlist)
        {
            return goodsCategoryDal.DeleteGoodsCategoryList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.GoodsCategoryModel GetGoodsCategoryModel(int id)
        {
            return goodsCategoryDal.GetGoodsCategoryModel(id);
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
            return goodsCategoryDal.GetPagerData("tb_GoodsCategory", "Id,CategoryName,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate", order, pageSize, pageIndex, strSql.ToString(), out totalCount);
        }

        /// <summary>
        /// 获取物资类别集合
        /// </summary>
        /// <returns></returns>
        public string GetGoodsCategoryDT()
        {
            return goodsCategoryDal.GetGoodsCategoryDT();
        }
    }
}
