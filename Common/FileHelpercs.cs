using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DriveMgr.Common
{
    public static class FileHelpercs
    {
        /// <summary>
        /// 修改文件名称
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="desPath"></param>
        /// <returns></returns>
        public static bool ChangeFileName(string srcPath,string desPath)
        {
            try
            {
                if (File.Exists(srcPath))
                {
                    File.Move(srcPath, desPath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
