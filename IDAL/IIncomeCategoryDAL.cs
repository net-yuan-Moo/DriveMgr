/***********************
 @ author:zlong
 @ Date:2015-01-06
 @ Desc:收入分类接口
 * ********************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DriveMgr.IDAL
{
    public interface IIncomeCategoryDAL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool AddIncomeCategory(Model.IncomeCategoryModel model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool UpdateIncomeCategory(Model.IncomeCategoryModel model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool DeleteIncomeCategory(int id);

        /// <summary>
        /// 批量删除数据
        /// </summary>
        bool DeleteIncomeCategoryList(string idlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.IncomeCategoryModel GetIncomeCategoryModel(int Id);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">要取的列名（逗号分开）</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="where">查询条件</param>
        /// <param name="totalCount">总记录数</param>
        string GetPagerData(string tableName, string columns, string order, int pageSize, int pageIndex, string where, out int totalCount);


        /// <summary>
        /// 得到入账统计的表名和字段名
        /// </summary>
        /// <returns></returns>
        DataTable GetTableFromIncome();

        /// <summary>
        /// 获得一年的入账收入
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        DataSet GetIncomeOfOneYear(DataTable dt, string year);

        /// <summary>
        /// 获得一年的入账收入
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        DataSet GetIncomeOfOneYear(string year);
    }
}
