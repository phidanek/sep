using SEP.Models;

namespace SEP.DataAccess.Repository.IRepository
{
    public interface IExperienceRepository : IRepository<Experience>
    {
        void Update(Experience obj);
        IEnumerable<Experience> GetByUserId(string userId);
    }
}
