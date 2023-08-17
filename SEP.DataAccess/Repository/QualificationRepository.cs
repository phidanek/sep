using SEP.DataAccess.Repository.IRepository;
using SEP.Models;

namespace SEP.DataAccess.Repository
{
    public class QualificationRepository : Repository<Qualifications>, IQualificationRepository
    {
        private ApplicationDbContext _db;
        public QualificationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Qualifications obj)
        {
            _db.Qualifications.Update(obj);
        }
        public IEnumerable<Qualifications> GetByUserId(string userId)
        {
            IEnumerable<Qualifications> qualifications = _db.Qualifications.Where(d => d.StudentId == userId);
            return qualifications;
        }
    }
}
