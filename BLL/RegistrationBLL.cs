using DriveMgr.Common;
using DriveMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class RegistrationBLL
    {
        private static readonly DriveMgr.IDAL.IRegistrationDAL dal = DriveMgr.DALFactory.Factory.GetRegistrationDAL();

		#region  Method

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(DriveMgr.Model.RegistrationModel model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(DriveMgr.Model.RegistrationModel model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(long ID)
		{
			
			return dal.Delete(ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(IDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DriveMgr.Model.RegistrationModel GetModel(long ID)
		{
			
			return dal.GetModel(ID);
		}


		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DriveMgr.Model.RegistrationModel> DataTableToList(DataTable dt)
		{
			List<DriveMgr.Model.RegistrationModel> modelList = new List<DriveMgr.Model.RegistrationModel>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DriveMgr.Model.RegistrationModel model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new DriveMgr.Model.RegistrationModel();
					if(dt.Rows[n]["ID"]!=null && dt.Rows[n]["ID"].ToString()!="")
					{
						model.ID=long.Parse(dt.Rows[n]["ID"].ToString());
					}
					if(dt.Rows[n]["StudentsName"]!=null && dt.Rows[n]["StudentsName"].ToString()!="")
					{
					model.StudentsName=dt.Rows[n]["StudentsName"].ToString();
					}
					if(dt.Rows[n]["StudentCode"]!=null && dt.Rows[n]["StudentCode"].ToString()!="")
					{
					model.StudentCode=dt.Rows[n]["StudentCode"].ToString();
					}
					if(dt.Rows[n]["Sex"]!=null && dt.Rows[n]["Sex"].ToString()!="")
					{
						if((dt.Rows[n]["Sex"].ToString()=="1")||(dt.Rows[n]["Sex"].ToString().ToLower()=="true"))
						{
						model.Sex=true;
						}
						else
						{
							model.Sex=false;
						}
					}
					if(dt.Rows[n]["Age"]!=null && dt.Rows[n]["Age"].ToString()!="")
					{
						model.Age=int.Parse(dt.Rows[n]["Age"].ToString());
					}
					if(dt.Rows[n]["IsLocal"]!=null && dt.Rows[n]["IsLocal"].ToString()!="")
					{
						if((dt.Rows[n]["IsLocal"].ToString()=="1")||(dt.Rows[n]["IsLocal"].ToString().ToLower()=="true"))
						{
						model.IsLocal=true;
						}
						else
						{
							model.IsLocal=false;
						}
					}
					if(dt.Rows[n]["PeriodsID"]!=null && dt.Rows[n]["PeriodsID"].ToString()!="")
					{
						model.PeriodsID=int.Parse(dt.Rows[n]["PeriodsID"].ToString());
					}
					if(dt.Rows[n]["CardNum"]!=null && dt.Rows[n]["CardNum"].ToString()!="")
					{
					model.CardNum=dt.Rows[n]["CardNum"].ToString();
					}
					if(dt.Rows[n]["Address"]!=null && dt.Rows[n]["Address"].ToString()!="")
					{
					model.Address=dt.Rows[n]["Address"].ToString();
					}
					if(dt.Rows[n]["Remark"]!=null && dt.Rows[n]["Remark"].ToString()!="")
					{
					model.Remark=dt.Rows[n]["Remark"].ToString();
					}
					if(dt.Rows[n]["Status"]!=null && dt.Rows[n]["Status"].ToString()!="")
					{
						model.Status=int.Parse(dt.Rows[n]["Status"].ToString());
					}
					if(dt.Rows[n]["Operater"]!=null && dt.Rows[n]["Operater"].ToString()!="")
					{
						model.Operater=dt.Rows[n]["Operater"].ToString();
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
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
            return dal.GetPager(tableName, columns, order, pageSize, pageIndex, where, out totalCount);
        }

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
        public DataTable GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetStudentsList(string strWhere)
        {
            return dal.GetStudentsList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetExportStudentsList(string strWhere)
        {
            return dal.GetExportStudentsList(strWhere);
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
            return dal.DoTuition(model);
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
            return dal.PayExam(model);
        }
        

        /// <summary>
        /// 退学
        /// </summary>
        /// <param name="stuID">学员编号</param>
        /// <param name="dropMoney">退费金额</param>
        /// <param name="remark">备注信息</param>
        /// <returns></returns>
        public bool DropOut(long stuID, decimal dropMoney, string remark)
        {
            return dal.DropOut(stuID, dropMoney, remark);
        }

        public DataTable BindPeroid()
        {
            return dal.BindPeroid();
        }

        public string GetShoudPayByIsLocal(bool isLocal)
        {
            return dal.GetShoudPayByIsLocal(isLocal);
        }

        /// <summary>
        /// 分页获取分配学员数据列表
        /// </summary>
        public DataTable GetDistributeStuListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetDistributeStuListByPage(strWhere,orderby,startIndex,endIndex);
        }

        /// <summary>
        /// 获取分配学员记录总数
        /// </summary>
        public int GetDistributeStuRecordCount(string strWhere)
        {
            return dal.GetDistributeStuRecordCount(strWhere);
        }

        /// <summary>
        /// 读取身份证卡信息
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public RegistrationByCardModel GetInfoByCard(out string errorMsg)
        {
            RegistrationByCardHelper helper = new RegistrationByCardHelper();
            errorMsg = string.Empty;
            int port = 0;
            LookCard(out port);
            string picPath = Common.AppString.picPath;
            //@"F:\源码\DriveMgr\WebUI\admin\images\Pictures\"
            RegistrationByCardModel model = helper.ReadCardInfoWithOutFingerPrint(picPath, out errorMsg);
            return model;
        }

        public string LookCard(out int port)
        {
            RegistrationByCardHelper helper = new RegistrationByCardHelper();
            port = 0;
            string errorMsg  = helper.LookCard(out port);  //监听是否插入设备
            return errorMsg;
        }

        /// <summary>
        /// 获得给定期数学员信息
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataSet GetStudentsByPeroid(int period)
        {
            return dal.GetStudentsByPeroid(period);
        }

        /// <summary>
        /// 取数据，构造json数据，为画图做准备
        /// </summary>
        /// <param name="type"></param>
        /// <param name="checkedYear"></param>
        /// <returns></returns>
        public HighChartOptions GetStudentsHighchart(HighchartTypeEnum type, int period,string peroidName)
        {
            DataSet ds = GetStudentsByPeroid(period);
            List<Series> ser = new List<Series>();
            Series s = new Series();
            s.name = "学员情况";
            List<object> obj = new List<object>();
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                PieSeriesData pieSeriesData = new PieSeriesData();
                string tname = tname =ds.Tables[i].Rows[0][0].ToString();
                int tdata = Int32.Parse(ds.Tables[i].Rows[0][1].ToString());
                pieSeriesData.name = tname;
                pieSeriesData.y = tdata;

                obj.Add(pieSeriesData);

            }
            s.data = obj;
            s.type = ChartTypeEnum.pie.ToString();
            s.allowPointSelect = false;
            ser.Add(s);
            HighChartOptions currentChart = new HighChartOptions();
            currentChart = new HighChartOptions()
            {
                //chart = new Chart()
                //{
                //    renderTo = "highChartDiv",
                //    type = ChartTypeEnum.area.ToString(),
                //    reflow=true
                //},
                title = new Title() { text = peroidName + "期学员统计图" },
                //xAxis = new List<XAxis>() { 
                //    new XAxis(){
                //        categories = new List<string>() { "一月", "二月", "三月", "四月", "五月","六月", "七月", "八月", "九月", "十月","十一月","十二月" },
                //        reversed = false,
                //        opposite = false
                //    }
                //},
                yAxis = new YAxis() { title = new Title() { text = peroidName + "期学员统计" } },
                //tooltip = new ToolTip() { crosshairs = new List<bool>() { true, false } },
                series = ser
            };
            return currentChart;
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
        public string GetPayListOfPagerData(string ui_students_period,string ui_students_name,string ui_students_code, string order, int pageSize, int pageIndex, out int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" DeleteMark = 0 and flag=1");

            if (ui_students_name.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_students_name))   //防止sql注入
                strSql.Append( string.Format(" and StudentsName like '%{0}%'", ui_students_name.Trim()));
            if (ui_students_code.Trim() != "" && !DriveMgr.Common.SqlInjection.GetString(ui_students_code))
                strSql.Append(string.Format(" and StudentCode like '%{0}%'", ui_students_code.Trim()));
            if (ui_students_period.Trim() != "select" && ui_students_period.Trim() != "")
                strSql.Append(" and PeriodsID = '" + ui_students_period.Trim() + "'");

            return dal.GetPager("V_PayList", "PriceType, StudentsID, StudentsName, StudentCode,PeriodCode, TotalPrice, CreateDate, CreatePerson", order, pageSize, pageIndex, strSql.ToString(), out totalCount);
        }

		#endregion  Method
    }
}
