using DSCC.CW1._9713.API.Db;
using DSCC.CW1._9713.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DSCC.CW1._9713.API.Services
{
    public class CustomerService : IService<Customer>
    {
        private readonly AppDbContext dbContext;

        public CustomerService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Customer item)
        {
            dbContext.Add(item);
            await SaveAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var customer = await dbContext.Customers.FindAsync(Id);
            if (customer != null)
            {
                dbContext.Customers.Remove(customer);
                await SaveAsync();
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await dbContext.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int Id)
        {
            return await dbContext.Customers.FindAsync(Id);
        }

        public async Task UpdateAsync(Customer item)
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
