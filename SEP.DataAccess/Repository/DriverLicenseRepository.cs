using SEP.DataAccess.Repository.IRepository;
using SEP.Models;

namespace SEP.DataAccess.Repository
{
    public class DriverLicenseRepository : Repository<DriverLicense>, IDriverLicenseRepository
    {
        private ApplicationDbContext _db;
        public DriverLicenseRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(DriverLicense obj)
        {
            _db.DriverLicense.Update(obj);
        }
    }
}
