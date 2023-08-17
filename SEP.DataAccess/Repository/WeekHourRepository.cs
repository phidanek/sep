using SEP.DataAccess.Repository.IRepository;
using SEP.Models;

namespace SEP.DataAccess.Repository
{
    public class WeekHourRepository : Repository<WeekHour>, IWeekHourRepository
    {
        private ApplicationDbContext _db;
        public WeekHourRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(WeekHour obj)
        {
            _db.WeekHour.Update(obj);
        }
    }
}
