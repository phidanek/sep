using SEP.Models;

namespace SEP.DataAccess.Repository.IRepository
{
    public interface IJobTypeRepository : IRepository<JobType>
    {
        void Update(JobType obj);
    }
}
