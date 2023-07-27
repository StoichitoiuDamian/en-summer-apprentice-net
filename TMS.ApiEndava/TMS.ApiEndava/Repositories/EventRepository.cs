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

        public int Add(Event @event)
        {
            throw new NotImplementedException();
        }

        public void Delete(Event @event)
        {
            _dbContext.Remove(@event);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Event> GetAll()
        {
            var events = _dbContext.Event1s;

            return events;
        }

        public async Task<Event> GetById(long id)
        {
            var @event = await _dbContext.Event1s.Where(e => e.EventId == id).FirstOrDefaultAsync();

            return @event;
        }



        public void Update(Event @event)
        {
            _dbContext.Entry(@event).State = EntityState.Modified;
            _dbContext.SaveChanges();

        }
    }
}
