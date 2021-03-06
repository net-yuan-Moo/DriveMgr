﻿/***********************
 @ author:zlong
 @ Date:2015-01-08
 @ Desc:贷款管理逻辑层
 * ********************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class LoanBLL
    {
        private static readonly DriveMgr.IDAL.ILoanDAL loanDal = DriveMgr.DALFactory.Factory.GetLoanDAL();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddLoan(Model.LoanModel model)
        {
            return loanDal.AddLoan(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateLoan(Model.LoanModel model)
        {
            return loanDal.UpdateLoan(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteLoan(int id)
        {
            return loanDal.DeleteLoan(id);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteLoanList(string idlist)
        {
            return loanDal.DeleteLoanList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.LoanModel GetLoanModel(int id)
        {
            return loanDal.GetLoanModel(id);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="bank">银行</param>
        /// <param name="lenders">贷款人</param>
        /// <param name="createStartDate">开始时间</param>
        /// <param name="createEndDate">结束时间</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public string GetPagerData(string bank, string lenders, string createStartDate, string createEndDate, string order, int pageSize, int pageIndex, out int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DeleteMark = 0 ");
            if (bank.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(bank))
            {
                strSql.Append(" and Bank like '%" + bank + "%'");
            }
            if (lenders.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(lenders))
            {
                strSql.Append(" and Lenders like '%" + lenders + "%'");
            }
            if (createStartDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate > '" + createStartDate + "'");
            }
            if (createEndDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate < '" + createEndDate + "'");
            }
            return loanDal.GetPagerData("tb_Loan", "Id,Bank,Lenders,LoanDate,LoanAmount,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate", order, pageSize, pageIndex, strSql.ToString(), out totalCount);
        }

    }
}
