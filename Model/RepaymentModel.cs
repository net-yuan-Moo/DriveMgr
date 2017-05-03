/***********************
 @ author:zlong
 @ Date:2015-01-08
 @ Desc:还款管理实体
 * ********************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.Model
{
    public class RepaymentModel
    {
        #region Model
        private int _id;
        private decimal? _repaymentamount;
        private string _bank;
        private string _repaymentperson;
        private DateTime? _repaymentdate;
        private string _remark;
        private string _createperson;
        private DateTime? _createdate;
        private string _updateperson;
        private DateTime? _updatedate;
        private bool _deletemark;
        /// <summary>
        /// 编号
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 还款金额
        /// </summary>
        public decimal? RepaymentAmount
        {
            set { _repaymentamount = value; }
            get { return _repaymentamount; }
        }
        /// <summary>
        /// 银行
        /// </summary>
        public string Bank
        {
            set { _bank = value; }
            get { return _bank; }
        }
        /// <summary>
        /// 还款人
        /// </summary>
        public string RepaymentPerson
        {
            set { _repaymentperson = value; }
            get { return _repaymentperson; }
        }
        /// <summary>
        /// 还款日期
        /// </summary>
        public DateTime? RepaymentDate
        {
            set { _repaymentdate = value; }
            get { return _repaymentdate; }
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
        /// 创建日期
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
        /// 更新日期
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
        #endregion Model
    }
}
