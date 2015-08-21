using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace TxlMvc.Helper
{
    public class SecurityHelper
    {
        /// <summary>
        /// 简单MD5加密
        /// </summary>
        /// <param name="encrytionString"></param>
        /// <returns></returns>
        public static string Encrypt(string encrytionString)
        {
            var bytes = Encoding.ASCII.GetBytes(encrytionString);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(bytes);
            return System.Text.Encoding.Default.GetString(result);
            //FormsAuthentication.HashPasswordForStoringInConfigFile(encrytionString,"MD5");
            //return Convert.ToBase64String(bytes);
            
        }
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="strText">加密字符</param>
        /// <param name="strEncrKey">加密密钥</param>
        /// <returns></returns>
        public static String Encrypt(String strText, String strEncrKey)
        {
            Byte[] byKey = { };
            Byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="sDecrKey"></param>
        /// <returns></returns>

        public static String Decrypt(String strText, String sDecrKey)
        {
            Byte[] byKey = { };
            Byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            Byte[] inputByteArray = new byte[strText.Length];
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}