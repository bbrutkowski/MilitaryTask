using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using MilitaryTask.Bindings;
using MilitaryTask.BussinesLogic.Interfaces;
using Ninject;

internal class Program
{
    private static async Task Main()
    {
        var kernel = new StandardKernel();
        kernel.Load(new Bindings());
        var orderCostsService = kernel.Get<IBillingService>();

        var programResult = await Run(orderCostsService);
        await Console.Out.WriteLineAsync(programResult.Value);
        Console.ReadKey();
    }

    private static async Task<Result<string>> Run(IBillingService billingService)
    {
        var getResult = await billingService.GetBillingListAsync();
        if (getResult.IsFailure) return Result.Failure<string>(getResult.Error);

        await Console.Out.WriteLineAsync("The list of billings has been successfully downloaded." +
           "Now it will be saved in the database");

        var savingBillingsResult = await billingService.SaveBillingsAsync(getResult.Value);
        if (savingBillingsResult.IsFailure) return Result.Failure<string>(savingBillingsResult.Error);

        return Result.Success("The program completed successfully");   
    }
}