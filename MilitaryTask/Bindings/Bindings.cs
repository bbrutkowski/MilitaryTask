using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MilitaryTask.BussinesLogic;
using MilitaryTask.BussinesLogic.Interfaces;
using MilitaryTask.Repository;
using MilitaryTask.Repository.Interfaces;
using Ninject.Modules;
using DataContextAlias = MilitaryTask.DataContext.DataContext;

namespace MilitaryTask.Bindings
{
    internal class Bindings : NinjectModule
    {
        public override void Load()
        {
            var services = new ServiceCollection();
            services.AddHttpClient();

            services.AddTransient<IAuthService, AuthService>();

            var serviceProvider = services.BuildServiceProvider();
            Bind<IServiceProvider>().ToConstant(serviceProvider);

            Bind<IHttpClientFactory>().ToMethod(ctx => serviceProvider.GetRequiredService<IHttpClientFactory>());
            Bind<IAuthService>().ToMethod(ctx => serviceProvider.GetRequiredService<IAuthService>());

            Bind<IBillingService>().To<BillingService>(); 
            Bind<IOrderCostsRespository>().To<OrderCostsRepository>(); 
        }
    }
}
