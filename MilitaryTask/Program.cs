using MilitaryTask.Bindings;
using MilitaryTask.BussinesLogic.Interfaces;
using Ninject;

internal class Program
{
    private static async Task Main()
    {
        var kernel = new StandardKernel();
        kernel.Load(new Bindings());
        var orderCostsService = kernel.Get<IOrderCostsService>();
        await Run(orderCostsService);
    }

    private static async Task Run(IOrderCostsService orderCostsService)
    {
        var costsData = await orderCostsService.GetOrderCostsAsync();
        await orderCostsService.SaveOrderCostsAsync(costsData.Value);
    }
}