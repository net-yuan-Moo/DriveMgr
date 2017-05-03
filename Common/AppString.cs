using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DriveMgr.Common
{
    public static class AppString
    {
        public static readonly string picPath = ConfigurationManager.AppSettings["picPath"];
    }
}
