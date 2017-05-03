using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DriveMgr.IDAL;
using System.Data.SqlClient;
using System.Collections;

namespace DriveMgr.SQLServerDAL
{
    public class CoachDAL : ICoachDAL
    {
        public string GetPager(string tableName, string columns, string order, int pageSize, int pageIndex, string where, out int totalCount)
        {
            DataTable dt = DriveMgr.Common.SqlPagerHelper.GetPagerOfDrive(tableName, columns, order, pageSize, pageIndex, where, out totalCount);
            return DriveMgr.Common.JsonHelper.ToJson(dt);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(DriveMgr.Model.CoachModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_Coach(");
            strSql.Append("CoachName,CardNum,Age,Sex,Phone,Address)");
            strSql.Append(" values (");
            strSql.Append("@CoachName,@CardNum,@Age,@Sex,@Phone,@Address)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CoachName", SqlDbType.VarChar,20),
					new SqlParameter("@CardNum", SqlDbType.VarChar,30),
					new SqlParameter("@Age", SqlDbType.Int,4),
					new SqlParameter("@Sex", SqlDbType.Bit,1),
					new SqlParameter("@Phone", SqlDbType.VarChar,30),
					new SqlParameter("@Address", SqlDbType.VarChar,200)};
            parameters[0].Value = model.CoachName;
            parameters[1].Value = model.CardNum;
            parameters[2].Value = model.Age;
            parameters[3].Value = model.Sex;
            parameters[4].Value = model.Phone;
            parameters[5].Value = model.Address;

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
        public bool Update(DriveMgr.Model.CoachModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Coach set ");
            strSql.Append("CoachName=@CoachName,");
            strSql.Append("CardNum=@CardNum,");
            strSql.Append("Age=@Age,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("Phone=@Phone,");
            strSql.Append("Address=@Address");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@CoachName", SqlDbType.VarChar,20),
					new SqlParameter("@CardNum", SqlDbType.VarChar,30),
					new SqlParameter("@Age", SqlDbType.Int,4),
					new SqlParameter("@Sex", SqlDbType.Bit,1),
					new SqlParameter("@Phone", SqlDbType.VarChar,30),
					new SqlParameter("@Address", SqlDbType.VarChar,200),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.CoachName;
            parameters[1].Value = model.CardNum;
            parameters[2].Value = model.Age;
            parameters[3].Value = model.Sex;
            parameters[4].Value = model.Phone;
            parameters[5].Value = model.Address;
            parameters[6].Value = model.ID;

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
        /// 删除一条数据(伪删除)
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Coach set CoachStatus=0 ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

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
        /// 批量删除数据(伪删除)
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Coach set CoachStatus=0 ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = Convert.ToInt32(DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), null));
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
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,CoachName,CardNum,Age,Sex,Phone,Address,CoachStatus ");
            strSql.Append(" FROM tb_Coach ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DriveMgr.Common.SqlHelper.GetDataTable(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.Text, strSql.ToString(), null);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM tb_Coach ");
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
        /// 记录分配学员情况
        /// </summary>
        /// <param name="coachID"></param>
        /// <param name="dtStudents"></param>
        /// <returns></returns>
        public bool AddDistributeStu(int coachID, DataTable dtStudents, string operater)
        {
            if (dtStudents == null)
            {
                return false;
            }
            Hashtable ht = new Hashtable();
            List<SqlParameter[]> listArr = new List<SqlParameter[]>();
            for (int i = 0; i < dtStudents.Rows.Count; i++)
            {
                SqlParameter[] parameters = {
					new SqlParameter("@CoachID", SqlDbType.Int),
					new SqlParameter("@StudentsID", SqlDbType.BigInt),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
                                            new SqlParameter("@Operater", SqlDbType.VarChar,50)};
                parameters[0].Value = coachID;
                parameters[1].Value = Int32.Parse(dtStudents.Rows[i]["StudentsID"].ToString());
                parameters[2].Value = DateTime.Now;
                parameters[3].Value = operater;
                listArr.Add(parameters);

                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"if not exists(select 1 from tb_DistributeStudents where
                               StudentsID=@StudentsID) insert into tb_DistributeStudents(");
                strSql.Append("CoachID,StudentsID,CreateTime,Operater,DistributeStuStatus)");
                strSql.Append(" values (");
                strSql.Append("@CoachID,@StudentsID,@CreateTime,@Operater,1)");

                strSql.Append(@"else update tb_DistributeStudents
                                 set CoachID=@CoachID,CreateTime=@CreateTime,Operater=@Operater,DistributeStuStatus=1
                                 where StudentsID=@StudentsID");

                ht.Add(strSql, listArr[i]);
            }

            bool result = DriveMgr.Common.SqlHelper.ExecuteNonQuery(DriveMgr.Common.SqlHelper.connStrDriveData, ht);
            return result;
        }

        /// <summary>
        /// 编辑分配学员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditDistributeStudents(Model.DistributeStudentsModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_DistributeStudents set ");
            strSql.Append("CoachID=@CoachID,CreateTime=@CreateTime,Operater=@Operater,DistributeStuStatus=1");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@CoachID", SqlDbType.Int),
					new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@Operater", SqlDbType.VarChar,50)};
            parameters[0].Value = model.CoachID;
            parameters[1].Value = model.ID;
            parameters[2].Value = model.CreateTime;
            parameters[3].Value = model.Operater;

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
        /// 获得教练培训学员情况
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataSet GetCoachTeachInfo(int peridID)
        {
            //存储过程的形式
            SqlParameter[] parameters = {
					new SqlParameter("@peroidID", SqlDbType.Int)};
            parameters[0].Value = peridID;

            return DriveMgr.Common.SqlHelper.GetDataset(DriveMgr.Common.SqlHelper.connStrDriveData, CommandType.StoredProcedure, "up_coachStudents", parameters);
        }
    }
}
