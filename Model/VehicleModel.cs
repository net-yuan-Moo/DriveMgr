/***********************
 @ author:zlong
 @ Date:2015-01-08
 @ Desc:车辆管理实体
 * ********************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.Model
{
    public class VehicleModel
    {
        #region Model
        private int _id;
        private string _brands;
        private string _licencePlateNum;
        private string _carModel;
        private decimal? _buyPrice;
        private DateTime? _buydate;
        private string _owner;
        private string _remark;
        private int? _status;
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
        /// 车牌号
        /// </summary>
        public string LicencePlateNum
        {
            set { _licencePlateNum = value; }
            get { return _licencePlateNum; }
        }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brands
        {
            set { _brands = value; }
            get { return _brands; }
        }        
        /// <summary>
        /// 型号
        /// </summary>
        public string CarModel
        {
            set { _carModel = value; }
            get { return _carModel; }
        }
        /// <summary>
        /// 购买价钱
        /// </summary>
        public decimal? BuyPrice
        {
            set { _buyPrice = value; }
            get { return _buyPrice; }
        }
        /// <summary>
        /// 购买日期
        /// </summary>
        public DateTime? BuyDate
        {
            set { _buydate = value; }
            get { return _buydate; }
        }
        /// <summary>
        /// 所有者
        /// </summary>
        public string Owner
        {
            set { _owner = value; }
            get { return _owner; }
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
        /// 状态
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
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
