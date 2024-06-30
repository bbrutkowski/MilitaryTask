using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using MilitaryTask.Model;
using MilitaryTask.Repository.Interfaces;
using DataContextAlias = MilitaryTask.DataContext.DataContext;

namespace MilitaryTask.Repository
{
    internal class BillingRepository : IBillingRespository
    {
        private readonly DataContextAlias _dataContext;

        public BillingRepository(DataContextAlias dataContext) => _dataContext = dataContext;
         
        public async Task<Result> SaveSortedBillsAsync(List<Bill> bills)
        {
            try
            {
                foreach (var bill in bills)
                {
                    var existingTender = await _dataContext.Tenders
                        .FirstOrDefaultAsync(t => t.TenderId == bill.Tender.TenderId);
                    if (existingTender is not null)
                    {
                        bill.TenderId = existingTender.Id;
                        bill.Tender = existingTender;
                    }
                    else
                    {
                        await _dataContext.Tenders.AddAsync(bill.Tender);
                        await _dataContext.SaveChangesAsync();  
                        bill.TenderId = bill.Tender.Id;
                    }

                    var existingBillType = await _dataContext.BillTypes
                        .FirstOrDefaultAsync(bt => bt.BillTypeId == bill.BillType.BillTypeId);
                    if (existingBillType is not null)
                    {
                        bill.BillTypeId = existingBillType.Id;
                        bill.BillType = existingBillType;
                    }
                    else
                    {
                        await _dataContext.BillTypes.AddAsync(bill.BillType);
                        await _dataContext.SaveChangesAsync();  
                        bill.BillTypeId = bill.BillType.Id;
                    }

                    await _dataContext.Bills.AddAsync(bill);
                }

                await _dataContext.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception ex)
            { 
                return Result.Failure(ex.Message);
            }
        } 

        public async Task<Result> SaveBillTypesAsync(List<BillType> billTypes)
        {
            try
            {
                await _dataContext.BillTypes.AddRangeAsync(billTypes);
                await _dataContext.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception e)
            {
                return Result.Failure(e.Message);
            }
        }
    }
}
