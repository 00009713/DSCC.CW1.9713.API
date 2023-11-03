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

        public void Create(Order item)
        {
            dbContext.Add(item);
            Save();
        }

        public void Delete(int Id)
        {
            var order = dbContext.Orders.Find(Id);
            dbContext.Orders.Remove(order);
            Save();
        }

        public IEnumerable<Order> GetAll()
        {
            return dbContext.Orders.ToList();
        }

        public Order GetById(int Id)
        {
            var prod = dbContext.Orders.Find(Id);
            return prod;
        }

        public void Update(Order item)
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
