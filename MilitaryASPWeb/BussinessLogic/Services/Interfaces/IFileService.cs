using CSharpFunctionalExtensions;
using MilitaryASPWeb.Models.Model;

namespace MilitaryASPWeb.Models.Services.Interfaces
{
    public interface IFileService
    {
        public Task<Result<ProductCatalog>> ProcessXmlFiles();
    }
}
