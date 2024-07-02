using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MilitaryTask.AutoMapper;
using MilitaryTask.BussinesLogic;
using MilitaryTask.BussinesLogic.Interfaces;
using MilitaryTask.Model.Auth;
using MilitaryTask.Repository;
using MilitaryTask.Repository.Interfaces;
using Ninject;
using Ninject.Modules;
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

            var configuration = new ConfigurationBuilder()
                 .SetBasePath(AppContext.BaseDirectory)
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .Build();

            services.Configure<OAuthSettings>(configuration.GetSection("OAuthSettings"));

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

            Bind<IBillService>().To<BillService>();
            Bind<IBillRespository>().To<BillRepository>();

            Bind<IOfferService>().To<OfferService>();
            Bind<IOfferRepository>().To<OfferRepository>();

            Bind<IBillTypeRepository>().To<BillTypeRepository>();

            Bind<IConfiguration>().ToConstant(configuration);
            Bind<DataContextAlias>().ToSelf().WithConstructorArgument("configuration", configuration);
        }
    }
}
