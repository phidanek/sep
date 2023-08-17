using SEP.Models;

namespace SEP.DataAccess.Repository.IRepository
{
    public interface IEmployerRepository : IRepository<Employer>
    {
        void Update(Employer obj);
    }
}
