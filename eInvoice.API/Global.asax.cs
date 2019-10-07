using eInvoice.Model;
using eInvoice.MultiLanguages;
using eInvoice.Repository.DataContext;
using eInvoice.Repository.Interface;
using eInvoice.Services.Interface;
using eInvoice.Services.Service;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace eInvoice.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Cầu hình cho log4net
            log4net.Config.XmlConfigurator.Configure();

            //Cấu hình Automapper
            AutoMapperConfiguration.Configure();

            //Cấu hình đa ngôn ngữ
            String culture = ConfigurationManager.AppSettings["culture"];
            String ForderCulture = ConfigurationManager.AppSettings["ForderCulture"];
            ConfigMultiLanguage.Configure(culture, ForderCulture);

            //Cấu hình dependence injection
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            //Lifestyle: 
            //Singleton trong suốt vòng đời ứng dụng chỉ có 1 instance duy nhất
            //Scoped: Chỉ tồn tại 1 instance trong 1 lần request (mỗi request là 1 scope).
            //Transient: Một instance mới luôn được tạo, mỗi khi được yêu cầu.
            container.Register<IInvoiceCategorys, InvoiceCategorysService>(Lifestyle.Scoped );
            container.Register<IAuthentication, AuthenticationService>(Lifestyle.Scoped);
            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
