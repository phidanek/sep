using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SEP.Models;

namespace SEP.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Employer> Employer { get; set; }
        public DbSet<JobPost> JobPost { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<DriverLicense> DriverLicense { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Race> Race { get; set; }
        public DbSet<Nationality> Nationality { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<YearOfStudy> YearOfStudy { get; set; }
        public DbSet<Qualifications> Qualifications { get; set; }
        public DbSet<Referees> Referees { get; set; }
        public DbSet<Experience> Experience { get; set; }

        public DbSet<JobType> JobType { get; set; }
        public DbSet<WeekHour> WeekHour { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<BusinessType> BusinessType { get; set; }
        public DbSet<StudentApplication> StudentApplication { get; set; }
        public DbSet<ApplicationDocument> ApplicationDocument { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatus { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // Configure JobType entity
            modelBuilder.Entity<JobType>().ToTable("JobType");
            modelBuilder.Entity<JobType>().HasKey(f => f.Id);
            modelBuilder.Entity<JobType>().Property(f => f.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<JobType>().Property(f => f.Name).IsRequired();

            // Configure WeekHour entity
            modelBuilder.Entity<WeekHour>().ToTable("WeekHour");
            modelBuilder.Entity<WeekHour>().HasKey(f => f.Id);
            modelBuilder.Entity<WeekHour>().Property(f => f.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<WeekHour>().Property(f => f.Name).IsRequired();
            modelBuilder.Entity<WeekHour>().HasOne(d => d.JobType)
                .WithMany(f => f.WeekHours)
                .HasForeignKey(d => d.JobTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Status entity
            modelBuilder.Entity<Status>().ToTable("Status");
            modelBuilder.Entity<Status>().HasKey(f => f.Id);
            modelBuilder.Entity<Status>().Property(f => f.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Status>().Property(f => f.Name).IsRequired();

            // Configure BusinessType entity
            modelBuilder.Entity<BusinessType>().ToTable("BusinessType");
            modelBuilder.Entity<BusinessType>().HasKey(f => f.Id);
            modelBuilder.Entity<BusinessType>().Property(f => f.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<BusinessType>().Property(f => f.Name).IsRequired();


            // Configure Faculty entity
            modelBuilder.Entity<Faculty>().ToTable("Faculty");
            modelBuilder.Entity<Faculty>().HasKey(f => f.Id);
            modelBuilder.Entity<Faculty>().Property(f => f.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Faculty>().Property(f => f.Name).IsRequired();

            // Configure Gender entity
            modelBuilder.Entity<Gender>().ToTable("Gender");
            modelBuilder.Entity<Gender>().HasKey(g => g.Id);
            modelBuilder.Entity<Gender>().Property(g => g.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Gender>().Property(g => g.Name).IsRequired();

            // Configure Department entity
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Department>().HasKey(d => d.Id);
            modelBuilder.Entity<Department>().Property(d => d.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Department>().Property(d => d.Name).IsRequired();
            modelBuilder.Entity<Department>().HasOne(d => d.Faculty)
                .WithMany(f => f.Departments)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.Restrict);


            // Configure Race entity
            modelBuilder.Entity<Race>().ToTable("Race");
            modelBuilder.Entity<Race>().HasKey(r => r.Id);
            modelBuilder.Entity<Race>().Property(r => r.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Race>().Property(r => r.Name).IsRequired();

            // Configure Nationality entity
            modelBuilder.Entity<Nationality>().ToTable("Nationality");
            modelBuilder.Entity<Nationality>().HasKey(n => n.Id);
            modelBuilder.Entity<Nationality>().Property(n => n.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Nationality>().Property(n => n.Name).IsRequired();

            // Configure YearOfStudy entity
            modelBuilder.Entity<YearOfStudy>().ToTable("YearOfStudy");
            modelBuilder.Entity<YearOfStudy>().HasKey(y => y.Id);
            modelBuilder.Entity<YearOfStudy>().Property(y => y.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<YearOfStudy>().Property(y => y.Name).IsRequired();

            // confingure DriverLicense entity
            modelBuilder.Entity<DriverLicense>().ToTable("DriverLicense");
            modelBuilder.Entity<DriverLicense>().HasKey(x => x.Id);
            modelBuilder.Entity<DriverLicense>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<DriverLicense>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<ApplicationStatus>().ToTable("ApplicationStatus");
            modelBuilder.Entity<ApplicationStatus>().HasKey(f => f.Id);
            modelBuilder.Entity<ApplicationStatus>().Property(f => f.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ApplicationStatus>().Property(f => f.Name).IsRequired();

            // Seed data for JobType
            modelBuilder.Entity<JobType>().HasData(
                new JobType { Id = 1, Name = "Part-time" },
                new JobType { Id = 2, Name = "Fulltime" }

            );

            modelBuilder.Entity<ApplicationStatus>().HasData(
                new ApplicationStatus { Id = 1, Name = "Pending" },
                new ApplicationStatus { Id = 2, Name = "Approved" },
                new ApplicationStatus { Id = 3, Name = "Rejected" },
                new ApplicationStatus { Id = 4, Name = "Withdrawn" }

            );

            // Seed data for WeekHours
            modelBuilder.Entity<WeekHour>().HasData(
                new WeekHour { Id = 1, Name = "<2", JobTypeId = 1 },
                new WeekHour { Id = 2, Name = "4 to 6", JobTypeId = 1 },
                new WeekHour { Id = 3, Name = "6 to 8", JobTypeId = 1 },
                new WeekHour { Id = 4, Name = "8 to 12", JobTypeId = 1 },
                new WeekHour { Id = 5, Name = ">12", JobTypeId = 1 },
                new WeekHour { Id = 6, Name = "Fulltime", JobTypeId = 2 }

            );

            // Seed data for Status
            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "Pending" },
                new Status { Id = 2, Name = "Rejected" },
                new Status { Id = 3, Name = "Queried" },
                new Status { Id = 4, Name = "Closed" },
                new Status { Id = 5, Name = "Withdrawn" },
                new Status { Id = 6, Name = "Approved" },
                new Status { Id = 7, Name = "Unsuccessful" },
                new Status { Id = 8, Name = "Successful" },
                new Status { Id = 9, Name = "Cancelled" }

            );

            // Seed data for Status
            modelBuilder.Entity<BusinessType>().HasData(
                new BusinessType { Id = 1, Name = "Sole Proprietorship" },
                new BusinessType { Id = 2, Name = "Partnership" },
                new BusinessType { Id = 3, Name = "Pty Ltd - Proprietary limited company" },
                new BusinessType { Id = 4, Name = "Public Company" },
                new BusinessType { Id = 5, Name = "Private Company" },
                new BusinessType { Id = 6, Name = "State Owned Companies" },
                new BusinessType { Id = 7, Name = "Non-profit Company" }

            );

            // Seed data for Faculties
            modelBuilder.Entity<Faculty>().HasData(
                new Faculty { Id = 1, Name = "Faculty of Commerce, Law, and Management" },
                new Faculty { Id = 2, Name = "Faculty of Engineering and the Built Environment" },
                new Faculty { Id = 3, Name = "Faculty of Health Sciences" },
                new Faculty { Id = 4, Name = "Faculty of Humanities" },
                new Faculty { Id = 5, Name = "Faculty of Science" }
            );


            // Seed data for Departments
            modelBuilder.Entity<Department>().HasData(
                // Faculty of Commerce, Law, and Management
                new Department { Id = 1, Name = "School of Accounting", FacultyId = 1 },
                new Department { Id = 2, Name = "School of Economic and Business Sciences", FacultyId = 1 },
                new Department { Id = 3, Name = "Wits Business School", FacultyId = 1 },
                new Department { Id = 4, Name = "School of Governance", FacultyId = 1 },
                new Department { Id = 5, Name = "School of Law", FacultyId = 1 },
                new Department { Id = 6, Name = "Graduate School of Public and Development Management", FacultyId = 1 },

                // Faculty of Engineering and the Built Environment
                new Department { Id = 7, Name = "School of Architecture and Planning", FacultyId = 2 },
                new Department { Id = 8, Name = "School of Chemical and Metallurgical Engineering", FacultyId = 2 },
                new Department { Id = 9, Name = "School of Civil and Environmental Engineering", FacultyId = 2 },
                new Department { Id = 10, Name = "School of Construction Economics and Management", FacultyId = 2 },
                new Department { Id = 11, Name = "School of Electrical and Information Engineering", FacultyId = 2 },
                new Department { Id = 12, Name = "School of Mechanical, Industrial, and Aeronautical Engineering", FacultyId = 2 },
                new Department { Id = 13, Name = "School of Mining Engineering", FacultyId = 2 },

                // Faculty of Health Sciences
                new Department { Id = 14, Name = "School of Anatomical Sciences", FacultyId = 3 },
                new Department { Id = 15, Name = "School of Clinical Medicine", FacultyId = 3 },
                new Department { Id = 16, Name = "School of Oral Health Sciences", FacultyId = 3 },
                new Department { Id = 17, Name = "School of Pathology", FacultyId = 3 },
                new Department { Id = 18, Name = "School of Physiology", FacultyId = 3 },
                new Department { Id = 19, Name = "School of Public Health", FacultyId = 3 },

                // Faculty of Humanities
                new Department { Id = 20, Name = "School of Arts", FacultyId = 4 },
                new Department { Id = 21, Name = "School of Education", FacultyId = 4 },
                new Department { Id = 22, Name = "School of Human and Community Development", FacultyId = 4 },
                new Department { Id = 23, Name = "School of Literature, Language and Media", FacultyId = 4 },
                new Department { Id = 24, Name = "School of Social Sciences", FacultyId = 4 },
                new Department { Id = 25, Name = "School of Therapeutic Sciences", FacultyId = 4 },

                // Faculty of Science
                new Department { Id = 26, Name = "School of Animal, Plant and Environmental Sciences", FacultyId = 5 },
                new Department { Id = 27, Name = "School of Chemistry", FacultyId = 5 },
                new Department { Id = 28, Name = "School of Computer Science and Applied Mathematics", FacultyId = 5 },
                new Department { Id = 29, Name = "School of Geography, Archaeology and Environmental Studies", FacultyId = 5 },
                new Department { Id = 30, Name = "School of Molecular and Cell Biology", FacultyId = 5 },
                new Department { Id = 31, Name = "School of Physics", FacultyId = 5 },
                new Department { Id = 32, Name = "School of Statistics and Actuarial Science", FacultyId = 5 }
            );

            // Seed data for Genders
            modelBuilder.Entity<Gender>().HasData(
                new Gender { Id = 1, Name = "Male" },
                new Gender { Id = 2, Name = "Female" },
                new Gender { Id = 3, Name = "Other" }
            );

            // Seed data for Races
            modelBuilder.Entity<Race>().HasData(
                new Race { Id = 1, Name = "African" },
                new Race { Id = 2, Name = "Coloured" },
                new Race { Id = 3, Name = "Indian" },
                new Race { Id = 4, Name = "White" },
                new Race { Id = 5, Name = "Other" }
            );

            // Seed data for Nationalities
            modelBuilder.Entity<Nationality>().HasData(
                new Nationality { Id = 1, Name = "South African" },
                new Nationality { Id = 2, Name = "non South African" }
            ); ;

            // Seed data for YearsOfStudy
            modelBuilder.Entity<YearOfStudy>().HasData(
                new YearOfStudy { Id = 1, Name = "Year 1" },
                new YearOfStudy { Id = 2, Name = "Year 2" },
                new YearOfStudy { Id = 3, Name = "Year 3" },
                new YearOfStudy { Id = 4, Name = "Year 4" },
                new YearOfStudy { Id = 5, Name = "Honors" },
                new YearOfStudy { Id = 6, Name = "Master's" },
                new YearOfStudy { Id = 7, Name = "PhD" }
            );
            // seed data for DriverLicense
            modelBuilder.Entity<DriverLicense>().HasData(
                new DriverLicense { Id = 1, Name = "A - MotorCycle" },
                new DriverLicense { Id = 2, Name = "A1 - Light MotorCycle" },
                new DriverLicense { Id = 3, Name = "B - Light Motor Vehicle" },
                new DriverLicense { Id = 4, Name = "C - Heavy Motor Vehicle" },
                new DriverLicense { Id = 5, Name = "C1 - Light Heavy Motor Vehicle" },
                new DriverLicense { Id = 6, Name = "EB - Ligth Motor Vehicle + Trailer" },
                new DriverLicense { Id = 7, Name = "None"}
                );
        }

    }
}
