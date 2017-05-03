/***********************
 @ author:zlong
 @ Date:2015-01-08
 @ Desc:办公费用管理实体
 * ********************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.Model
{
    public class OfficeModel
    {
        #region Model
        private int _id;
        private decimal? _officeamount;
        private string _tagperson;
        private string _officeuse;
        private DateTime? _useDate;
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
        /// 办公费用
        /// </summary>
        public decimal? OfficeAmount
        {
            set { _officeamount = value; }
            get { return _officeamount; }
        }
        /// <summary>
        /// 责任人
        /// </summary>
        public string TagPerson
        {
            set { _tagperson = value; }
            get { return _tagperson; }
        }
        /// <summary>
        /// 用途
        /// </summary>
        public string OfficeUse
        {
            set { _officeuse = value; }
            get { return _officeuse; }
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
        /// 更新日期
        /// </summary>
        public DateTime? UseDate
        {
            set { _useDate = value; }
            get { return _useDate; }
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
