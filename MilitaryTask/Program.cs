using Microsoft.Extensions.Configuration;
using MilitaryTask.Bindings;
using MilitaryTask.BussinesLogic.Interfaces;
using MilitaryTask.DataContext;
using Ninject;

internal class Program
{
    private static async Task Main()
    {
        var kernel = new StandardKernel();
        kernel.Load(new Bindings());
        var orderCostsService = kernel.Get<IBillingService>();

        var context = kernel.Get<DataContext>();

        await Run(orderCostsService);
    }

    private static async Task Run(IBillingService orderCostsService)
    {
        var getResult = await orderCostsService.GetBillingListAsync();
        if (getResult.IsSuccess) await Console.Out.WriteLineAsync("The list of billings has been successfully downloaded." +
            "Now it will be saved in the database");

        Console.ReadKey();


    }
}