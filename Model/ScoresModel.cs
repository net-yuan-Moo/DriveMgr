using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.Model
{
    public class ScoresModel
    {
        #region Model
        private long _id;
        private long? _studentsid;
        private decimal? _scoreone;
        private int? _onestatus;
        private decimal? _scoretwo;
        private int? _twostatus;
        private decimal? _socrethree;
        private int? _threestatus;
        private decimal? _scorefour;
        private int? _fourstatus;
        private string _remark;
        /// <summary>
        /// 
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? StudentsID
        {
            set { _studentsid = value; }
            get { return _studentsid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ScoreOne
        {
            set { _scoreone = value; }
            get { return _scoreone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OneStatus
        {
            set { _onestatus = value; }
            get { return _onestatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ScoreTwo
        {
            set { _scoretwo = value; }
            get { return _scoretwo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TwoStatus
        {
            set { _twostatus = value; }
            get { return _twostatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SocreThree
        {
            set { _socrethree = value; }
            get { return _socrethree; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ThreeStatus
        {
            set { _threestatus = value; }
            get { return _threestatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ScoreFour
        {
            set { _scorefour = value; }
            get { return _scorefour; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FourStatus
        {
            set { _fourstatus = value; }
            get { return _fourstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

    }
}
