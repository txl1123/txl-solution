using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TxlMvc.Helper;
using System.Web;
using TxlMvc.Controllers;


namespace GTA.CSMAR.Web.Webui.Extensions
{
    public class AuthorizeExAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //string err = "";

            bool isAuth = base.AuthorizeCore(httpContext)||
                (UserHelper.GetCurrentUserInfo() != null) ;
            if (isAuth)
                UserHelper.RefreshAuthTimeout();

            return isAuth;
        }
    }
}