using TMS.ApiEndava.Models;

namespace TMS.ApiEndava.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order1> GetAll();

        Task<Order1> GetById(long id);

        int Add(Order1 @order);

        void Update(Order1 @order);

        void Delete(Order1 @order);
    }
}
