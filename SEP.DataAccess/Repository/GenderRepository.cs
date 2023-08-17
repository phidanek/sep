using SEP.DataAccess.Repository.IRepository;
using SEP.Models;

namespace SEP.DataAccess.Repository
{
    public class GenderRepository : Repository<Gender>, IGenderRepository
    {
        private ApplicationDbContext _db;
        public GenderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Gender obj)
        {
            _db.Gender.Update(obj);
        }
    }
}
