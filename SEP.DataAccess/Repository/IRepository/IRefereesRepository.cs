using SEP.Models;

namespace SEP.DataAccess.Repository.IRepository
{
    public interface IRefereesRepository : IRepository<Referees>
    {
        void Update(Referees obj);
        IEnumerable<Referees> GetByUserId(string userId);
    }
}
