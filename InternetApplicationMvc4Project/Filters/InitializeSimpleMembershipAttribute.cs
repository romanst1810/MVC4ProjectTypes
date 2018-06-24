using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using WebMatrix.WebData;
using InternetApplicationMvc4Project.Models;

namespace InternetApplicationMvc4Project.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        // Метод будет запущен перед обращением к методу действия контроллера.
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Проверяем что ASP.NET Simple Membership инициализируется только при первом запуске приложения
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<UsersContext>(null);

                try
                {
                    using (var context = new UsersContext())
                    {
                        if (!context.Database.Exists())
                        {
                            // Создаем SimpleMembership базу данных без Entity Framework схемы миграции.
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }
                    // Инициализирует базу Mеmbership.
                    // Данный метод проверяет что база данных существует, 
                    // Первый параметр - строка подключения к базе
                    // Второй параметр - Имя таблицы где будет сохранятся информация о пользователе
                    // Третий параметр - Колонка в таблице UserProfile которая содержит ID пользователя
                    // Четвертый параметр - Колонка в таблице UserProfile, которая содержит имя пользователя.
                    // Пятый параметр - указывает нужно ли создавать таблицы Membership если их нет.
                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }
}
