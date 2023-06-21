using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RhyoliteERP.Web.Helpers
{
    public static class RazorHtmlCustomHelper
    {

        public static HtmlString IsActive(this IHtmlHelper htmlHelper, ViewContext context, string[] controller, params string[] actions)
        {
            string currentController = context.RouteData.Values["controller"].ToString();
            string currentAction = context.RouteData.Values["action"].ToString();

            if (controller.Contains(currentController) && actions.Contains(currentAction))
            {
                return new HtmlString("active");
            }
            return new HtmlString("");
        }

        public static HtmlString IsActive(this IHtmlHelper htmlHelper, ViewContext context, string[] controller)
        {
            string currentController = context.RouteData.Values["controller"].ToString();


            if (controller.Contains(currentController))
            {
                return new HtmlString("active");
            }
            return new HtmlString("");
        }
    }
}
