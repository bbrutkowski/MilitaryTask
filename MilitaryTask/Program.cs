using MilitaryTask.Bindings;
using MilitaryTask.BussinesLogic.Interfaces;
using Ninject;
using System.Reflection;

internal class Program
{
    private readonly IOrderCostsService _orderCostsService;

    public Program(IOrderCostsService orderCostsService)
    {
        _orderCostsService = orderCostsService;
    }

    private static void Main()
    {
        var kernel = new StandardKernel();
        kernel.Load(new Bindings());
        var orderCostsService = kernel.Get<IOrderCostsService>();

        new Program(orderCostsService).Run();
    }

    private async void Run()
    {
        await _orderCostsService.GetOrderCostsAsync();
    }
}