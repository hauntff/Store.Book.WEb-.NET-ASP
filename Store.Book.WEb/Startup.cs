using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Owin;
using Owin;
using Store.Book.WEb.Services;
using Store.Book.WEb.Interfaces;
using Store.Book.WEb.Models;
using Store.Book.WEb.Repos;

using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(Store.Book.WEb.Startup))]
namespace Store.Book.WEb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<ApplicationDbContext>();
            builder.RegisterType<AuthorRepository>().As<IAuthorRepository>();
            builder.RegisterType<StoreService>().As<IStoreService>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();

            ConfigureAuth(app);
        }
    }
}
