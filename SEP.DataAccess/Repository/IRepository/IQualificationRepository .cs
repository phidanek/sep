using SEP.Models;

namespace SEP.DataAccess.Repository.IRepository
{
    public interface IQualificationRepository : IRepository<Qualifications>
    {
        void Update(Qualifications obj);
        IEnumerable<Qualifications> GetByUserId(string userId);
    }
}
