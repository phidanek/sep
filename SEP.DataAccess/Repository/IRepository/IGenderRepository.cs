using SEP.Models;

namespace SEP.DataAccess.Repository.IRepository
{
    public interface IGenderRepository : IRepository<Gender>
    {
        void Update(Gender obj);
    }
}
