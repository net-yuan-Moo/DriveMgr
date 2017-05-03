using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.Model
{
    public class RegistrationByCardModel
    {
        public string StudentsName{get;set;}
        public string Sex{get;set;}

        public string Nation{get;set;}
        public string Born{get;set;}
        public string Address{get;set;}
        public string IDCardNo{get;set;}
        public string GrantDept{get;set;}
        public string UserLifeBegin{get;set;}
        public string UserLifeEnd{get;set;}
        public string PhotoFileName{get;set;}
        public string FingerPrint { get; set; }
        public int Age { get; set; }
    }
}
