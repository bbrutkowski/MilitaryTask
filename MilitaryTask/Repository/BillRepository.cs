using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MilitaryTask.Model;
using MilitaryTask.Repository.Interfaces;
using DataContextAlias = MilitaryTask.DataContext.DataContext;

namespace MilitaryTask.Repository
{
    internal class BillRepository : IBillRespository
    {
        private readonly DataContextAlias _dataContext;

        public BillRepository(DataContextAlias dataContext) => _dataContext = dataContext;
         
        public async Task<Result> SaveBillsAsync(List<Bill> bills)
        {
            try
            {
                foreach (var bill in bills)
                {
                    if (bill.Offer.Id != 0) _dataContext.Entry(bill.Offer).State = EntityState.Modified;             
                    if (bill.BillType.Id != 0) _dataContext.Entry(bill.BillType).State = EntityState.Modified;
                }

                await _dataContext.Bills.AddRangeAsync(bills);
                await _dataContext.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception ex)
            { 
                return Result.Failure(ex.Message);
            }
        } 
    }
}
