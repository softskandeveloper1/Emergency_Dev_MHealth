using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MHealth
{
    public static class HMTLHelperExtensions
    {
        public static string IsSelected(this IHtmlHelper html, string controller = null, string action = null, string cssClass = null, string start_text = null)
        {
            try
            {
                if (String.IsNullOrEmpty(cssClass))
                    cssClass = "active";

                string currentAction = (string)html.ViewContext.RouteData.Values["action"]?.ToString()?.ToLower();
                string currentController = (string)html.ViewContext.RouteData.Values["controller"]?.ToString()?.ToLower();

                if (String.IsNullOrEmpty(controller))
                    controller = currentController;

                if (String.IsNullOrEmpty(action))
                    action = currentAction;

                return controller == currentController && action == currentAction ?
                    cssClass : String.Empty;
            }
            catch
            {
                return String.Empty;
            }
          
        }

        public static string IsSelectedFirst(this IHtmlHelper html, string[] controllers = null, string cssClass = null)
        {

            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            //string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"].ToString().ToLower();

            return controllers.Contains(currentController) ?
                cssClass : String.Empty;
        }




        public static string PageClass(this IHtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

    }
}
