/***********************
 @ author:zlong
 @ Date:2015-01-08
 @ Desc:业务招待管理逻辑层
 * ********************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class BusinessEntertainBLL
    {
        private static readonly DriveMgr.IDAL.IBusinessEntertainDAL businessEntertainDal = DriveMgr.DALFactory.Factory.GetBusinessEntertainDAL();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddBusinessEntertain(Model.BusinessEntertainModel model)
        {
            return businessEntertainDal.AddBusinessEntertain(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateBusinessEntertain(Model.BusinessEntertainModel model)
        {
            return businessEntertainDal.UpdateBusinessEntertain(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteBusinessEntertain(int id)
        {
            return businessEntertainDal.DeleteBusinessEntertain(id);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteBusinessEntertainList(string idlist)
        {
            return businessEntertainDal.DeleteBusinessEntertainList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.BusinessEntertainModel GetBusinessEntertainModel(int id)
        {
            return businessEntertainDal.GetBusinessEntertainModel(id);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="entertainObject">招待对象</param>
        /// <param name="entertainUse">用途</param>
        /// <param name="transactor">经办人</param>
        /// <param name="createStartDate">开始时间</param>
        /// <param name="createEndDate">结束时间</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public string GetPagerData(string entertainObject, string entertainUse, string transactor, string createStartDate, string createEndDate, string order, int pageSize, int pageIndex, out int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DeleteMark = 0 ");
            if (entertainObject.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(entertainObject))
            {
                strSql.Append(" and EntertainObject like '%" + entertainObject + "%'");
            }
            if (entertainUse.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(entertainUse))
            {
                strSql.Append(" and EntertainUse like '%" + entertainUse + "%'");
            }
            if (transactor.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(transactor))
            {
                strSql.Append(" and Transactor like '%" + transactor + "%'");
            }
            if (createStartDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate > '" + createStartDate + "'");
            }
            if (createEndDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate < '" + createEndDate + "'");
            }
            return businessEntertainDal.GetPagerData("tb_BusinessEntertain", "Id,EntertainObject,EntertainUse,Transactor,EntertainAmount,TransactDate,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate", order, pageSize, pageIndex, strSql.ToString(), out totalCount);
        }
    }
}
