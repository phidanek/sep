using SEP.DataAccess.Repository.IRepository;
using SEP.Models;

namespace SEP.DataAccess.Repository
{
    public class YearOfStudyRepository : Repository<YearOfStudy>, IYearOfStudyRepository
    {
        private ApplicationDbContext _db;
        public YearOfStudyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(YearOfStudy obj)
        {
            _db.YearOfStudy.Update(obj);
        }
    }
}
