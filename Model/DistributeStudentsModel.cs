using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.Model
{
    public class DistributeStudentsModel
    {
        #region Model
        private long _id;
        private int? _coachid;
        private long? _studentsid;
        private DateTime? _createtime;
        private string _operater;
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
        public int? CoachID
        {
            set { _coachid = value; }
            get { return _coachid; }
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
        #endregion Model
    }
}
