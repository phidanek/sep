using SEP.Models;

namespace SEP.DataAccess.Repository.IRepository
{
    public interface IStatusRepository : IRepository<Status>
    {
        void Update(Status obj);
    }
}
