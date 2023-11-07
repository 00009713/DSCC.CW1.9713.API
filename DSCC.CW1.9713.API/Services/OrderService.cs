using DSCC.CW1._9713.API.Db;
using DSCC.CW1._9713.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DSCC.CW1._9713.API.Services
{
    public class OrderService : IService<Order>
    {
        private readonly AppDbContext dbContext;

        public OrderService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Order item)
        {
            dbContext.Add(item);
            await SaveAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var order = await dbContext.Orders.FindAsync(Id);
            if (order != null)
            {
                dbContext.Orders.Remove(order);
                await SaveAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await dbContext.Orders.ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int Id)
        {
            return await dbContext.Orders.FindAsync(Id);
        }

        public async Task UpdateAsync(Order item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
            await SaveAsync();
        }

        // Method for saving the changes
        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
