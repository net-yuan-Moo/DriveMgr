﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.Model
{
    public class EnterStoreHouseModel
    {
        private int _id;
        private string _entersn;
        private DateTime? _enterdate;
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
        /// 入库单号
        /// </summary>
        public string EnterSN
        {
            set { _entersn = value; }
            get { return _entersn; }
        }
        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime? EnterDate
        {
            set { _enterdate = value; }
            get { return _enterdate; }
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
