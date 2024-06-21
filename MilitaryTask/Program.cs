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

        await Run(orderCostsService);
    }

    private static async Task Run(IBillingService billingService)
    {
        var getResult = await billingService.GetBillingListAsync();
        if (getResult.IsSuccess) await Console.Out.WriteLineAsync("The list of billings has been successfully downloaded." +
            "Now it will be saved in the database");

        await billingService.SaveBillingsAsync(getResult.Value);

        Console.ReadKey();


    }
}