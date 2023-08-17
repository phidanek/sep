using SEP.DataAccess.Repository.IRepository;
using SEP.Models;

namespace SEP.DataAccess.Repository
{
    public class ExperienceRepository : Repository<Experience>, IExperienceRepository
    {
        private ApplicationDbContext _db;
        public ExperienceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Experience obj)
        {
            _db.Experience.Update(obj);
        }

        public IEnumerable<Experience> GetByUserId(string userId)
        {
            IEnumerable<Experience> experiences = _db.Experience.Where(d => d.StudentId == userId);
            return experiences;
        }
    }
}
