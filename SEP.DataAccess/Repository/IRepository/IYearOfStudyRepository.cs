using SEP.Models;

namespace SEP.DataAccess.Repository.IRepository
{
    public interface IYearOfStudyRepository : IRepository<YearOfStudy>
    {
        void Update(YearOfStudy obj);
    }
}
