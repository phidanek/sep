using SEP.Models;

namespace SEP.DataAccess.Repository.IRepository
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        void Update(Department obj);
    }
}
