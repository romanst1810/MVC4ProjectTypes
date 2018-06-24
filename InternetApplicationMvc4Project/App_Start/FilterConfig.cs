using System.Web;
using System.Web.Mvc;

namespace InternetApplicationMvc4Project
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}