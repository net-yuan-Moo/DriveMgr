using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.Model
{
    public class PayModel
    {
        #region Model
        private long _id;
        private long? _studentsid;
        private decimal? _shoudpay;
        private decimal? _realpay;
        private decimal? _salepay;
        private string _remark;
        private int? _status;
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
        public decimal? ShoudPay
        {
            set { _shoudpay = value; }
            get { return _shoudpay; }
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
        public decimal? SalePay
        {
            set { _salepay = value; }
            get { return _salepay; }
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
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
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
