﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TxlMvc.Models;
using TxlMvc.Helper;
using System.Web.Security;
namespace TxlMvc.Helper
{
    public class UserHelper
    {
        //当前用户信息
        private static string CURRENT_USER_INFO = "CurrentUserInfo";
        private UsersContext udb = new UsersContext();
        //登录
        public static bool Login(LoginModel userInfo,out string errorMsg)
        {
            errorMsg = "";
            LoginModel loginInfo = new LoginModel()
            {
                UserName = userInfo.UserName,
                Password = SecurityHelper.Encrypt(userInfo.Password)
            };
            //验证登录信息
            bool loginstate;
            try
            {
                return loginstate = UserProfileData.CheckLogin(loginInfo);
            }
            catch (Exception ex)
            {
                errorMsg ="用户登录出错"+ex.ToString();
                return false;
            }
        }
        //注册
        public static bool Register(UserProfile userInfo, out string errorMsg)
        {
            errorMsg = "";
            UserProfile regInfo = new UserProfile()
            {
                UserName = userInfo.UserName,
                Password = SecurityHelper.Encrypt(userInfo.Password),
                RoleId=1//权限角色
            };
            int registerState;
            UserProfileData userPorfile=new UserProfileData();
            var userList=userPorfile.select(0," username ","username='"+regInfo.UserName+"'",null);
            bool  isNewName = true ;
            if (userList.Count > 0)
            {
                isNewName = false;
                errorMsg = "该用户名已注册";
                return isNewName;
                
            }
            try
            {
                registerState = userPorfile.insert(regInfo);
                if (registerState > 0)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                errorMsg = "注册出错" + ex.ToString();
                return false;
            }
        }

        #region 保存当前用户信息
        ///// <summary>
        ///// 将用户信息写入缓存以及Cookies
        ///// </summary>
        ///// <param name="onlineUser">当前用户信息</param>
        //private static void SetCurrentUserInfo(OnlineUser onlineUser)
        //{
        //    HttpContext.Current.Session[CURRENT_USER_INFO] = onlineUser;
        //    HttpContext.Current.Session.Timeout = AUTH_TIMEOUT; // 注意此处是设置所有session的过期时间       

        //    DateTime expiration = DateTime.Now;s
        //    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
        //        1,
        //        onlineUser.Username,
        //        expiration,
        //        expiration.AddMinutes(AUTH_TIMEOUT),
        //        true,
        //        onlineUser.OnlineID.ToString());

        //    HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName].Value = FormsAuthentication.Encrypt(ticket);
        //    HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName].Expires = ticket.Expiration;
        //    HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName].HttpOnly = true;
        //}
        #endregion
    }
}