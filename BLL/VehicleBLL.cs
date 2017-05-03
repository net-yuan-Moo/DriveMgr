using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class VehicleBLL
    {
        private static readonly DriveMgr.IDAL.IVehicleDAL vehicleDal = DriveMgr.DALFactory.Factory.GetVehicleDAL();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddVehicle(Model.VehicleModel model)
        {
            return vehicleDal.AddVehicle(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateVehicle(Model.VehicleModel model)
        {
            return vehicleDal.UpdateVehicle(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteVehicle(int id)
        {
            return vehicleDal.DeleteVehicle(id);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteVehicleList(string idlist)
        {
            return vehicleDal.DeleteVehicleList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.VehicleModel GetVehicleModel(int id)
        {
            return vehicleDal.GetVehicleModel(id);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="licencePlateNum">车牌号</param>
        /// <param name="brands">品牌</param>
        /// <param name="createStartDate">开始时间</param>
        /// <param name="createEndDate">结束时间</param>
        /// <param name="order">排序</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public string GetPagerData(string licencePlateNum, string brands, string createStartDate, string createEndDate, string order, int pageSize, int pageIndex, out int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DeleteMark = 0 ");
            if (licencePlateNum.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(licencePlateNum))
            {
                strSql.Append(" and LicencePlateNum like '%" + licencePlateNum + "%'");
            }
            if (brands.Trim() != string.Empty && !DriveMgr.Common.SqlInjection.GetString(brands))
            {
                strSql.Append(" and Brands like '%" + brands + "%'");
            }
            if (createStartDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate > '" + createStartDate + "'");
            }
            if (createEndDate.Trim() != string.Empty)
            {
                strSql.Append(" and CreateDate < '" + createEndDate + "'");
            }
            return vehicleDal.GetPagerData("tb_Vehicle", "Id,LicencePlateNum,Brands,CarModel,BuyPrice,BuyDate,Owner,Remark,Status,CreatePerson,CreateDate,UpdatePerson,UpdateDate", order, pageSize, pageIndex, strSql.ToString(), out totalCount);
        }

        /// <summary>
        /// 获取车牌号集合
        /// </summary>
        /// <returns></returns>
        public string GetVehicleDT()
        {
            return vehicleDal.GetVehicleDT();
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            return vehicleDal.GetList(strWhere);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return vehicleDal.GetRecordCount(strWhere);
        }

        /// <summary>
        /// 记录分配车辆情况
        /// </summary>
        /// <param name="vehicleID"></param>
        /// <param name="dtStudents"></param>
        /// <param name="operater"></param>
        /// <returns></returns>
        public bool AddDistributeVehicle(int vehicleID, int subjectId, DataTable dtStudents, string operater)
        {
            return vehicleDal.AddDistributeVehicle(vehicleID,subjectId,dtStudents,operater);
        }

         /// <summary>
        /// 记录分配车辆情况
        /// </summary>
        /// <param name="vehicleID"></param>
        /// <param name="dtStudents"></param>
        /// <param name="operater"></param>
        /// <returns></returns>
        public string AddDistributeVehicle(int subjectId,string operater)
        {
            //根据车辆数量，学员数量平均分配
            //比如：111个学员,10个车辆。则每个车辆分配：111/10=12(个)学员
            try
            {
                RegistrationBLL bllStu = new RegistrationBLL();

                int vehicleCount = this.GetRecordCount(" Status = 0 and DeleteMark = 0");

                //获得未分车的学员个数

                int studentsCount = bllStu.GetRecordCount(@" flag=1  and PeriodsID = (SELECT TOP 1 CurrentPeroidID FROM tb_CurrentPeroid) 
                                 and A.DistributeVihicleStatus=0 and SubjectID='"+subjectId+"'");
                int yushu = 0;
                int shoudDistributeCount = Math.DivRem(studentsCount, vehicleCount, out yushu);
                if (yushu != 0)
                {
                    shoudDistributeCount += 1;
                }
                //int shoudDistributeCount = (studentsCount / coachCount) + 1;

                //获得未分车的学员
                DataTable dtVehicle = this.GetList(" Status = 0 and DeleteMark = 0"); //0为启用
                int startIndex = 0;
                int endIndex = shoudDistributeCount;
                for (int i = 0; i < dtVehicle.Rows.Count; i++)
                {
                    
                    DataTable dtStudents = bllStu.GetListByPage(@"  flag=1  and PeriodsID = (SELECT TOP 1 CurrentPeroidID FROM tb_CurrentPeroid)
                                      and DistributeVihicleStatus=0  and SubjectID='" + subjectId + "' ", "ID", startIndex, endIndex);

                    int vehicleID = Int32.Parse(dtVehicle.Rows[i]["Id"].ToString());
                    bool result = AddDistributeVehicle(vehicleID,subjectId, dtStudents, operater);
                }

                return "共有学员" + studentsCount + "个;车辆" + vehicleCount + "个;每个车辆分得学员" + shoudDistributeCount + "个.";
            }
            catch
            {
                return "自动分配车辆出错，请检查！";
            }
        }

        /// <summary>
        /// 修改分配车辆信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditDistributeStudents(Model.DistributionVehicleModel model)
        {
            return vehicleDal.EditDistributeStudents(model);
        }
    }
}
