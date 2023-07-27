using Microsoft.EntityFrameworkCore;
using static TMS.ApiEndava.Repositories.TicketCategoryRepository;
using TMS.ApiEndava.Models;

namespace TMS.ApiEndava.Repositories
{
    public class TicketCategoryRepository : ITicketCategoryRespository
    {
        private EndavaPracticaV3Context _dbContext;

            public TicketCategoryRepository()
            {
                _dbContext = new EndavaPracticaV3Context();
            }



            public int Add(TicketCategory @ticketCategory)
            {
                throw new NotImplementedException();
            }



            public void Delete(TicketCategory @ticketCategory)
            {
                _dbContext.Remove(@ticketCategory);
                _dbContext.SaveChanges();
            }





            public IEnumerable<TicketCategory> GetAll()
            {
                var @ticketCategory = _dbContext.TicketCategories;



                return @ticketCategory;
            }



            public async Task<TicketCategory> GetById(int id)
            {
                var @ticketCategory = await _dbContext.TicketCategories
                 .Where(e => e.TicketCategoryId == id).FirstOrDefaultAsync();



                return @ticketCategory;
            }



            public void Update(TicketCategory @ticketCategory)
            {
                _dbContext.Entry(@ticketCategory).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
        }
}
