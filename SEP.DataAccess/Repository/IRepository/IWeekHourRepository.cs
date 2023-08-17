using SEP.Models;

namespace SEP.DataAccess.Repository.IRepository
{
    public interface IWeekHourRepository : IRepository<WeekHour>
    {
        void Update(WeekHour obj);
    }
}
