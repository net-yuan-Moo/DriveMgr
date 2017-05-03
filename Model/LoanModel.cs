/***********************
 @ author:zlong
 @ Date:2015-01-08
 @ Desc:贷款管理实体
 * ********************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.Model
{
    public class LoanModel
    {
        #region Model
        private int _id;
        private decimal? _loanamount;
        private string _bank;
        private string _lenders;
        private DateTime? _loandate;
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
        /// 贷款金额
        /// </summary>
        public decimal? LoanAmount
        {
            set { _loanamount = value; }
            get { return _loanamount; }
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
        /// 贷款人
        /// </summary>
        public string Lenders
        {
            set { _lenders = value; }
            get { return _lenders; }
        }
        /// <summary>
        /// 贷款日期
        /// </summary>
        public DateTime? LoanDate
        {
            set { _loandate = value; }
            get { return _loandate; }
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
