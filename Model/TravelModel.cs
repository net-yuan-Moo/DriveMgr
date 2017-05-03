/***********************
 @ author:zlong
 @ Date:2015-01-08
 @ Desc:差旅管理实体
 * ********************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.Model
{
    public class TravelModel
    {
        #region Model
        private int _id;
        private decimal? _travelamount;
        private string _travelperson;
        private string _traveluse;
        private string _remark;
        private string _createperson;
        private DateTime? _traveDate;
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
        /// 金额
        /// </summary>
        public decimal? TravelAmount
        {
            set { _travelamount = value; }
            get { return _travelamount; }
        }
        /// <summary>
        /// 出差人
        /// </summary>
        public string TravelPerson
        {
            set { _travelperson = value; }
            get { return _travelperson; }
        }
        /// <summary>
        /// 用途
        /// </summary>
        public string TravelUse
        {
            set { _traveluse = value; }
            get { return _traveluse; }
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
        public DateTime? TraveDate
        {
            set { _traveDate = value; }
            get { return _traveDate; }
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
