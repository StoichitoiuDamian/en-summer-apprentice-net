using Microsoft.EntityFrameworkCore;
using TMS.ApiEndava.Models;

namespace TMS.ApiEndava.Repositories
{
    public class EventRepository : IEventRepository
    {
        private EndavaPracticaV3Context _dbContext;

        public EventRepository() 
        { 
            _dbContext = new EndavaPracticaV3Context();
        }

        public int Add(Event1 @event)
        {
            throw new NotImplementedException();
        }

        public void Delete(Event1 @event)
        {
            _dbContext.Remove(@event);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Event1> GetAll()
        {
            var events = _dbContext.Event1s;

            return events;
        }

        public async Task<Event1> GetById(long id)
        {
            var @event = await _dbContext.Event1s.Where(e => e.EventId == id).FirstOrDefaultAsync();

            return @event;
        }



        public void Update(Event1 @event)
        {
            _dbContext.Entry(@event).State = EntityState.Modified;
            _dbContext.SaveChanges();

        }
    }
}
