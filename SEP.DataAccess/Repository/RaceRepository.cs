using SEP.DataAccess.Repository.IRepository;
using SEP.Models;

namespace SEP.DataAccess.Repository
{
    public class RaceRepository : Repository<Race>, IRaceRepository
    {
        private ApplicationDbContext _db;
        public RaceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Race obj)
        {
            _db.Race.Update(obj);
        }
    }
}
