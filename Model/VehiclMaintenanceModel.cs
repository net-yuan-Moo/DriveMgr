/***********************
 @ author:zlong
 @ Date:2015-01-08
 @ Desc:车辆维护情况记录实体
 * ********************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.Model
{
    public class VehiclMaintenanceModel
    {
        private int _id;
        private int? _vehicleid;
        private int? _maintenancetype;
        private decimal? _maintencosts;
        private string _maintenperson;
        private DateTime? _maintendate;
        private string _remark;
        private string _createperson;
        private DateTime? _createdate;
        private string _updateperson;
        private DateTime? _updatedate;
        private bool _deletemark = false;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? VehicleId
        {
            set { _vehicleid = value; }
            get { return _vehicleid; }
        }
        /// <summary>
        /// 维护类别
        /// </summary>
        public int? MaintenanceType
        {
            set { _maintenancetype = value; }
            get { return _maintenancetype; }
        }
        /// <summary>
        /// 费用
        /// </summary>
        public decimal? MaintenCosts
        {
            set { _maintencosts = value; }
            get { return _maintencosts; }
        }
        /// <summary>
        /// 维护人
        /// </summary>
        public string MaintenPerson
        {
            set { _maintenperson = value; }
            get { return _maintenperson; }
        }
        /// <summary>
        /// 维护时间
        /// </summary>
        public DateTime? MaintenDate
        {
            set { _maintendate = value; }
            get { return _maintendate; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatePerson
        {
            set { _createperson = value; }
            get { return _createperson; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdatePerson
        {
            set { _updateperson = value; }
            get { return _updateperson; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate
        {
            set { _updatedate = value; }
            get { return _updatedate; }
        }
        /// <summary>
        /// 删除标志
        /// </summary>
        public bool DeleteMark
        {
            set { _deletemark = value; }
            get { return _deletemark; }
        }
    }
}
