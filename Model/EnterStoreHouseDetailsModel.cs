using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.Model
{
    public class EnterStoreHouseDetailsModel
    {
        private int _id;
        private string _enterdetailssn;
        private int? _goodsid;
        private int? _enterquantity;
        private int? _enterstorehouseid;
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
        /// 明细单号
        /// </summary>
        public string EnterDetailsSN
        {
            set { _enterdetailssn = value; }
            get { return _enterdetailssn; }
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
        /// 数量
        /// </summary>
        public int? EnterQuantity
        {
            set { _enterquantity = value; }
            get { return _enterquantity; }
        }
        /// <summary>
        /// 入库编号
        /// </summary>
        public int? EnterStoreHouseID
        {
            set { _enterstorehouseid = value; }
            get { return _enterstorehouseid; }
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
