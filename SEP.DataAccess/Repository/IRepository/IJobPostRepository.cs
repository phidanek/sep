using SEP.Models;

namespace SEP.DataAccess.Repository.IRepository
{
    public interface IJobPostRepository : IRepository<JobPost>
    {
        void Update(JobPost obj);
        IEnumerable<StudentApplication> GetApplyJobPost(string userId);
    }
}
