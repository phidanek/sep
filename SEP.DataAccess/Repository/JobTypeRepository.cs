using SEP.DataAccess.Repository.IRepository;
using SEP.Models;

namespace SEP.DataAccess.Repository
{
    public class JobTypeRepository : Repository<JobType>, IJobTypeRepository
    {
        private ApplicationDbContext _db;
        public JobTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(JobType obj)
        {
            _db.JobType.Update(obj);
        }
    }
}
