
using TMS.ApiEndava.Models;

namespace TMS.ApiEndava.Repositories

{
    public interface IEventRepository
    {
        IEnumerable<Event1> GetAll();
        
       Task<Event1> GetById(long id);

        int Add(Event1 @event);

        void Update (Event1 @event);
        
        void Delete (Event1 @event);
    }
}
