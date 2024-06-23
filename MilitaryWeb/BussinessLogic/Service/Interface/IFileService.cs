using CSharpFunctionalExtensions;
using MilitaryWeb.BussinessLogic.Model;

namespace MilitaryWeb.BussinessLogic.Service.Interface
{
    public interface IFileService
    {
        public Task<Result<ProductCatalog>> ProcessXmlFiles();

    }
}
