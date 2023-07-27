
using TMS.ApiEndava.Models;

namespace TMS.ApiEndava.Repositories

{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();
        
       Task<Event> GetById(long id);

        int Add(Event @event);

        void Update (Event @event);
        
        void Delete (Event @event);
    }
}
