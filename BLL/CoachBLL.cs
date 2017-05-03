using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
//
using DriveMgr.BLL;
using DriveMgr.Common;

namespace DriveMgr.BLL
{
    public class CoachBLL
    {
        private static readonly DriveMgr.IDAL.ICoachDAL dal = DriveMgr.DALFactory.Factory.GetCoachDAL();

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
        /// 增加一条数据
        /// </summary>
        public bool Add(DriveMgr.Model.CoachModel model)
        {
            return dal.Add(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DriveMgr.Model.CoachModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据(伪删除)
        /// </summary>
        public bool Delete(int ID)
        {
            return dal.Delete(ID);
        }
        /// <summary>
        /// 批量删除数据(伪删除)
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 记录分配学员情况
        /// </summary>
        /// <param name="coachID"></param>
        /// <param name="dtStudents"></param>
        /// <returns></returns>
        public bool AddDistributeStu(int coachID, DataTable dtStudents, string operater)
        {
            return dal.AddDistributeStu(coachID, dtStudents, operater);
        }

        /// <summary>
        /// 自动分配学员
        /// </summary>
        /// <returns></returns>
        public string AutoDistributeStudents(string operater)
        {
            //根据教练数量，学员数量平均分配
            //比如：111个学员,10个教练。则每个教练分配：111/10=12(个)学员
            try
            {
                RegistrationBLL bllStu = new RegistrationBLL();
                CoachBLL bllCoach = new CoachBLL();

                //得到有效的教练个数
                int coachCount = this.GetRecordCount(" CoachStatus=1");

                //得到还未分配教练的学员个数
                int studentsCount = bllStu.GetDistributeStuRecordCount(@" flag=1  and PeriodsID = (SELECT TOP 1 CurrentPeroidID FROM tb_CurrentPeroid)
                and DistributeStuStatus=0 ");

                int yushu = 0;
                int shoudDistributeCount = Math.DivRem(studentsCount, coachCount, out yushu);
                if (yushu != 0)
                {
                    shoudDistributeCount += 1;
                }
                //int shoudDistributeCount = (studentsCount / coachCount) + 1;

                DataTable dtCoach = this.GetList(" CoachStatus=1");
                int startIndex = 0;
                int endIndex = shoudDistributeCount;

                for (int i = 0; i < dtCoach.Rows.Count; i++)
                {
                    DataTable dtStudents = bllStu.GetDistributeStuListByPage(@"  flag=1  and PeriodsID = (SELECT TOP 1 CurrentPeroidID FROM tb_CurrentPeroid)
                    and DistributeStuStatus=0 ", "ID", startIndex, endIndex);

                    int coachID = Int32.Parse(dtCoach.Rows[i]["ID"].ToString());
                    bool result = AddDistributeStu(coachID, dtStudents, operater);
                }

                return "共有学员" + studentsCount + "个;教练" + coachCount + "个;每个教练分得学员" + shoudDistributeCount + "个.";
            }
            catch
            {
                return "自动分配学员出错，请检查！";
            }
        }

        /// <summary>
        /// 更新分配学员信息
        /// </summary>
        public bool EditDistributeStudents(DriveMgr.Model.DistributeStudentsModel model)
        {
            return dal.EditDistributeStudents(model);
        }

        /// <summary>
        /// 获得教练培训学员情况
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataSet GetCoachTeachInfo(int peridID)
        {
            return dal.GetCoachTeachInfo(peridID);
        }

        /// <summary>
        /// 取数据，构造json数据，为画图做准备
        /// </summary>
        /// <param name="type"></param>
        /// <param name="checkedYear"></param>
        /// <returns></returns>
        public HighChartOptions GetCoachTeachHighchart(HighchartTypeEnum type, int peridID, string peridName)
        {
            DataSet ds = GetCoachTeachInfo(peridID);
            List<Series> ser = new List<Series>();
            List<string> xaxis = new List<string>();

            for (int j = 1; j < ds.Tables[0].Columns.Count; j++)
            {
                Series s = new Series();
                List<object> obj = new List<object>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string coachName = ds.Tables[0].Rows[i]["CoachName"].ToString();
                   
                    xaxis.Add(coachName);
                    int tname = Int32.Parse(ds.Tables[0].Rows[i][j].ToString());
                    obj.Add(tname);
                }
                
                s.data = obj;
                s.type = ChartTypeEnum.column.ToString();
                s.allowPointSelect = false;
                if (j == 1)
                {
                    s.name = "在学";
                }
                else if (j == 2)
                {
                    s.name = "毕业";
                }
                else if (j == 3)
                {
                    s.name = "退学";
                }
                ser.Add(s);
                
            }
            HighChartOptions currentChart = new HighChartOptions();
            currentChart = new HighChartOptions()
            {
                //chart = new Chart()
                //{
                //    renderTo = "highChartDiv",
                //    type = ChartTypeEnum.area.ToString(),
                //    reflow=true
                //},
                title = new Title() { text = peridName + "期教练培训学员情况图" },
                xAxis = new List<XAxis>() { 
                    new XAxis(){
                        categories = xaxis,
                        reversed = false,
                        opposite = false
                    }
                },
                yAxis = new YAxis() { title = new Title() { text = peridName + "期教练培训学员" } },
                //tooltip = new ToolTip() { crosshairs = new List<bool>() { true, false } },
                series = ser
            };
            return currentChart;
        }
    }
}
