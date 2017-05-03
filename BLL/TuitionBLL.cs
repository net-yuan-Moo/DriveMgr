using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class TuitionBLL
    {
        private static readonly DriveMgr.IDAL.ITuitionDAL tuitionDal = DriveMgr.DALFactory.Factory.GetTuitionDAL();

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool IsExistTuition(int localType)
        {
            return tuitionDal.IsExistTuition(localType);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddTuition(Model.TuitionModel model)
        {
            return tuitionDal.AddTuition(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateTuition(Model.TuitionModel model)
        {
            return tuitionDal.UpdateTuition(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteTuition(int id)
        {
            return tuitionDal.DeleteTuition(id);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteTuitionList(string idlist)
        {
            return tuitionDal.DeleteTuitionList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.TuitionModel GetTuitionModel(int id)
        {
            return tuitionDal.GetTuitionModel(id);
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
            return tuitionDal.GetPagerData("tb_Tuition", "Id,LocalType,Costs,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate", order, pageSize, pageIndex, strSql.ToString(), out totalCount);
        }

    }
}
