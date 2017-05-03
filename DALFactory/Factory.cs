using System;
using System.Collections.Generic;
using System.Text;

using log4net;

//Blog：oppoic.cnblogs.com
//QQ群：33353329

namespace DriveMgr.DALFactory
{
    /// <summary>
    /// 工厂类：创建访问数据库的实例对象
    /// </summary>
    public class Factory
    {
        /// <summary>
        /// 根据传入的类名获取实例对象
        /// </summary>
        private static object GetInstance(string name)
        {
            //ILog log = LogManager.GetLogger(typeof(Factory));  //初始化日志记录器

            string configName = System.Configuration.ConfigurationManager.AppSettings["DataAccess"];
            if (string.IsNullOrEmpty(configName))
            {
                //log.Fatal("没有从配置文件中获取命名空间名称！");   //Fatal致命错误，优先级最高
                throw new InvalidOperationException();    //抛错，代码不会向下执行了
            }

            string className = string.Format("{0}.{1}", configName, name);  //DriveMgr.SQLServerDAL.传入的类名name

            //加载程序集
            System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(configName);
            //创建指定类型的对象实例
            return assembly.CreateInstance(className);
        }

        /// <summary>
        /// 利用反射获取访问登录信息的数据访问对象（结合配置文件app.config）
        /// </summary>
        public static DriveMgr.IDAL.IAuthority GetAuthorityDAL()
        {
            DriveMgr.IDAL.IAuthority authority = GetInstance("Authority") as DriveMgr.IDAL.IAuthority;
            return authority;
        }

        public static DriveMgr.IDAL.IBug GetBugDAL()
        {
            DriveMgr.IDAL.IBug bug = GetInstance("Bug") as DriveMgr.IDAL.IBug;
            return bug;
        }

        public static DriveMgr.IDAL.IButton GetButtonDAL()
        {
            DriveMgr.IDAL.IButton button = GetInstance("Button") as DriveMgr.IDAL.IButton;
            return button;
        }
        public static DriveMgr.IDAL.IDepartment GetDepartmentDAL()
        {
            DriveMgr.IDAL.IDepartment department = GetInstance("Department") as DriveMgr.IDAL.IDepartment;
            return department;
        }

        public static DriveMgr.IDAL.ILoginLog GetLoginInfoDAL()
        {
            DriveMgr.IDAL.ILoginLog loginInfo = GetInstance("LoginLog") as DriveMgr.IDAL.ILoginLog;
            return loginInfo;
        }

        public static DriveMgr.IDAL.IMenu GetMenuDAL()
        {
            DriveMgr.IDAL.IMenu menu = GetInstance("Menu") as DriveMgr.IDAL.IMenu;
            return menu;
        }

        public static DriveMgr.IDAL.IRole GetRoleDAL()
        {
            DriveMgr.IDAL.IRole role = GetInstance("Role") as DriveMgr.IDAL.IRole;
            return role;
        }

        public static DriveMgr.IDAL.IRoleMenuButton GetRoleMenuButtonDAL()
        {
            DriveMgr.IDAL.IRoleMenuButton roleMenuButton = GetInstance("RoleMenuButton") as DriveMgr.IDAL.IRoleMenuButton;
            return roleMenuButton;
        }

        public static DriveMgr.IDAL.IUser GetUserDAL()
        {
            DriveMgr.IDAL.IUser user = GetInstance("User") as DriveMgr.IDAL.IUser;
            return user;
        }

        public static DriveMgr.IDAL.IUserDepartment GetUserDepartmentDAL()
        {
            DriveMgr.IDAL.IUserDepartment userDepartment = GetInstance("UserDepartment") as DriveMgr.IDAL.IUserDepartment;
            return userDepartment;
        }

        public static DriveMgr.IDAL.IUserOperateLog GetUserOperateLogDAL()
        {
            DriveMgr.IDAL.IUserOperateLog userOperateLog = GetInstance("UserOperateLog") as DriveMgr.IDAL.IUserOperateLog;
            return userOperateLog;
        }

        public static DriveMgr.IDAL.IUserRole GetUserRoleDAL()
        {
            DriveMgr.IDAL.IUserRole userRole = GetInstance("UserRole") as DriveMgr.IDAL.IUserRole;
            return userRole;
        }

        public static DriveMgr.IDAL.IRegistrationDAL GetRegistrationDAL()
        {
            DriveMgr.IDAL.IRegistrationDAL registration = GetInstance("RegistrationDAL") as DriveMgr.IDAL.IRegistrationDAL;
            return registration;
        }

        public static DriveMgr.IDAL.IMenuButton GetMenuButtonDAL()
        {
            DriveMgr.IDAL.IMenuButton menuButton = GetInstance("MenuButton") as DriveMgr.IDAL.IMenuButton;
            return menuButton;
        }

        public static DriveMgr.IDAL.IArchivesDAL GetArchivesDAL()
        {
            DriveMgr.IDAL.IArchivesDAL archives = GetInstance("ArchivesDAL") as DriveMgr.IDAL.IArchivesDAL;
            return archives;
        }

        public static DriveMgr.IDAL.ICoachDAL GetCoachDAL()
        {
            DriveMgr.IDAL.ICoachDAL coach = GetInstance("CoachDAL") as DriveMgr.IDAL.ICoachDAL;
            return coach;
        }

        public static DriveMgr.IDAL.IPeriodsDAL GetPeriodsDAL()
        {
            DriveMgr.IDAL.IPeriodsDAL periods = GetInstance("PeriodsDAL") as DriveMgr.IDAL.IPeriodsDAL;
            return periods;
        }

        public static DriveMgr.IDAL.IScoresDAL GetScoresDAL()
        {
            DriveMgr.IDAL.IScoresDAL scores = GetInstance("ScoresDAL") as DriveMgr.IDAL.IScoresDAL;
            return scores;
        }

        public static DriveMgr.IDAL.IAppointmentDAL GetAppointmentDAL()
        {
            DriveMgr.IDAL.IAppointmentDAL appointment = GetInstance("AppointmentDAL") as DriveMgr.IDAL.IAppointmentDAL;
            return appointment;
        }

        public static DriveMgr.IDAL.IIncomeCategoryDAL GetIncomeCategoryDAL()
        {
            DriveMgr.IDAL.IIncomeCategoryDAL incomeCategory = GetInstance("IncomeCategoryDAL") as DriveMgr.IDAL.IIncomeCategoryDAL;
            return incomeCategory;
        }

        public static DriveMgr.IDAL.IBusinessEntertainDAL GetBusinessEntertainDAL()
        {
            DriveMgr.IDAL.IBusinessEntertainDAL businessEntertain = GetInstance("BusinessEntertainDAL") as DriveMgr.IDAL.IBusinessEntertainDAL;
            return businessEntertain;
        }

        public static DriveMgr.IDAL.IExpensesCategoryDAL GetExpensesCategoryDAL()
        {
            DriveMgr.IDAL.IExpensesCategoryDAL expensesCategory = GetInstance("ExpensesCategoryDAL") as DriveMgr.IDAL.IExpensesCategoryDAL;
            return expensesCategory;
        }

        public static DriveMgr.IDAL.IOfficeDAL GetOfficeDAL()
        {
            DriveMgr.IDAL.IOfficeDAL office = GetInstance("OfficeDAL") as DriveMgr.IDAL.IOfficeDAL;
            return office;
        }

        public static DriveMgr.IDAL.ILoanDAL GetLoanDAL()
        {
            DriveMgr.IDAL.ILoanDAL loan = GetInstance("LoanDAL") as DriveMgr.IDAL.ILoanDAL;
            return loan;
        }

        public static DriveMgr.IDAL.IRepaymentDAL GetRepaymentDAL()
        {
            DriveMgr.IDAL.IRepaymentDAL repayment = GetInstance("RepaymentDAL") as DriveMgr.IDAL.IRepaymentDAL;
            return repayment;
        }

        public static DriveMgr.IDAL.ISiteRentalDAL GetSiteRentalDAL()
        {
            DriveMgr.IDAL.ISiteRentalDAL siteRental = GetInstance("SiteRentalDAL") as DriveMgr.IDAL.ISiteRentalDAL;
            return siteRental;
        }

        public static DriveMgr.IDAL.ISiteRentalLocalDAL GetSiteRentalLocalDAL()
        {
            DriveMgr.IDAL.ISiteRentalLocalDAL siteRental = GetInstance("SiteRentalLocalDAL") as DriveMgr.IDAL.ISiteRentalLocalDAL;
            return siteRental;
        }

        public static DriveMgr.IDAL.IVehiclMaintenanceDAL GetVehiclMaintenanceDAL()
        {
            DriveMgr.IDAL.IVehiclMaintenanceDAL vehiclMaintenance = GetInstance("VehiclMaintenanceDAL") as DriveMgr.IDAL.IVehiclMaintenanceDAL;
            return vehiclMaintenance;
        }

        public static DriveMgr.IDAL.IVehicleRentalDAL GetVehicleRentalDAL()
        {
            DriveMgr.IDAL.IVehicleRentalDAL vehicleRental = GetInstance("VehicleRentalDAL") as DriveMgr.IDAL.IVehicleRentalDAL;
            return vehicleRental;
        }

        public static DriveMgr.IDAL.IVehicleRentalLocalDAL GetVehicleRentalLocalDAL()
        {
            DriveMgr.IDAL.IVehicleRentalLocalDAL vehicleRental = GetInstance("VehicleRentalLocalDAL") as DriveMgr.IDAL.IVehicleRentalLocalDAL;
            return vehicleRental;
        }

        public static DriveMgr.IDAL.IVehicleDAL GetVehicleDAL()
        {
            DriveMgr.IDAL.IVehicleDAL vehicle = GetInstance("VehicleDAL") as DriveMgr.IDAL.IVehicleDAL;
            return vehicle;
        }

        public static DriveMgr.IDAL.ITravelDAL GetTravelDAL()
        {
            DriveMgr.IDAL.ITravelDAL travel = GetInstance("TravelDAL") as DriveMgr.IDAL.ITravelDAL;
            return travel;
        }

        public static DriveMgr.IDAL.IPriceConfigDAL GetPriceConfigDAL()
        {
            DriveMgr.IDAL.IPriceConfigDAL priceConfig = GetInstance("PriceConfigDAL") as DriveMgr.IDAL.IPriceConfigDAL;
            return priceConfig;
        }

        public static DriveMgr.IDAL.ITuitionDAL GetTuitionDAL()
        {
            DriveMgr.IDAL.ITuitionDAL tuition = GetInstance("TuitionDAL") as DriveMgr.IDAL.ITuitionDAL;
            return tuition;
        }
        public static DriveMgr.IDAL.IGoodsCategoryDAL GetGoodsCategoryDAL()
        {
            DriveMgr.IDAL.IGoodsCategoryDAL goodsCategory = GetInstance("GoodsCategoryDAL") as DriveMgr.IDAL.IGoodsCategoryDAL;
            return goodsCategory;
        }

        public static DriveMgr.IDAL.IGoodsDAL GetGoodsDAL()
        {
            DriveMgr.IDAL.IGoodsDAL goods = GetInstance("GoodsDAL") as DriveMgr.IDAL.IGoodsDAL;
            return goods;
        }

        public static DriveMgr.IDAL.IEnterStoreHouseDAL GetEnterStoreHouseDAL()
        {
            DriveMgr.IDAL.IEnterStoreHouseDAL enterStoreHouse = GetInstance("EnterStoreHouseDAL") as DriveMgr.IDAL.IEnterStoreHouseDAL;
            return enterStoreHouse;
        }

        public static DriveMgr.IDAL.IOutStoreHouseDAL GetOutStoreHouseDAL()
        {
            DriveMgr.IDAL.IOutStoreHouseDAL outStoreHouse = GetInstance("OutStoreHouseDAL") as DriveMgr.IDAL.IOutStoreHouseDAL;
            return outStoreHouse;
        }

        public static DriveMgr.IDAL.IEnterStoreHouseDetailDAL GetEnterStoreHouseDetailDAL()
        {
            DriveMgr.IDAL.IEnterStoreHouseDetailDAL enterStoreHouseDetail = GetInstance("EnterStoreHouseDetailDAL") as DriveMgr.IDAL.IEnterStoreHouseDetailDAL;
            return enterStoreHouseDetail;
        }

        public static DriveMgr.IDAL.IOutStoreHouseDetailsDAL GetOutStoreHouseDetailsDAL()
        {
            DriveMgr.IDAL.IOutStoreHouseDetailsDAL outStoreHouseDetails = GetInstance("OutStoreHouseDetailsDAL") as DriveMgr.IDAL.IOutStoreHouseDetailsDAL;
            return outStoreHouseDetails;
        }
    }
}
