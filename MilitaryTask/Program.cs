using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MilitaryTask.BussinesLogic;
using MilitaryTask.BussinesLogic.Interfaces;
using MilitaryTask.Repository.Interfaces;
using MilitaryTask.Repository;
using Microsoft.EntityFrameworkCore;
using MilitaryTask.AutoMapper;
using MilitaryTask.DataContext;

internal class Program
{
    private static async Task Main()
    {
        var serviceCollection = GetServiceCollection();

        var programResult = await Run(serviceCollection);
        if (programResult.IsFailure)
        {
            await Console.Out.WriteLineAsync(programResult.Error);
            Console.ReadKey();
            return;
        }

        await Console.Out.WriteLineAsync(programResult.Value);
        Console.ReadKey();
    }

    private static ServiceCollection GetServiceCollection()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var serviceCollection = new ServiceCollection();

        serviceCollection
            .AddSingleton<IConfiguration>(configuration)
            .AddDbContext<MyDataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
            .AddHttpClient()
            .AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            })
            .AddScoped<IHttpService, HttpService>()
            .AddScoped<IBillService, BillService>()
            .AddScoped<IBillRespository, BillRepository>()
            .AddScoped<IOrderService, OrderService>()
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<IOfferRepository, OfferRepository>()
            .AddScoped<IBillTypeRepository, BillTypeRepository>()
            .AddScoped<IAuthService, AuthService>();

        return serviceCollection;
    }

    private static async Task<Result<string>> Run(ServiceCollection serviceCollection)
    {
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
        var scope = scopeFactory.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var billService = scopedProvider.GetRequiredService<IBillService>();
        var authService = scopedProvider.GetRequiredService<IAuthService>();
        var orderService = scopedProvider.GetRequiredService<IOrderService>();

        var authResult = await authService.GetAuthAsync();
        if (authResult.IsFailure) return Result.Failure<string>("Authorization not granted");

        await Console.Out.WriteLineAsync("Authorization granted");

        var offerIdsResult = await orderService.GetOrderIdsAsync();
        if (offerIdsResult.IsFailure) return Result.Failure<string>(offerIdsResult.Error);

        var billListResult = await billService.GetBillsByOfferIdAsync(offerIdsResult.Value, authResult.Value);
        if (billListResult.IsFailure) Result.Failure<string>(billListResult.Error);

        await Console.Out.WriteLineAsync("Billings details successfully downloaded. Now it will be saved in the database");

        var savingResult = await billService.SaveBillsAsync(billListResult.Value);
        if (savingResult.IsFailure) return Result.Failure<string>(savingResult.Error);

        await Console.Out.WriteLineAsync("Data has been successfully saved to the database");
        return Result.Success("Program completed successfully");
    }
}