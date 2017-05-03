using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Script.Serialization;

namespace DriveMgr.Common
{
    /// <summary>
    /// 用户帮助类：获取Cookie里的用户对象
    /// </summary>
    public class UserHelper
    {
        /// <summary>
        /// 获取保存在cookie里的用户对象
        /// </summary>
        public static DriveMgr.Model.User GetUser(HttpContext context)
        {
            try
            {
                if (context.Request.IsAuthenticated)
                {
                    FormsIdentity id = (FormsIdentity)context.User.Identity;
                    FormsAuthenticationTicket tickets = id.Ticket;

                    //反序列化获取票证里序列化的用户对象
                    DriveMgr.Model.User userFromCookie = new JavaScriptSerializer().Deserialize<DriveMgr.Model.User>(tickets.UserData);
                    return userFromCookie;
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

    }
}
