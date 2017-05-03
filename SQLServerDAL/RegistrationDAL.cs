using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DriveMgr.IDAL;
using DriveMgr.Model;
using System.Collections;

namespace DriveMgr.SQLServerDAL
{
    public class RegistrationDAL : IRegistrationDAL
    {
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DriveMgr.Model.RegistrationModel GetModel(long stuId)
        {
            StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 ID,StudentsName,StudentCode,Sex,Age,PhoneNum,IsLocal,PeriodsID,CardNum,Address,PicPath,Remark,Status,Operater,flag from tb_Students ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt)
			};
            parameters[0].Value = stuId;

            DriveMgr.Model.RegistrationModel model = new DriveMgr.Model.RegistrationModel();
            DataTable dt = DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), parameters);
            if (dt.Rows.Count > 0)
			{
				if(dt.Rows[0]["ID"]!=null && dt.Rows[0]["ID"].ToString()!="")
				{
					model.ID=long.Parse(dt.Rows[0]["ID"].ToString());
				}
				if(dt.Rows[0]["StudentsName"]!=null && dt.Rows[0]["StudentsName"].ToString()!="")
				{
					model.StudentsName=dt.Rows[0]["StudentsName"].ToString();
				}
				if(dt.Rows[0]["StudentCode"]!=null && dt.Rows[0]["StudentCode"].ToString()!="")
				{
					model.StudentCode=dt.Rows[0]["StudentCode"].ToString();
				}
				if(dt.Rows[0]["Sex"]!=null && dt.Rows[0]["Sex"].ToString()!="")
				{
					if((dt.Rows[0]["Sex"].ToString()=="1")||(dt.Rows[0]["Sex"].ToString().ToLower()=="true"))
					{
						model.Sex=true;
					}
					else
					{
						model.Sex=false;
					}
				}
				if(dt.Rows[0]["Age"]!=null && dt.Rows[0]["Age"].ToString()!="")
				{
					model.Age=int.Parse(dt.Rows[0]["Age"].ToString());
				}
                if (dt.Rows[0]["PhoneNum"] != null && dt.Rows[0]["PhoneNum"].ToString() != "")
                {
                    model.PhoneNum = dt.Rows[0]["PhoneNum"].ToString();
                }
				if(dt.Rows[0]["IsLocal"]!=null && dt.Rows[0]["IsLocal"].ToString()!="")
				{
					if((dt.Rows[0]["IsLocal"].ToString()=="1")||(dt.Rows[0]["IsLocal"].ToString().ToLower()=="true"))
					{
						model.IsLocal=true;
					}
					else
					{
						model.IsLocal=false;
					}
				}
				if(dt.Rows[0]["PeriodsID"]!=null && dt.Rows[0]["PeriodsID"].ToString()!="")
				{
					model.PeriodsID=int.Parse(dt.Rows[0]["PeriodsID"].ToString());
				}
				if(dt.Rows[0]["CardNum"]!=null && dt.Rows[0]["CardNum"].ToString()!="")
				{
					model.CardNum=dt.Rows[0]["CardNum"].ToString();
				}
				if(dt.Rows[0]["Address"]!=null && dt.Rows[0]["Address"].ToString()!="")
				{
					model.Address=dt.Rows[0]["Address"].ToString();
				}
				if(dt.Rows[0]["PicPath"]!=null && dt.Rows[0]["PicPath"].ToString()!="")
				{
					model.PicPath=dt.Rows[0]["PicPath"].ToString();
				}
				if(dt.Rows[0]["Remark"]!=null && dt.Rows[0]["Remark"].ToString()!="")
				{
					model.Remark=dt.Rows[0]["Remark"].ToString();
				}
				if(dt.Rows[0]["Status"]!=null && dt.Rows[0]["Status"].ToString()!="")
				{
					model.Status=int.Parse(dt.Rows[0]["Status"].ToString());
				}
				if(dt.Rows[0]["Operater"]!=null && dt.Rows[0]["Operater"].ToString()!="")
				{
					model.Operater=dt.Rows[0]["Operater"].ToString();
				}
				if(dt.Rows[0]["flag"]!=null && dt.Rows[0]["flag"].ToString()!="")
				{
					model.flag=int.Parse(dt.Rows[0]["flag"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(DriveMgr.Model.RegistrationModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_Students(");
            strSql.Append("StudentsName,StudentCode,Sex,Age,IsLocal,PhoneNum,PeriodsID,CardNum,Address,PicPath,Remark,Status,Operater)");
            strSql.Append(" values (");
            strSql.Append("@StudentsName,@StudentCode,@Sex,@Age,@IsLocal,@PhoneNum,@PeriodsID,@CardNum,@Address,@PicPath,@Remark,@Status,@Operater)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@StudentsName", SqlDbType.VarChar,20),
					new SqlParameter("@StudentCode", SqlDbType.VarChar,20),
					new SqlParameter("@Sex", SqlDbType.Bit,1),
					new SqlParameter("@Age", SqlDbType.Int,4),
					new SqlParameter("@IsLocal", SqlDbType.Bit,1),
                    new SqlParameter("@PhoneNum",SqlDbType.VarChar,50),
					new SqlParameter("@PeriodsID", SqlDbType.Int,4),
					new SqlParameter("@CardNum", SqlDbType.VarChar,20),
					new SqlParameter("@Address", SqlDbType.VarChar,200),
					new SqlParameter("@PicPath", SqlDbType.VarChar,200),
					new SqlParameter("@Remark", SqlDbType.VarChar,500),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Operater", SqlDbType.VarChar,50)};
            parameters[0].Value = model.StudentsName;
            parameters[1].Value = model.StudentCode;
            parameters[2].Value = model.Sex;
            parameters[3].Value = model.Age;
            parameters[4].Value = model.IsLocal;
            parameters[5].Value = model.PhoneNum; //手机号码
            parameters[6].Value = model.PeriodsID;
            parameters[7].Value = model.CardNum;
            parameters[8].Value = model.Address;
            parameters[9].Value = model.PicPath;
            parameters[10].Value = model.Remark;
            parameters[11].Value = model.Status;
            parameters[12].Value = model.Operater;

            int rows = Convert.ToInt32(DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), parameters));
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DriveMgr.Model.RegistrationModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Students set ");
            strSql.Append("StudentsName=@StudentsName,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("Age=@Age,");
            strSql.Append("IsLocal=@IsLocal,");
            strSql.Append("PhoneNum=@PhoneNum,");
            strSql.Append("PeriodsID=@PeriodsID,");
            strSql.Append("CardNum=@CardNum,");
            strSql.Append("Address=@Address,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("Operater=@Operater");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@StudentsName", SqlDbType.VarChar,20),
					new SqlParameter("@Sex", SqlDbType.Bit,1),
					new SqlParameter("@Age", SqlDbType.Int,4),
					new SqlParameter("@IsLocal", SqlDbType.Bit,1),
                    new SqlParameter("@PhoneNum",SqlDbType.VarChar,50),
					new SqlParameter("@PeriodsID", SqlDbType.Int,4),
					new SqlParameter("@CardNum", SqlDbType.VarChar,20),
					new SqlParameter("@Address", SqlDbType.VarChar,200),
					new SqlParameter("@Remark", SqlDbType.VarChar,500),
					new SqlParameter("@Operater", SqlDbType.VarChar,50),
					new SqlParameter("@ID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.StudentsName;
            parameters[1].Value = model.Sex;
            parameters[2].Value = model.Age;
            parameters[3].Value = model.IsLocal;
            parameters[4].Value = model.PhoneNum; //手机号码
            parameters[5].Value = model.PeriodsID;
            parameters[6].Value = model.CardNum;
            parameters[7].Value = model.Address;
            parameters[8].Value = model.Remark;
            parameters[9].Value = model.Operater;
            parameters[10].Value = model.ID;

            int rows = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据(伪删除)
        /// </summary>
        public bool Delete(long ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Students set flag=0 ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.BigInt,8)			};
            parameters[0].Value = ID;

            int rows = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据(伪删除)
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Students set flag=0 ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), null);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select count(1) FROM  dbo.tb_DistributionVehicle A
                            LEFT JOIN tb_Students B
                            ON A.StudentsID=B.ID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = Convert.ToInt32(DriveMgr.Common.SqlHelper.ExecuteScalar(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), null));
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetDistributeStuRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select count(1) FROM  dbo.tb_DistributeStudents A
                            LEFT JOIN tb_Students B
                            ON A.StudentsID=B.ID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = Convert.ToInt32(DriveMgr.Common.SqlHelper.ExecuteScalar(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), null));
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

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
        public string GetPager(string tableName, string columns, string order, int pageSize, int pageIndex, string where, out int totalCount)
        {
            DataTable dt = DriveMgr.Common.SqlPagerHelper.GetPagerOfDrive(tableName, columns, order, pageSize, pageIndex, where, out totalCount);
            return DriveMgr.Common.JsonHelper.ToJson(dt);
        }

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
        public string GetPayListPager(string tableName, string columns, string order, int pageSize, int pageIndex, string where, out int totalCount)
        {
            string connectionString = DriveMgr.Common.SqlHelper.financialMgrConn;
            DataTable dt = DriveMgr.Common.SqlPagerHelper.GetPagerData(connectionString, tableName, columns, order, pageSize, pageIndex, where, out totalCount);
            return DriveMgr.Common.JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataTable GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from V_DistributeVehicle T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), null);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataTable GetDistributeStuListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from V_DistributeStudents T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM V_ApplyTable ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetExportStudentsList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT 
      [StudentsName] as '学员姓名'
      ,[StudentCode] as '学员编号'
      
      ,CASE [Sex]
         WHEN 1 THEN '男'
         WHEN 0 THEN '女'
         ELSE '其他' END  as '性别'
      ,[Age] as '年龄'
      ,[CardNum] as '身份证号'
      ,[PhoneNum] as '电话号码'
      ,CASE [IsLocal]
         WHEN 1 THEN '是'
         WHEN 0 THEN '否'
         ELSE '其他' END  as '是否本地'

       ,CASE [Status]
         WHEN 0 THEN '在学'
         WHEN 1 THEN '毕业'
         WHEN 2 THEN '退学'
         ELSE '其他' END  as '学员状态'
         
      ,[PeriodCode] as '期数'
      ,[Address] as '家庭住址'
      ,[Remark] as '备注'
            ,[ShoudPay] as '应缴学费'
      ,[RealPay] as '实际缴费'

      ,CASE [PayStatus]
         WHEN 0 THEN '未缴费'
         WHEN 1 THEN '未完全缴费'
         WHEN 2 THEN '已缴费'
         ELSE '其他' END  as '缴费状态' FROM V_StudentsBaseData ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetStudentsList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT * FROM V_StudentsBaseData ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), null);
        }

        /// <summary>
        /// 缴学费
        /// </summary>
        /// <param name="stuID">学员编号</param>
        /// <param name="money">缴费金额</param>
        /// <param name="remark">备注信息</param>
        /// <returns></returns>
        public bool DoTuition(PayModel model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@StudentsID", SqlDbType.BigInt),
					new SqlParameter("@RealPay", SqlDbType.Decimal),
                    new SqlParameter("@SalePay", SqlDbType.Decimal),
                    new SqlParameter("@payStatus", SqlDbType.Int),
                    new SqlParameter("@Operater", SqlDbType.VarChar,50),
					new SqlParameter("@Remark", SqlDbType.VarChar,200),
                                        new SqlParameter("@ShoudPay", SqlDbType.Int)};
            parameters[0].Value = model.StudentsID;
            parameters[1].Value = model.RealPay;
            parameters[2].Value = model.SalePay;
            parameters[3].Value = model.Status;
            parameters[4].Value = model.Operater;
            parameters[5].Value = model.Remark;

            parameters[6].Value = model.ShoudPay;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Pay set RealPay=@RealPay,SalePay=@SalePay,Status=@payStatus,Operater=@Operater,Remark=@Remark,ShoudPay=@ShoudPay ");
            strSql.Append(" where StudentsID =@StudentsID");

            int rows = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 缴考试费
        /// </summary>
        /// <param name="stuID">学员编号</param>
        /// <param name="money">缴费金额</param>
        /// <param name="remark">备注信息</param>
        /// <returns></returns>
        public bool PayExam(PayExamModel model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@StudentsID", SqlDbType.BigInt),
					new SqlParameter("@RealPay", SqlDbType.Decimal),
                    new SqlParameter("@Operater", SqlDbType.VarChar,50),
					new SqlParameter("@Remark", SqlDbType.VarChar,200)};
            parameters[0].Value = model.StudentsID;
            parameters[1].Value = model.RealPay;
            parameters[2].Value = model.Operater;
            parameters[3].Value = model.Remark;

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"insert into tb_ExamPay(StudentsID,RealPay,Operater,Remark)
                            values(@StudentsID,@RealPay,@Operater,@Remark)");

            int rows = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否本地
        /// </summary>
        /// <param name="isLocal"></param>
        /// <returns></returns>
        public string GetShoudPayByIsLocal(bool isLocal)
        {
            string strSql = "";
            if (isLocal)
            {
                strSql = "select Costs FROM tb_Tuition Where LocalType=0";  //本地户口
            }
            else
            {
                strSql = "select Costs FROM tb_Tuition Where LocalType=1";  //外地户口
            }

            object obj = DriveMgr.Common.SqlHelper.ExecuteScalar(DriveMgr.Common.SqlHelper.financialMgrConn, CommandType.Text, strSql.ToString(), null);
            if (obj != null)
            {
                return obj.ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 退学
        /// </summary>
        /// <param name="stuID">学员编号</param>
        /// <param name="dropMoney">退费金额</param>
        /// <param name="remark">备注信息</param>
        /// <returns></returns>
        public bool DropOut(long stuID, decimal leaveMoney, string remark)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@StudentsID", SqlDbType.BigInt),
					new SqlParameter("@LeaveMoney", SqlDbType.Decimal),
					new SqlParameter("@Remark", SqlDbType.VarChar,200)};
            parameters[0].Value = stuID;
            parameters[1].Value = leaveMoney;
            parameters[2].Value = remark;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Pay set RealPay=@LeaveMoney ");
            strSql.Append(" where StudentsID =@StudentsID");

            StringBuilder strSqlExit = new StringBuilder();
            strSqlExit.Append("update tb_Students set Status=2"); //2:退学
            strSqlExit.Append(" where ID =@StudentsID");

            SqlParameter[] parametersExit = {
					new SqlParameter("@StudentsID", SqlDbType.BigInt)};
            parametersExit[0].Value = stuID;

            Hashtable ht = new Hashtable();
            ht.Add(strSql, parameters);
            ht.Add(strSqlExit, parametersExit);

            bool result = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.connStrDriveData,ht);
            return result;
        }

        /// <summary>
        /// 绑定期数
        /// </summary>
        /// <returns></returns>
        public DataTable BindPeroid()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT A.ID, A.PeriodCode, A.Remark, A.flag, ISNULL(B.flag, 0) AS peroidStatus
                      FROM  dbo.tb_Periods AS A LEFT OUTER JOIN
                      dbo.tb_CurrentPeroid AS B ON A.ID = B.CurrentPeroidID
                      
                       where A.flag=1 order by peroidStatus desc");  //有效的期数

            return DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), null);
        }

        /// <summary>
        /// 获得给定期数学员信息
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataSet GetStudentsByPeroid(int period)
        {
            //存储过程的形式
            SqlParameter[] parameters = {
					new SqlParameter("@peroidID", SqlDbType.Int)};
            parameters[0].Value = period;

            return DriveMgr.Common.SqlHelper.GetDataset(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.StoredProcedure, "up_graduater", parameters);
        }
    }
}
