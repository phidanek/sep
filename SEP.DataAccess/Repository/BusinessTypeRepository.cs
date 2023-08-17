using SEP.DataAccess.Repository.IRepository;
using SEP.Models;
namespace SEP.DataAccess.Repository
{
    public class BusinessTypeRepository : Repository<BusinessType>, IBusinessTypeRepository
    {
        private ApplicationDbContext _db;
        public BusinessTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(BusinessType obj)
        {
            _db.BusinessType.Update(obj);
        }
    }
}
