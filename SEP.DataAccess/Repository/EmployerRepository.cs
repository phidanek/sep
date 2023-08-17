using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SEP.DataAccess.Repository.IRepository;
using SEP.Models;

namespace SEP.DataAccess.Repository
{
    public class EmployerRepository : Repository<Employer>, IEmployerRepository
    {
        private ApplicationDbContext _db;
        public EmployerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            //_db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public void Update(Employer obj)
        {
            //_db.Employer.AsNoTracking().FirstOrDefault(x => x.Id == obj.Id);
            _db.Employer.Update(obj);
        }
    }
}
