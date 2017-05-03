using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.Model
{
    public class DistributionVehicleModel
    {
        #region Model
        private long _id;
        private long? _studentsid;
        private int? _subjectid;
        private int? _vehicleid;
        private DateTime? _createtime;
        private string _operater;
        private int? _distributevihiclestatus;
        /// <summary>
        /// 
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? StudentsID
        {
            set { _studentsid = value; }
            get { return _studentsid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SubjectID
        {
            set { _subjectid = value; }
            get { return _subjectid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? VehicleID
        {
            set { _vehicleid = value; }
            get { return _vehicleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Operater
        {
            set { _operater = value; }
            get { return _operater; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DistributeVihicleStatus
        {
            set { _distributevihiclestatus = value; }
            get { return _distributevihiclestatus; }
        }
        #endregion Model
    }
}
