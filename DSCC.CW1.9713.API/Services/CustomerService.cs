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
        public void Create(Customer item)
        {
            dbContext.Add(item);
            Save();
        }

        public void Delete(int Id)
        {
            var customer = dbContext.Customers.Find(Id);
            dbContext.Customers.Remove(customer);
            Save();
        }

        public IEnumerable<Customer> GetAll()
        {
            return dbContext.Customers.ToList();
        }

        public Customer GetById(int Id)
        {
            var customer = dbContext.Customers.Find(Id);
            return customer;
        }

        public void Update(Customer item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
            Save();
        }

        // Method for saving the changes
        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
