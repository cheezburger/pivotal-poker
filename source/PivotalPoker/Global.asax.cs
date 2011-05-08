using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using PivotalPoker.Models;

namespace PivotalPoker
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("projects.index", "projects/{projectId}", new { controller = "Projects", action = "Index" });
            routes.MapRoute("stories.detail", "projects/{projectId}/stories/{storyId}", new { controller = "Story", action = "Detail" });
            routes.MapRoute("stories.vote",   "projects/{projectId}/stories/{storyId}/vote", new { controller = "Story", action = "Vote" });
            routes.MapRoute("stories.votes",  "projects/{projectId}/stories/{storyId}/status", new { controller = "Story", action = "Status" });
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacWebTypesModule());
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsSelf().AsImplementedInterfaces();
            builder.Register(c => new Pivotal(c.Resolve<IConfig>().Get<string>("PivotalUserAPIKey"))).As<IPivotal>();
            builder.RegisterType<GameRepository>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}