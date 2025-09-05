using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace sarah10797WebApp
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            ScriptManager.ScriptResourceMapping.AddDefinition(
       "jquery",
       new ScriptResourceDefinition
       {
           Path = "~/Scripts/jquery-3.6.0.min.js",           // مسار الملف المحلي
            DebugPath = "~/Scripts/jquery-3.6.0.js",          // مسار النسخة غير المضغوطة
            CdnPath = "https://code.jquery.com/jquery-3.6.0.min.js",   // مسار CDN
            CdnDebugPath = "https://code.jquery.com/jquery-3.6.0.js",
           CdnSupportsSecureConnection = true,
           LoadSuccessExpression = "window.jQuery"
       });
        }
    }
}