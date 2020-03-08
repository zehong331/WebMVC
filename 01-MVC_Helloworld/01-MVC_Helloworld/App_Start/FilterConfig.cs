using System.Web;
using System.Web.Mvc;

namespace _01_MVC_Helloworld
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
