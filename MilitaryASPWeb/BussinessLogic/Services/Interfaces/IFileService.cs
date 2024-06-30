using CSharpFunctionalExtensions;
using MilitaryASPWeb.BussinessLogic.Model;

namespace MilitaryASPWeb.Models.Services.Interfaces
{
    public interface IFileService
    {
        public Task<Result<List<Product>>> CreateProductListFromDeliveredXmlFilesAsync();
    }
}
