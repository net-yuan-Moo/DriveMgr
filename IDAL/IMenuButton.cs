using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DriveMgr.IDAL
{
    public interface IMenuButton
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(int menuId, string[] buttonlist);
    }
}
