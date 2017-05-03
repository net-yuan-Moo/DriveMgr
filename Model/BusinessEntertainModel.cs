/***********************
 @ author:zlong
 @ Date:2015-01-08
 @ Desc:业务招待管理实体
 * ********************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.Model
{
    public class BusinessEntertainModel
    {
        #region Model
        private int _id;
        private decimal? _entertainamount;
        private string _entertainobject;
        private string _transactor;
        private DateTime? _transactdate;
        private string _entertainuse;
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
        /// 招待费用
        /// </summary>
        public decimal? EntertainAmount
        {
            set { _entertainamount = value; }
            get { return _entertainamount; }
        }
        /// <summary>
        /// 招待对象
        /// </summary>
        public string EntertainObject
        {
            set { _entertainobject = value; }
            get { return _entertainobject; }
        }
        /// <summary>
        /// 经办人
        /// </summary>
        public string Transactor
        {
            set { _transactor = value; }
            get { return _transactor; }
        }
        /// <summary>
        /// 招待日期
        /// </summary>
        public DateTime? TransactDate
        {
            set { _transactdate = value; }
            get { return _transactdate; }
        }
        /// <summary>
        /// 用途
        /// </summary>
        public string EntertainUse
        {
            set { _entertainuse = value; }
            get { return _entertainuse; }
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
