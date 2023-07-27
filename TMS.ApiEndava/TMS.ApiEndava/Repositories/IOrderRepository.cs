using TMS.ApiEndava.Models;

namespace TMS.ApiEndava.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();

        Task<Order> GetById(long id);

        int Add(Order @order);

        void Update(Order @order);

        void Delete(Order @order);
    }
}
