using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MilitaryTask.BussinesLogic;
using MilitaryTask.BussinesLogic.Interfaces;
using MilitaryTask.Repository;
using MilitaryTask.Repository.Interfaces;
using Ninject.Modules;
using System.Net.Http;
using static CSharpFunctionalExtensions.Result;
using DataContextAlias = MilitaryTask.DataContext.DataContext;

namespace MilitaryTask.Bindings
{
    internal class Bindings : NinjectModule
    {
        public override void Load()
        {
            var services = new ServiceCollection();
            services.AddHttpClient();

            var serviceProvider = services.BuildServiceProvider();

            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

            Bind<IHttpClientFactory>().ToConstant(httpClientFactory);

            Bind<IHttpService>().To<HttpService>();

            Bind<IAuthService>().To<AuthService>();

            Bind<IBillingService>().To<BillingService>();
            Bind<IBillingRespository>().To<BillingRepository>();

            Bind<IOfferService>().To<OfferService>();
            Bind<IOfferRepository>().To<OfferRepository>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            Bind<IConfiguration>().ToConstant(configuration);
            Bind<DataContextAlias>().ToSelf().WithConstructorArgument("configuration", configuration);
        }
    }
}
