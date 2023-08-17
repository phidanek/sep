using Microsoft.EntityFrameworkCore;
using SEP.DataAccess.Repository.IRepository;
using SEP.Models;

namespace SEP.DataAccess.Repository
{
    public class JobPostRepository : Repository<JobPost>, IJobPostRepository
    {
        private ApplicationDbContext _db;
        public JobPostRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(JobPost obj)
        {
            _db.JobPost.Update(obj);
        }
        public IEnumerable<StudentApplication> GetApplyJobPost(string userId)
        {
            IEnumerable<StudentApplication> studentApplication = _db.StudentApplication.Where(d => d.ApplicationUserId == userId)
                .Include(a=>a.jobPost).ThenInclude(a=>a.Department)
                .Include(a=>a.jobPost).ThenInclude(a=>a.WeekHour);
            //IEnumerable<JobPost> jobPosts = _db.StudentApplication.Where(d => d.ApplicationUserId == userId).Select(a => a.jobPost);
           // IEnumerable<JobPost> jobPosts=new List<JobPost>();
            return studentApplication;
        }
    }
}
