using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DriveMgr.Common
{
    public static class NewtonJsonHelper
    {
        /// <summary>
        /// ToJson的几种重载
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static string ToJson(this object obj, Formatting formatting)
        {
            return JsonConvert.SerializeObject(obj, formatting);
        }

        public static string ToJson(this object obj, params JsonConverter[] converters)
        {
            return JsonConvert.SerializeObject(obj, converters);
        }

        public static string ToJson(this object obj, Formatting formatting, params JsonConverter[] converts)
        {
            return JsonConvert.SerializeObject(obj, formatting, converts);
        }

        public static string ToJson(this object obj, Formatting formatting, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(obj, formatting, settings);
        }

        /// <summary>
        /// 返回失败信息
        /// </summary>
        /// <param name="strCode"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static string FailReturn(string strCode, string strMsg)
        {
            if (!strCode.Equals("0x9999"))
            {
                strMsg = "";
            }
            string strResult = "{\"code\":\"" + strCode + "\",\"result\":\"fail\",\"info\":\"" + strMsg + "\"}";
            return strResult;
        }

        public static string FailReturn(string strMsg)
        {
            string strCode = "0x9999";
            return FailReturn(strCode, strMsg);
        }

        /// <summary>
        /// 返回成功信息
        /// </summary>
        /// <param name="strInfo"></param>
        /// <returns></returns>
        public static string SuccessReturn(string strInfo)
        {
            string strResult = "{\"result\":\"success\",\"info\":" + strInfo + "}";
            return strResult;
        }

        public static string EasyUIReturn(int total, string strInfo)
        {
            string strResult = "{\"total\":\"" + total + "\",\"rows\":" + strInfo + "}";
            return strResult;
        }

        /// <summary>
        /// 返回Table信息
        /// </summary>
        /// <param name="datarows"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this DataRow[] datarows)
        {
            DataTable newdt = new DataTable();
            newdt = datarows[0].Table.Clone();
            foreach (DataRow dr in datarows)
            {
                newdt.Rows.Add(dr.ItemArray);
            }
            return newdt;
        }
    }
}
