using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using MilitaryTask.Bindings;
using MilitaryTask.BussinesLogic;
using MilitaryTask.BussinesLogic.Interfaces;
using Ninject;

internal class Program
{
    private static async Task Main()
    {
       

        var programResult = await Run();
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
        var orderService = kernel.Get<IOrderService>();

        var auth = await authService.GetAuthAsync();

        var orderIdResult = await orderService.GetOrderIdAsync();
        if (orderIdResult.IsFailure) return Result.Failure<string>(orderIdResult.Error); 

        var billingDetailsResult = await billingService.GetBillingDetailsByOrderIdAsync(orderIdResult.Value, auth.Value);


        //var getResult = await billingService.GetFilesDataAsync();
        //if (getResult.IsFailure) return Result.Failure<string>(getResult.Error);

        //await Console.Out.WriteLineAsync("The list of billings has been successfully downloaded." +
        //   " Now it will be saved in the database");

        //var deserializationResult = await billingService.DeserializeFilesToBillingEntryListAsync(getResult.Value);
        //if (deserializationResult.IsFailure) return Result.Failure<string>("An error occurred while deserializing files");


        //await Console.Out.WriteLineAsync("Saving billings to database");

        //var savingBillingsResult = await billingService.SaveBillingsAsync(deserializationResult.Value);
        //if (savingBillingsResult.IsFailure) return Result.Failure<string>(savingBillingsResult.Error);

        return Result.Success("The program completed successfully");   
    }
}