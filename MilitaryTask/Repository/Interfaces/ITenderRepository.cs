using CSharpFunctionalExtensions;
using MilitaryTask.Model;

namespace MilitaryTask.Repository.Interfaces
{
    public interface ITenderRepository
    {
        Task<Result> SaveTenderAsync(Tender tender);
        Task<Result> SaveTendersAsync(List<Tender> tender);
    }
}
