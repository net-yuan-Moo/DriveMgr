using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.Model
{
    public class CoachModel
    {
        #region Model
        private int _id;
        private string _coachname;
        private string _cardnum;
        private int? _age = 18;
        private bool _sex = true;
        private string _phone;
        private string _address;
        private int? _coachStatus = 1;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CoachName
        {
            set { _coachname = value; }
            get { return _coachname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CardNum
        {
            set { _cardnum = value; }
            get { return _cardnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Age
        {
            set { _age = value; }
            get { return _age; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CoachStatus
        {
            set { _coachStatus = value; }
            get { return _coachStatus; }
        }
        #endregion Model
    }
}
