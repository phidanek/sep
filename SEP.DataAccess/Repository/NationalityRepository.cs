using SEP.DataAccess.Repository.IRepository;
using SEP.Models;

namespace SEP.DataAccess.Repository
{
    public class NationalityRepository : Repository<Nationality>, INationalityRepository
    {
        private ApplicationDbContext _db;
        public NationalityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Nationality obj)
        {
            _db.Nationality.Update(obj);
        }
    }
}
