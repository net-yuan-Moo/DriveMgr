using DriveMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DriveMgr.IDAL
{
    public interface IRegistrationDAL
    {
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        DriveMgr.Model.RegistrationModel GetModel(long stuId);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(DriveMgr.Model.RegistrationModel model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(DriveMgr.Model.RegistrationModel model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(long ID);
       
        /// <summary>
        /// 批量删除数据
        /// </summary>
        bool DeleteList(string IDlist);
        
        /// <summary>
        /// 获取记录总数
        /// </summary>
        int GetRecordCount(string strWhere);
        
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
        string GetPager(string tableName, string columns, string order, int pageSize, int pageIndex, string where, out int totalCount);

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
        string GetPayListPager(string tableName, string columns, string order, int pageSize, int pageIndex, string where, out int totalCount);

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        DataTable GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataTable GetList(string strWhere);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataTable GetStudentsList(string strWhere);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataTable GetExportStudentsList(string strWhere);

        /// <summary>
        /// 缴学费
        /// </summary>
        /// <param name="stuID">学员编号</param>
        /// <param name="money">缴费金额</param>
        /// <param name="remark">备注信息</param>
        /// <returns></returns>
        bool DoTuition(PayModel model);

        /// <summary>
        /// 缴考试费
        /// </summary>
        /// <param name="stuID">学员编号</param>
        /// <param name="money">缴费金额</param>
        /// <param name="remark">备注信息</param>
        /// <returns></returns>
        bool PayExam(PayExamModel model);
        

        /// <summary>
        /// 退学
        /// </summary>
        /// <param name="stuID">学员编号</param>
        /// <param name="dropMoney">退费金额</param>
        /// <param name="remark">备注信息</param>
        /// <returns></returns>
        bool DropOut(long stuID, decimal dropMoney, string remark);

        /// <summary>
        /// 绑定期数
        /// </summary>
        /// <returns></returns>
        DataTable BindPeroid();

        string GetShoudPayByIsLocal(bool isLocal);

        /// <summary>
        /// 分页获取分配学员数据列表
        /// </summary>
        DataTable GetDistributeStuListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        /// <summary>
        /// 获取分配学员记录总数
        /// </summary>
        int GetDistributeStuRecordCount(string strWhere);

        /// <summary>
        /// 获得给定期数学员信息
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        DataSet GetStudentsByPeroid(int period);
    }
}
