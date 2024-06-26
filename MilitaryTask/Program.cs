﻿using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using MilitaryTask.Bindings;
using MilitaryTask.BussinesLogic.Interfaces;
using Ninject;

internal class Program
{
    private static async Task Main()
    {
        var programResult = await Run();
        if (programResult.IsFailure)
        {
            await Console.Out.WriteLineAsync(programResult.Error);
            Console.ReadKey();
            return;
        }

        await Console.Out.WriteLineAsync(programResult.Value);
        Console.ReadKey();
    }

    private static IConfiguration AppConfig()
    {
        return new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
    }

    private static async Task<Result<string>> Run()
    {
        var configuration = AppConfig();
        var kernel = new StandardKernel();

        kernel.Bind<IConfiguration>().ToConstant(configuration);
        kernel.Load(new Bindings());

        var billingService = kernel.Get<IBillingService>();
        var authService = kernel.Get<IAuthService>();
        var orderService = kernel.Get<IOfferService>();

        var authResult = await authService.GetAuthAsync();
        if (authResult.IsFailure) return Result.Failure<string>("Authorization not granted");

        await Console.Out.WriteLineAsync("Authorization granted");

        var orderIdResult = await orderService.GetOfferIdAsync(); 
        if (orderIdResult.IsFailure) return Result.Failure<string>(orderIdResult.Error); 

        var billListResult = await billingService.GetBillsByOfferIdAsync(orderIdResult.Value, authResult.Value);
        if (billListResult.IsFailure) Result.Failure<string>(billListResult.Error);

        await Console.Out.WriteLineAsync("Billings details successfully downloaded. Now it will be saved in the database");

        var savingResult = await billingService.SaveBillsAsync(billListResult.Value);
        if (savingResult.IsFailure) return Result.Failure<string>(savingResult.Error);

        await Console.Out.WriteLineAsync("Data has been successfully saved to the database");

        return Result.Success("Program completed successfully");   
    }
}