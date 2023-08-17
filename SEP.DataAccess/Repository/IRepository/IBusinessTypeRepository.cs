using SEP.Models;

namespace SEP.DataAccess.Repository.IRepository
{
    public interface IBusinessTypeRepository : IRepository<BusinessType>
    {
        void Update(BusinessType obj);
    }
}
