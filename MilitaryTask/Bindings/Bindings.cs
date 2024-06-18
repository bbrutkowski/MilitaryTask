using Microsoft.Extensions.DependencyInjection;
using MilitaryTask.BussinesLogic;
using MilitaryTask.BussinesLogic.Interfaces;
using Ninject.Modules;

namespace MilitaryTask.Bindings
{
    internal class Bindings : NinjectModule
    {
        public override void Load()
        {
            var services = new ServiceCollection();
            services.AddHttpClient();

            services.AddTransient<IFileService, FileService>();

            var serviceProvider = services.BuildServiceProvider();
            Bind<IServiceProvider>().ToConstant(serviceProvider);

            Bind<IHttpClientFactory>().ToMethod(ctx => serviceProvider.GetRequiredService<IHttpClientFactory>());
            Bind<IFileService>().ToMethod(ctx => serviceProvider.GetRequiredService<IFileService>());

            Bind<IOrderCostsService>().To<OrderCostsService>(); 
        }
    }
}
