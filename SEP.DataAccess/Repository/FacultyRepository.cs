using SEP.DataAccess.Repository.IRepository;
using SEP.Models;

namespace SEP.DataAccess.Repository
{
    public class FacultyRepository : Repository<Faculty>, IFacultyRepository
    {
        private ApplicationDbContext _db;
        public FacultyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Faculty obj)
        {
            _db.Faculty.Update(obj);
        }
    }
}
