using Microsoft.EntityFrameworkCore;
using TMS.ApiEndava.Models;

namespace TMS.ApiEndava.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private EndavaPracticaV3Context _dbContext;

        public OrderRepository()
        {
            _dbContext = new EndavaPracticaV3Context();
        }

        public int Add(Order @order)
        {
            throw new NotImplementedException();
        }

        public void Delete(Order order)
        {
            _dbContext.Remove(order);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Order> GetAll()
        {
            var @order = _dbContext.Order1s.ToList();

            return order;
        }

        public async Task<Order> GetById(long id)
        {
            var @order = await _dbContext.Order1s.Where(e => e.OrderId == id).FirstOrDefaultAsync();

            return order;
        }



        public void Update(Order @order)
        {
            _dbContext.Entry(@order).State = EntityState.Modified;
            _dbContext.SaveChanges();

        }
    }
}
