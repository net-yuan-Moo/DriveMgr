using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class PriceConfigBLL
    {
        private static readonly DriveMgr.IDAL.IPriceConfigDAL priceConfigDal = DriveMgr.DALFactory.Factory.GetPriceConfigDAL();


        /// 是否存在该记录
        /// </summary>
        public bool IsExistPriceConfig(string priceTypeName)
        {
            return priceConfigDal.IsExistPriceConfig(priceTypeName);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddPriceConfig(Model.PriceConfigModel model)
        {
            return priceConfigDal.AddPriceConfig(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdatePriceConfig(Model.PriceConfigModel model)
        {
            return priceConfigDal.UpdatePriceConfig(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeletePriceConfig(int id)
        {
            return priceConfigDal.DeletePriceConfig(id);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeletePriceConfigList(string idlist)
        {
            return priceConfigDal.DeletePriceConfigList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.PriceConfigModel GetPriceConfigModel(int id)
        {
            return priceConfigDal.GetPriceConfigModel(id);
        }


        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="categoryname">类型</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public string GetPagerData(string order, int pageSize, int pageIndex, out int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DeleteMark = 0 ");
            return priceConfigDal.GetPagerData("V_PriceConfig", "Id,ConfigType,ConfigTypeName,PriceTypeName,Price,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate", order, pageSize, pageIndex, strSql.ToString(), out totalCount);
        }

        /// <summary>
        /// 获取车牌号集合
        /// </summary>
        /// <returns></returns>
        public string GetPriceConfigDT(int configType)
        {
            return priceConfigDal.GetPriceConfigDT(configType);
        }
    }
}
