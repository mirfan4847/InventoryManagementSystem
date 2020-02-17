using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperStore.Common
{
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                url = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            // check  sessions here
            if (HttpContext.Current.Session[SessionVariables.Username] == null)
            {
                if (filterContext.Controller.TempData.ContainsKey("Key"))
                {
                    filterContext.Controller.TempData["Key"] = url;
                }
                else
                {
                    filterContext.Controller.TempData.Add("Key", url);
                }
                filterContext.Result = new RedirectResult("~/Users/Login?redirect=" + url);
                return;
            }
            //// Check if user can access URL
            //var htable = (System.Collections.Hashtable)HttpContext.Current.Session[SessionVariables.hTable];
            //var parent = (System.Collections.Hashtable)HttpContext.Current.Session[SessionVariables.parent];
            //var child = (System.Collections.Hashtable)HttpContext.Current.Session[SessionVariables.child];
            //var Parentmenu = (System.Collections.Hashtable)HttpContext.Current.Session[SessionVariables.parentmenu];
            
            base.OnActionExecuting(filterContext);
        }
    }
}