using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.Model
{
    public class PayExamModel
    {
        #region Model
        private long _id;
        private long? _studentsid;
        private decimal? _realpay;
        private string _remark;
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
        public long? StudentsID
        {
            set { _studentsid = value; }
            get { return _studentsid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? RealPay
        {
            set { _realpay = value; }
            get { return _realpay; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
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
