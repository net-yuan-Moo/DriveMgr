using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.Model
{
    public class GoodsModel
    {
        private int _id;
        private string _goodsname;
        private int? _goodscategoryid;
        private int? _minquantity;
        private int? _maxquantity;
        private int? _realquantity;
        private string _specification;
        private string _handleperson;
        private string _remark;
        private string _createperson;
        private DateTime? _createdate;
        private string _updateperson;
        private DateTime? _updatedate;
        private bool _deletemark;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 物品名
        /// </summary>
        public string GoodsName
        {
            set { _goodsname = value; }
            get { return _goodsname; }
        }
        /// <summary>
        /// 物资编号
        /// </summary>
        public int? GoodsCategoryID
        {
            set { _goodscategoryid = value; }
            get { return _goodscategoryid; }
        }
        /// <summary>
        /// 最小数量
        /// </summary>
        public int? MinQuantity
        {
            set { _minquantity = value; }
            get { return _minquantity; }
        }
        /// <summary>
        /// 最大数量
        /// </summary>
        public int? MaxQuantity
        {
            set { _maxquantity = value; }
            get { return _maxquantity; }
        }
        /// <summary>
        /// 实际数量
        /// </summary>
        public int? RealQuantity
        {
            set { _realquantity = value; }
            get { return _realquantity; }
        }
        /// <summary>
        /// 规格
        /// </summary>
        public string Specification
        {
            set { _specification = value; }
            get { return _specification; }
        }
        /// <summary>
        /// 办理人
        /// </summary>
        public string HandlePerson
        {
            set { _handleperson = value; }
            get { return _handleperson; }
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
        /// 
        /// </summary>
        public string CreatePerson
        {
            set { _createperson = value; }
            get { return _createperson; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UpdatePerson
        {
            set { _updateperson = value; }
            get { return _updateperson; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateDate
        {
            set { _updatedate = value; }
            get { return _updatedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteMark
        {
            set { _deletemark = value; }
            get { return _deletemark; }
        }
    }
}
