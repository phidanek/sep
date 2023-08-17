using SEP.Models;

namespace SEP.DataAccess.Repository.IRepository
{
    public interface IFacultyRepository : IRepository<Faculty>
    {
        void Update(Faculty obj);
    }
}
