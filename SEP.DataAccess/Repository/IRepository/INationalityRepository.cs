using SEP.Models;

namespace SEP.DataAccess.Repository.IRepository
{
    public interface INationalityRepository : IRepository<Nationality>
    {
        void Update(Nationality obj);
    }
}
