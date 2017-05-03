using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.Model
{
    public class RegistrationModel
    {
        #region Model
		private long _id;
		private string _studentsname;
		private string _studentcode;
		private bool _sex;
		private int? _age;
		private string _phonenum;
		private bool _islocal;
		private int? _periodsid;
		private string _cardnum;
		private string _address;
		private string _picpath;
		private string _remark;
		private int? _status;
		private string _operater;
		private int? _flag=1;
		/// <summary>
		/// 
		/// </summary>
		public long ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StudentsName
		{
			set{ _studentsname=value;}
			get{return _studentsname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StudentCode
		{
			set{ _studentcode=value;}
			get{return _studentcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Age
		{
			set{ _age=value;}
			get{return _age;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PhoneNum
		{
			set{ _phonenum=value;}
			get{return _phonenum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsLocal
		{
			set{ _islocal=value;}
			get{return _islocal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PeriodsID
		{
			set{ _periodsid=value;}
			get{return _periodsid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CardNum
		{
			set{ _cardnum=value;}
			get{return _cardnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 照片路径
		/// </summary>
		public string PicPath
		{
			set{ _picpath=value;}
			get{return _picpath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Operater
		{
			set{ _operater=value;}
			get{return _operater;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		#endregion Model
    }
}
