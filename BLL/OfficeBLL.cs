/***********************
 @ author:zlong
 @ Date:2015-01-08
 @ Desc:办公费用管理逻辑层
 * ********************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class OfficeBLL
    {
        private static readonly DriveMgr.IDAL.IOfficeDAL officeDal = DriveMgr.DALFactory.Factory.GetOfficeDAL();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddOffice(Model.OfficeModel model)
        {
            return officeDal.AddOffice(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateOffice(Model.OfficeModel model)
        {
            return officeDal.UpdateOffice(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteOffice(int id)
        {
            return officeDal.DeleteOffice(id);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteOfficeList(string idlist)
        {
            return officeDal.DeleteOfficeList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.OfficeModel GetOfficeModel(int id)
        {
            return officeDal.GetOfficeModel(id);
        }


        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="officeUse">用途</param>
        /// <param name="tagPerson">经办人</param>
        /// <param name="createStartDate">开始时间</param>
        /// <param name="createEndDate">结束时间</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public string GetPagerData(string officeUse, string tagPerson, string createStartDate, string createEndDate, string order, int pageSize, int pageIndex, out int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DeleteMark = 0 ");
            if (officeUse.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(officeUse))
            {
                strSql.Append(" and OfficeUse like '%" + officeUse + "%'");
            }
            if (tagPerson.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(tagPerson))
            {
                strSql.Append(" and TagPerson like '%" + tagPerson + "%'");
            }            
            if (createStartDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate > '" + createStartDate + "'");
            }
            if (createEndDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate < '" + createEndDate + "'");
            }
            return officeDal.GetPagerData("tb_Office", "Id,OfficeUse,TagPerson,UseDate,OfficeAmount,Remark,CreatePerson,CreateDate,UpdatePerson,UpdateDate", order, pageSize, pageIndex, strSql.ToString(), out totalCount);
        }

    }
}
