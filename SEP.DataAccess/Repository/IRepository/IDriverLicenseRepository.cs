using SEP.Models;

namespace SEP.DataAccess.Repository.IRepository
{
    public interface IDriverLicenseRepository : IRepository<DriverLicense>
    {
        void Update(DriverLicense obj);
    }
}