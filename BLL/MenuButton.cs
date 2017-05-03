using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.BLL
{
    public class MenuButton
    {
        private static readonly DriveMgr.IDAL.IMenuButton dal = DriveMgr.DALFactory.Factory.GetMenuButtonDAL();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(int menuId, string menuButtonId)
        {
            string[] buttonlist = menuButtonId.Split(',');
            return dal.Add(menuId, buttonlist);
        }
    }
}
