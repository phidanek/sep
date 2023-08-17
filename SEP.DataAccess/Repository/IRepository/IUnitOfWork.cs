
namespace SEP.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IJobPostRepository JobPost { get; }
        IEmployerRepository Employer { get; }
        IDriverLicenseRepository DriverLicence { get; }
        IGenderRepository Gender { get; }
        IRaceRepository Race { get; }
        INationalityRepository Nationality { get; }
        IYearOfStudyRepository YearOfStudy { get; }
        IDepartmentRepository Department { get; }
        IFacultyRepository Faculty { get; }
        IStudentRepository Student { get; }
        IRefereesRepository Referees { get; }
        IExperienceRepository Experience { get; }
        IQualificationRepository Qualification { get; }

        IJobTypeRepository JobType { get; }
        IWeekHourRepository WeekHour { get; }
        IStatusRepository Status { get; }
        IBusinessTypeRepository BusinessType { get; }

        IApplicationUserRepository ApplicationUser { get; }


        void Save();

    }
}
