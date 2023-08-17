using SEP.Models;

namespace SEP.DataAccess.Repository.IRepository
{
    public interface IRaceRepository : IRepository<Race>
    {
        void Update(Race obj);
    }
}
