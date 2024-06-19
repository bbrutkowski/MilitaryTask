using Microsoft.EntityFrameworkCore;
using MilitaryTask.Model;

namespace MilitaryTask.DataContext.Interface
{
    public interface IDataContext
    {
        DbSet<Order> OrderTable { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
