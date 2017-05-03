using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.Model
{
    public class OutStoreHouseDetailsModel
    {
        private int _id;
        private string _outdetailssn;
        private int? _goodsid;
        private int? _outstorehouseid;
        private int? _outquantity;
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
        /// 出库单号
        /// </summary>
        public string OutDetailsSN
        {
            set { _outdetailssn = value; }
            get { return _outdetailssn; }
        }
        /// <summary>
        /// 物品编号
        /// </summary>
        public int? GoodsID
        {
            set { _goodsid = value; }
            get { return _goodsid; }
        }
        /// <summary>
        /// 出库编号
        /// </summary>
        public int? OutStoreHouseID
        {
            set { _outstorehouseid = value; }
            get { return _outstorehouseid; }
        }
        /// <summary>
        /// 出库数量
        /// </summary>
        public int? OutQuantity
        {
            set { _outquantity = value; }
            get { return _outquantity; }
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
