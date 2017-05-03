using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.Model
{
    public class AppointmentModel
    {
        #region Model
        private long _id;
        private long? _studentsid;
        private int? _subjectid;
        private DateTime? _appointdate;
        private int? _appointstatus;
        private string _operater;
        private string _remark;
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
        public DateTime? AppointDate
        {
            set { _appointdate = value; }
            get { return _appointdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AppointStatus
        {
            set { _appointstatus = value; }
            get { return _appointstatus; }
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
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model
    }
}
