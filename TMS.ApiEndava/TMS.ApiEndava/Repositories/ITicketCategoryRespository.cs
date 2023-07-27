using TMS.ApiEndava.Models;

namespace TMS.ApiEndava.Repositories
{
    public interface ITicketCategoryRespository
    {
        IEnumerable<TicketCategory> GetAll();



        Task<TicketCategory> GetById(int id);



        int Add(TicketCategory @ticketCategory);



        void Update(TicketCategory @ticketCategory);



        void Delete(TicketCategory @ticketCategory);
    }
}
