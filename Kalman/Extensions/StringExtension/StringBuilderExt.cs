using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Kalman.Extensions
{
    public static class ExtensionsMethod
    {
        #region StringBuilder
        /// <summary>
        /// 移除头部和末部空格
        /// </summary>
        /// <param name="sb"></param>
        /// <returns></returns>
        public static StringBuilder Trim(this StringBuilder sb)
        {
            string strSb = sb.ToString();
            strSb = strSb.Trim();
            StringBuilder newSb = new StringBuilder(strSb);
            return newSb;
        }

        /// <summary>
        /// 移除头部指定字符
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="trimChars"></param>
        /// <returns></returns>
        public static StringBuilder TrimStart(this StringBuilder sb, params char[] trimChars)
        {
            string strSb = sb.ToString();

            strSb = strSb.TrimStart(trimChars);

            StringBuilder newSb = new StringBuilder(strSb);
            return newSb;
        }

        /// <summary>
        /// 移除头部字符串
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="trimString"></param>
        /// <returns></returns>
        public static StringBuilder TrimStart(this StringBuilder sb, string trimString)
        {
            string strSb = sb.ToString();

            if (strSb.StartsWith(trimString) == true)
            {
                strSb = strSb.Substring(trimString.Length, strSb.Length - trimString.Length);
            }

            StringBuilder newSb = new StringBuilder(strSb);
            return newSb;
        }

        /// <summary>
        /// 移除末尾指定字符
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="trimChars"></param>
        /// <returns></returns>
        public static StringBuilder TrimEnd(this StringBuilder sb, params char[] trimChars)
        {
            string strSb = sb.ToString();

            strSb = strSb.TrimEnd(trimChars);

            StringBuilder newSb = new StringBuilder(strSb);
            return newSb;
        }

        /// <summary>
        /// 移除末尾的字符串
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="trimString"></param>
        /// <returns></returns>
        public static StringBuilder TrimEnd(this StringBuilder sb, string trimString)
        {
            string strSb = sb.ToString();

            if (strSb.EndsWith(trimString) == true)
            {
                strSb = strSb.Substring(0, strSb.Length - trimString.Length);
            }

            StringBuilder newSb = new StringBuilder(strSb);
            return newSb;
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="startIndex"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static StringBuilder SubString(this StringBuilder sb, int startIndex, int Length)
        {
            string strSb = sb.ToString();

            if (strSb.Length > Length)
            {
                strSb = strSb.Substring(startIndex, Length);
            }

            StringBuilder newSb = new StringBuilder(strSb);
            return newSb;
        }

        /// <summary>
        /// 截取字符串的末尾部分
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static StringBuilder SubStringEnd(this StringBuilder sb, int Length)
        {
            string strSb = sb.ToString();

            if (strSb.Length > Length)
            {
                strSb = strSb.Substring(strSb.Length - Length, Length);
            }

            StringBuilder newSb = new StringBuilder(strSb);
            return newSb;
        }
        #endregion
    }
}
