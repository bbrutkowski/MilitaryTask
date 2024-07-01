using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MilitaryTask.AutoMapper;
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

            var serviceProvider = services.BuildServiceProvider();

            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

            Bind<IHttpClientFactory>().ToConstant(httpClientFactory);

            Bind<IHttpService>().To<HttpService>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            IMapper mapper = mapperConfig.CreateMapper();

            Bind<IMapper>().ToConstant(mapper);

            Bind<IAuthService>().To<AuthService>();

            Bind<IBillingService>().To<BillingService>();
            Bind<IBillingRespository>().To<BillingRepository>();

            Bind<IOfferService>().To<OfferService>();
            Bind<IOfferRepository>().To<OfferRepository>();

            Bind<IBillTypeRepository>().To<BillTypeRepository>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            Bind<IConfiguration>().ToConstant(configuration);
            Bind<DataContextAlias>().ToSelf().WithConstructorArgument("configuration", configuration);
        }
    }
}
