using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BasicMvc4Project
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        // Обработчик события старта приложения.
        protected void Application_Start()
        {
            // Регистрация областей. Поиск объектов типа производного от AreaRegistration и вызов методов RegisterArea для регистрации маршрутов каждой области.
            AreaRegistration.RegisterAllAreas();

            // Регистрации маршрутов для работы WebAPI.
            WebApiConfig.Register(GlobalConfiguration.Configuration);

            // Регистрация глобальных фильтров
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // Регистрация маршрутов.
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Регистрация bundle для оптимизации загрузки CSS и JavaScript файлов.
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}