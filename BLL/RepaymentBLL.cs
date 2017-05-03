using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class RepaymentBLL
    {
        private static readonly DriveMgr.IDAL.IRepaymentDAL repaymentDal = DriveMgr.DALFactory.Factory.GetRepaymentDAL();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddRepayment(Model.RepaymentModel model)
        {
            return repaymentDal.AddRepayment(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateRepayment(Model.RepaymentModel model)
        {
            return repaymentDal.UpdateRepayment(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteRepayment(int id)
        {
            return repaymentDal.DeleteRepayment(id);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteRepaymentList(string idlist)
        {
            return repaymentDal.DeleteRepaymentList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.RepaymentModel GetRepaymentModel(int id)
        {
            return repaymentDal.GetRepaymentModel(id);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="bank">银行</param>
        /// <param name="repaymentPerson">贷款人</param>
        /// <param name="createStartDate">开始时间</param>
        /// <param name="createEndDate">结束时间</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public string GetPagerData(string bank, string repaymentPerson, string createStartDate, string createEndDate, string order, int pageSize, int pageIndex, out int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DeleteMark = 0 ");
            if (bank.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(bank))
            {
                strSql.Append(" and Bank like '%" + bank + "%'");
            }
            if (repaymentPerson.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(repaymentPerson))
            {
                strSql.Append(" and RepaymentPerson like '%" + repaymentPerson + "%'");
            }
            if (createStartDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate > '" + createStartDate + "'");
            }
            if (createEndDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate < '" + createEndDate + "'");
            }
            return repaymentDal.GetPagerData("tb_Repayment", "Id,Bank,RepaymentPerson,RepaymentDate,RepaymentAmount,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate", order, pageSize, pageIndex, strSql.ToString(), out totalCount);
        }


    }
}
