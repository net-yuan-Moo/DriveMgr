using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DriveMgr.Common
{
    /// <summary>
    /// SQL分页帮助类
    /// </summary>
    public class SqlPagerHelper
    {
        /// <summary>
        /// 获取分页数据（单表分页）
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">要取的列名（逗号分开）</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="where">查询条件</param>
        /// <param name="totalCount">总记录数</param>
        public static DataTable GetPager(string tableName, string columns, string order, int pageSize, int pageIndex, string where, out int totalCount)
        {
            SqlParameter[] paras = { 
                                   new SqlParameter("@tablename",SqlDbType.VarChar,100),
                                   new SqlParameter("@columns",SqlDbType.VarChar,500),
                                   new SqlParameter("@order",SqlDbType.VarChar,100),
                                   new SqlParameter("@pageSize",SqlDbType.Int),
                                   new SqlParameter("@pageIndex",SqlDbType.Int),
                                   new SqlParameter("@where",SqlDbType.VarChar,2000),
                                   new SqlParameter("@totalCount",SqlDbType.Int)
                                   };
            paras[0].Value = tableName;
            paras[1].Value = columns;
            paras[2].Value = order;
            paras[3].Value = pageSize;
            paras[4].Value = pageIndex;
            paras[5].Value = where;
            paras[6].Direction = ParameterDirection.Output;   //输出参数

            DataTable dt = DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.connStr, CommandType.StoredProcedure, "sp_Pager", paras);
            totalCount = Convert.ToInt32(paras[6].Value);   //赋值输出参数，即当前记录总数
            return dt;
        }

        /// <summary>
        /// 获取分页数据（单表分页）
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">要取的列名（逗号分开）</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="where">查询条件</param>
        /// <param name="totalCount">总记录数</param>
        public static DataTable GetPagerOfDrive(string tableName, string columns, string order, int pageSize, int pageIndex, string where, out int totalCount)
        {
            SqlParameter[] paras = { 
                                   new SqlParameter("@tablename",SqlDbType.VarChar,100),
                                   new SqlParameter("@columns",SqlDbType.VarChar,500),
                                   new SqlParameter("@order",SqlDbType.VarChar,100),
                                   new SqlParameter("@pageSize",SqlDbType.Int),
                                   new SqlParameter("@pageIndex",SqlDbType.Int),
                                   new SqlParameter("@where",SqlDbType.VarChar,2000),
                                   new SqlParameter("@totalCount",SqlDbType.Int)
                                   };
            paras[0].Value = tableName;
            paras[1].Value = columns;
            paras[2].Value = order;
            paras[3].Value = pageSize;
            paras[4].Value = pageIndex;
            paras[5].Value = where;
            paras[6].Direction = ParameterDirection.Output;   //输出参数

            DataTable dt = DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.StoredProcedure, "sp_Pager", paras);
            totalCount = Convert.ToInt32(paras[6].Value);   //赋值输出参数，即当前记录总数
            return dt;
        }

        /// <summary>
        /// 获取分页数据（单表分页）
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">要取的列名（逗号分开）</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="where">查询条件</param>
        /// <param name="totalCount">总记录数</param>
        public static DataTable GetPagerData(string connectionString, string tableName, string columns, string order, int pageSize, int pageIndex, string where, out int totalCount)
        {
            SqlParameter[] paras = { 
                                   new SqlParameter("@tablename",SqlDbType.VarChar,100),
                                   new SqlParameter("@columns",SqlDbType.VarChar,500),
                                   new SqlParameter("@order",SqlDbType.VarChar,100),
                                   new SqlParameter("@pageSize",SqlDbType.Int),
                                   new SqlParameter("@pageIndex",SqlDbType.Int),
                                   new SqlParameter("@where",SqlDbType.VarChar,2000),
                                   new SqlParameter("@totalCount",SqlDbType.Int)
                                   };
            paras[0].Value = tableName;
            paras[1].Value = columns;
            paras[2].Value = order;
            paras[3].Value = pageSize;
            paras[4].Value = pageIndex;
            paras[5].Value = where;
            paras[6].Direction = ParameterDirection.Output;   //输出参数

            DataTable dt = DriveMgr.Common.SqlHelper.GetDataTable(connectionString, CommandType.StoredProcedure, "sp_Pager", paras);
            //totalCount = Convert.ToInt32(paras[6].Value);   //赋值输出参数，即当前记录总数
            totalCount = 0;   //赋值输出参数，即当前记录总数
            return dt;
        }
    }
}
