using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DriveMgr.IDAL
{
    public interface ICoachDAL
    {
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
        /// 增加一条数据
        /// </summary>
        bool Add(DriveMgr.Model.CoachModel model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(DriveMgr.Model.CoachModel model);

        /// <summary>
        /// 删除一条数据(伪删除)
        /// </summary>
        bool Delete(int ID);

        /// <summary>
        /// 批量删除数据(伪删除)
        /// </summary>
        bool DeleteList(string IDlist);

        /// <summary>
        /// 获取记录总数
        /// </summary>
        int GetRecordCount(string strWhere);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataTable GetList(string strWhere);

         /// <summary>
        /// 记录分配学员情况
        /// </summary>
        /// <param name="coachID"></param>
        /// <param name="dtStudents"></param>
        /// <returns></returns>
        bool AddDistributeStu(int coachID, DataTable dtStudents, string operater);

        /// <summary>
        /// 更新分配学员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool EditDistributeStudents(Model.DistributeStudentsModel model);

        /// <summary>
        /// 获得教练培训学员情况
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        DataSet GetCoachTeachInfo(int peridID);
    }
}
