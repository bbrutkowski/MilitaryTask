using CSharpFunctionalExtensions;
using MilitaryASPWeb.BussinessLogic.Model;

namespace MilitaryASPWeb.BussinessLogic.Services.Interfaces
{
    public interface IFileService
    {
        public Task<Result<ProductCatalog>> ProcessXmlFiles();
    }
}
