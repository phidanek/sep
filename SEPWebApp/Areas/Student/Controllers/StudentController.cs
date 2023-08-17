using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SEP.DataAccess;
using SEP.DataAccess.Repository.IRepository;
using SEP.Models;
using SEP.Models.ViewModels;
using SEP.Utility;
using SEPWebApp.Areas.Home.Controllers;
using SmartBreadcrumbs.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Cryptography;

namespace SEPWebApp.Areas.Controllers
{
    [Area("Student")]
    [Authorize(Roles = SD.Role_Student)]
    public class StudentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<IdentityUser> _userManager;
        private ApplicationDbContext _db;

        public StudentController(IUnitOfWork unitOfWork, ILogger<HomeController> logger, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _db = db;
        }

        [Breadcrumb("Home", AreaName = "Student")]
        public IActionResult Index()
        {

            var studentID = _userManager.GetUserId(User);
            Student student = _unitOfWork.Student.GetFirstOrDefault(d => d.Id == studentID);
            if (student == null)
            {
                return RedirectToAction("AddStudent", "Student", new { area = "Student" });
            }
            return View();
        }
        
        public IActionResult AddStudent()
        {

            var studentID = _userManager.GetUserId(User);
            Student student = _unitOfWork.Student.GetFirstOrDefault(d => d.Id == studentID);
            ApplicationUser user = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Id == studentID);
            StudentVM studentVM = new StudentVM();
            student = new Student();
            student.ApplicationUser = user;
            studentVM.Title = student.ApplicationUser.Title;
            studentVM.Telephone = student.ApplicationUser.Telephone;
            studentVM.cellPhone = student.ApplicationUser.PhoneNumber;
            studentVM.Email = student.ApplicationUser.Email;
            studentVM.FirstName = student.ApplicationUser.FirstName;
            studentVM.LastName = student.ApplicationUser.LastName;
            studentVM.DriverLicenceList = _unitOfWork.DriverLicence.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            studentVM.GenderList = _unitOfWork.Gender.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            studentVM.RaceList = _unitOfWork.Race.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            studentVM.NationalityList = _unitOfWork.Nationality.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            studentVM.YearOfStudyList = _unitOfWork.YearOfStudy.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            studentVM.FacutyList = _db.Faculty;
            studentVM.DepartmentList = _db.Department;

            return View(studentVM);
        }

        public IActionResult EditStudent(StudentVM student)
        {
            var studentID = _userManager.GetUserId(User);
            ApplicationUser user = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Id == studentID);
            Student studentVM = new Student();
            studentVM.ApplicationUser = user;
            studentVM.Achivements = student.Achivements;
            studentVM.Address = student.Address;
            studentVM.RaceId = (int)student.Race;
            studentVM.GenderId = (int)student.Gender;
            studentVM.IdNo = student.IdNo;
            studentVM.Interests = student.Interests;
            studentVM.ApplicationUser.Telephone = student.Telephone;
            studentVM.CareerObjective = student.CareerObjective;
            studentVM.DriversLicenseId = (int)student.DriversLicense;
            studentVM.YearOfStudyId = (int)student.YearOfStudy;
            studentVM.DepartmentId = (int)student.Department;
            studentVM.Skills = student.Skills;
            studentVM.NationalityId = (int)student.Nationality;
            studentVM.Interests = student.Interests;
            studentVM.ApplicationUser.PhoneNumber = student.cellPhone;

            _unitOfWork.Student.Add(studentVM);
            _unitOfWork.Save();
            TempData["success"] = "Student update successfull";
            return RedirectToAction("Index");

        }

        public JsonResult GetDepartments(int facultyId)
        {
            var departments = _unitOfWork.Department.GetAll().Where(d => d.FacultyId == facultyId);

            return Json(departments);
        }
        [Breadcrumb("Profile",AreaName ="Student")]
        public IActionResult Profile()
        {

            var studentId = _userManager.GetUserId(User);
            Student student = _unitOfWork.Student.GetFirstOrDefault(d => d.Id == studentId);
            ApplicationUser user = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Id == studentId);
            StudentVM studentVM = new StudentVM();  

            Department department = _unitOfWork.Department.GetFirstOrDefault(d => d.Id == student.DepartmentId);
            student.ApplicationUser = user;
            // studentVM =student;
            studentVM.Address = student.Address;
            studentVM.Race = student.RaceId;
            studentVM.Gender = student.GenderId;
            studentVM.cellPhone = student.ApplicationUser.PhoneNumber;
            studentVM.Email = student.ApplicationUser.Email;
            studentVM.FirstName = student.ApplicationUser.FirstName;
            studentVM.LastName = student.ApplicationUser.LastName;
            studentVM.IdNo = student.IdNo;
            studentVM.Interests = student.Interests;
            studentVM.Title = student.ApplicationUser.Title;
            studentVM.Telephone = student.ApplicationUser.Telephone;
            studentVM.CareerObjective = student.CareerObjective;
            studentVM.DriversLicense = student.DriversLicenseId;
            studentVM.YearOfStudy = student.YearOfStudyId;
            studentVM.Department = student.DepartmentId;
            studentVM.Skills = student.Skills;
            studentVM.Nationality = student.NationalityId;
            studentVM.Interests = student.Interests;
            studentVM.Faculty = department.FacultyId;
            studentVM.Achivements = student.Achivements;
            studentVM.DriverLicenceList = _unitOfWork.DriverLicence.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            studentVM.GenderList = _unitOfWork.Gender.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            studentVM.RaceList = _unitOfWork.Race.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            studentVM.NationalityList = _unitOfWork.Nationality.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            studentVM.YearOfStudyList = _unitOfWork.YearOfStudy.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            studentVM.FacutyList = _db.Faculty;
            studentVM.DepartmentList = _unitOfWork.Department.GetAll().Where(d => d.FacultyId == studentVM.Faculty);

            studentVM.Referees = _unitOfWork.Referees.GetByUserId(studentId);
            studentVM.Qualification = _unitOfWork.Qualification.GetByUserId(studentId);
            studentVM.Experience = _unitOfWork.Experience.GetByUserId(studentId);
            return View(studentVM);
        }
        [Breadcrumb("Referee", FromAction = "Profile")]
        public IActionResult AddReferees(Referees referees)
        {
            var studentID = _userManager.GetUserId(User);
            Student student = _unitOfWork.Student.GetFirstOrDefault(d => d.Id == studentID);
            referees.StudentId = studentID;
            referees.Student = student;
            _unitOfWork.Referees.Add(referees);
            _unitOfWork.Save();
            TempData["success"] = "Referee added successfully";
            return RedirectToAction("Profile");
        }

        public IActionResult UpdateReferees(Referees referees)
        {
            var studentID = _userManager.GetUserId(User);
            Student student = _unitOfWork.Student.GetFirstOrDefault(d => d.Id == studentID);
            referees.StudentId = studentID;
            referees.Student = student;
            _unitOfWork.Referees.Update(referees);
            _unitOfWork.Save();
            TempData["success"] = "Referee Updated successfully";
            return RedirectToAction("Profile");
        }
        [Breadcrumb("Referee", FromAction = "Profile")]
        public IActionResult EditReferees(int Id)
        {
            Referees referees = _unitOfWork.Referees.GetFirstOrDefault(d => d.Id == Id);
            return View(referees);
        }
        public IActionResult DeleteReferees(int Id)
        {
            Referees referees = _unitOfWork.Referees.GetFirstOrDefault(d => d.Id == Id);
            _unitOfWork.Referees.Remove(referees);
            _unitOfWork.Save();
            TempData["success"] = "Referee Deleted successfully";
            return RedirectToAction("Profile");
        }
        public IActionResult AddExperience(Experience experience)
        {
            var studentID = _userManager.GetUserId(User);
            Student student = _unitOfWork.Student.GetFirstOrDefault(d => d.Id == studentID);
            experience.StudentId = studentID;
            experience.Student = student;
            _unitOfWork.Experience.Add(experience);
            _unitOfWork.Save();
            TempData["success"] = "Experience added successfully";
            return RedirectToAction("Profile");
        }
        public IActionResult UpdateExperience(Experience experience)
        {
            var studentID = _userManager.GetUserId(User);
            Student student = _unitOfWork.Student.GetFirstOrDefault(d => d.Id == studentID);
            experience.StudentId = studentID;
            experience.Student = student;
            _unitOfWork.Experience.Update(experience);
            _unitOfWork.Save();
            TempData["success"] = "Experience Updated successfully";
            return RedirectToAction("Profile");
        }

        [Breadcrumb("Experience", FromAction = "Profile")]
        public IActionResult EditExperience(int Id)
        {
            Experience experience = _unitOfWork.Experience.GetFirstOrDefault(d => d.Id == Id);
            return View(experience);
        }
        public IActionResult DeleteExperience(int Id)
        {
            Experience experience = _unitOfWork.Experience.GetFirstOrDefault(d => d.Id == Id);
            _unitOfWork.Experience.Remove(experience);
            _unitOfWork.Save();
            TempData["success"] = "Experience Deleted successfully";
            return RedirectToAction("Profile");
        }
        public IActionResult AddQualification(Qualifications qualifications)
        {
            var studentID = _userManager.GetUserId(User);
            Student student = _unitOfWork.Student.GetFirstOrDefault(d => d.Id == studentID);
            qualifications.StudentId = studentID;
            qualifications.Student = student;
            _unitOfWork.Qualification.Add(qualifications);
            _unitOfWork.Save();
            TempData["success"] = "Qualification added successfully";
            return RedirectToAction("Profile");
        }
        public IActionResult UpdateQualification(Qualifications qualifications)
        {
            var studentID = _userManager.GetUserId(User);
            Student student = _unitOfWork.Student.GetFirstOrDefault(d => d.Id == studentID);
            qualifications.StudentId = studentID;
            qualifications.Student = student;
            _unitOfWork.Qualification.Update(qualifications);
            _unitOfWork.Save();
            TempData["success"] = "Qualification Updated successfully";
            return RedirectToAction("Profile");
        }
        [Breadcrumb("Qualification", FromAction ="Profile")]
        public IActionResult EditQualification(int Id)
        {
            Qualifications qualifications = _unitOfWork.Qualification.GetFirstOrDefault(d => d.Id == Id);
            return View(qualifications);
        }
        public IActionResult DeleteQualification(int Id)
        {
            Qualifications qualifications = _unitOfWork.Qualification.GetFirstOrDefault(d => d.Id == Id);
            _unitOfWork.Qualification.Remove(qualifications);
            _unitOfWork.Save();
            TempData["success"] = "Referee Deleted successfully";
            return RedirectToAction("Profile");
        }
        public IActionResult GetAllJobPost()
        {
            var JobPostList = _unitOfWork.JobPost.GetAll(includeProperties: "Faculty,Department,JobType,WeekHour");
            return Json(new { data = JobPostList });
        }
        public IActionResult GetJobPost(int Id)
        {
            var JobPostList = _unitOfWork.JobPost.GetFirstOrDefault(x => x.Id == Id);
            return Json(new { data = JobPostList });

        }
        public IActionResult GetAllAppyJobPost()
        {
            var studentID = _userManager.GetUserId(User);
            var JobPostList = _unitOfWork.StudentApplication.GetAll(includeProperties: "Department,WeekHour,Status").Where(u => u.ApplicationUserId == studentID);
            //var JobPostList = _unitOfWork.JobPost.GetApplyJobPost(studentID);
            //var JobPostList = _unitOfWork.JobPost.GetApplyJobPost(studentID);

            return Json(new { data = JobPostList });
        }
        [Breadcrumb("Profile", AreaName = "Student")]
        public IActionResult UpdateProfile(StudentVM student)
        {
            var studentID = _userManager.GetUserId(User);
            ApplicationUser user = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Id == studentID);
            Student studentVM = _unitOfWork.Student.GetFirstOrDefault(d => d.Id == studentID);

            studentVM.ApplicationUser = user;
            studentVM.Address = student.Address;
            studentVM.RaceId = (int)student.Race;
            studentVM.GenderId = (int)student.Gender;
            studentVM.IdNo = student.IdNo;
            studentVM.Interests = student.Interests;
            studentVM.ApplicationUser.Telephone = student.Telephone;
            studentVM.CareerObjective = student.CareerObjective;
            studentVM.DriversLicenseId = (int)student.DriversLicense;
            studentVM.YearOfStudyId = (int)student.YearOfStudy;
            studentVM.DepartmentId = (int)student.Department;
            studentVM.Skills = student.Skills;
            studentVM.NationalityId = (int)student.Nationality;
            studentVM.Interests = student.Interests;
            studentVM.ApplicationUser.PhoneNumber = student.cellPhone;
            studentVM.Achivements = student.Achivements;
            _unitOfWork.Student.Update(studentVM);
            _unitOfWork.Save();
            TempData["success"] = "Student update successfull";
            return RedirectToAction("Index");

        }

        [Breadcrumb("Application History", FromAction ="Search")]
        public IActionResult ReviewApplication(int Id)
        {
            StudentApplication studentApplication = new StudentApplication();
            studentApplication = _db.StudentApplication.Where(x => x.Id == Id).Include(a => a.jobPost).ThenInclude(a => a.JobType).Include(a=>a.jobPost).ThenInclude(a=>a.WeekHour).FirstOrDefault();
            return View(studentApplication);
        }
        public IActionResult WithdrawApplication(int Id)
        {
            StudentApplication studentApplication = new StudentApplication();
            studentApplication = _db.StudentApplication.Where(x => x.Id == Id).FirstOrDefault();
            studentApplication.status = "withdrawn";
            _db.StudentApplication.Update(studentApplication);
            _db.SaveChanges();
            return RedirectToAction("History");

        }

        [Breadcrumb("Referees", FromAction = "Profile")]
        public IActionResult Referees()
        {
            return View();
        }
        [Breadcrumb("Qualification", FromAction ="Profile")]
        public IActionResult Education()
        {
            return View();
        }
        [Breadcrumb("Experience", FromAction ="Profile")]
        public IActionResult Employment()
        {
            return View();
        }
        [Breadcrumb("Application History ", AreaName = "Student")]
        public IActionResult History()
        {
            return View();
        }
        [Breadcrumb("Apply", AreaName = "Student")]
        public IActionResult Search()
        {
            return View();
        }

        //File Upload
        //GET
        [Breadcrumb("Upload", AreaName = "Student")]
        public IActionResult File(int ApplicationId)
        {
            FileVM fileVM = new()
            {
                ApplicationDocument = new(),
                Application = new(),

            };

            fileVM.Application.Id = ApplicationId;
            return View(fileVM);


        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult File(FileVM obj, IFormFile? file)
        {


            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var uploads = Path.Combine(wwwRootPath, @"files\Documents");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.ApplicationDocument.FilePath != null)
                    {
                        var oldFilePath = Path.Combine(wwwRootPath, obj.ApplicationDocument.FilePath.TrimStart('\\'));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.ApplicationDocument.FilePath = @"\files\Documents\" + fileName + extension;
                    obj.ApplicationDocument.Name = fileName;
                    obj.ApplicationDocument.FileType = file.ContentType;
                }
              //  obj.ApplicationDocument.ApplicationId;
                _unitOfWork.ApplicationDocument.Add(obj.ApplicationDocument);

                _unitOfWork.Save();
                TempData["success"] = "Document uploaded successfully";
                return View();
            }

            return View();
        }

        //GET
        [Breadcrumb("Apply", AreaName = "Student")]
        public IActionResult Apply(int? id)
        {
            JobPostVM JobPostVM = new()
            {
                JobPost = new(),
                StatusList = _unitOfWork.Status.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            var StudentID = _userManager.GetUserId(User);


            //update JobPost
            JobPostVM.JobPost = _unitOfWork.JobPost.GetFirstOrDefault(u => u.Id == id);

            IEnumerable<Faculty> faculties = _db.Faculty;

            JobPostVM.FacultyList = faculties;

            IEnumerable<Department> departments = _db.Department.Where(d => d.FacultyId == JobPostVM.JobPost.FacultyId);

            JobPostVM.DepartmentList = departments;

            IEnumerable<JobType> jobTypes = _db.JobType;
            JobPostVM.JobTypeList = jobTypes;

            IEnumerable<WeekHour> weekHour = _db.WeekHour.Where(d => d.JobTypeId == JobPostVM.JobPost.JobTypeId);

            JobPostVM.WeekHourList = weekHour;

            return View(JobPostVM);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Apply(JobPostVM obj)
        {

            var EmployerId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                obj.StudentApplication.ApplicationUserId = EmployerId;
                obj.StudentApplication.JobPostId = obj.JobPost.Id;

                _unitOfWork.StudentApplication.Add(obj.StudentApplication);


                _unitOfWork.Save();
                TempData["success"] = "Application created successfully";
                return RedirectToAction("Search");
            }
            return View(obj);

        }




        #region API CALLS
        [HttpGet]
        public IActionResult GetAllDocuments(int ApplicationId)
        {
            var StudentID = _userManager.GetUserId(User);

            var DocumentList = _unitOfWork.ApplicationDocument.GetAll().Where(u => u.ApplicationId == ApplicationId);
            /*            var DocumentList = _unitOfWork.ApplicationDocument.GetAll();*/
            return Json(new { data = DocumentList });
        }

        [HttpDelete]
        public IActionResult DeleteDocument(int? id)
        {
            var obj = _unitOfWork.ApplicationDocument.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.FilePath.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.ApplicationDocument.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion



        #region API CALLS
        [HttpGet]
        public IActionResult GetAllDocuments()
        {
            var StudentID = _userManager.GetUserId(User);

            var DocumentList = _unitOfWork.ApplicationDocument.GetAll().Where(u => u.ApplicationUserId == StudentID);
            /*            var DocumentList = _unitOfWork.ApplicationDocument.GetAll();*/
            return Json(new { data = DocumentList });
        }

        [HttpDelete]
        public IActionResult DeleteDocument(int? id)
        {
            var obj = _unitOfWork.ApplicationDocument.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.FilePath.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.ApplicationDocument.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion



        #region API CALLS
        [HttpGet]
        public IActionResult GetAllDocuments()
        {
            var StudentID = _userManager.GetUserId(User);

            var DocumentList = _unitOfWork.ApplicationDocument.GetAll().Where(u => u.ApplicationUserId == StudentID);
            /*            var DocumentList = _unitOfWork.ApplicationDocument.GetAll();*/
            return Json(new { data = DocumentList });
        }

        [HttpDelete]
        public IActionResult DeleteDocument(int? id)
        {
            var obj = _unitOfWork.ApplicationDocument.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.FilePath.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.ApplicationDocument.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion



        #region API CALLS
        [HttpGet]
        public IActionResult GetAllDocuments()
        {
            var StudentID = _userManager.GetUserId(User);

            var DocumentList = _unitOfWork.ApplicationDocument.GetAll().Where(u => u.ApplicationUserId == StudentID);
            /*            var DocumentList = _unitOfWork.ApplicationDocument.GetAll();*/
            return Json(new { data = DocumentList });
        }

        [HttpDelete]
        public IActionResult DeleteDocument(int? id)
        {
            var obj = _unitOfWork.ApplicationDocument.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.FilePath.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.ApplicationDocument.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion



        #region API CALLS
        [HttpGet]
        public IActionResult GetAllDocuments()
        {
            var StudentID = _userManager.GetUserId(User);

            var DocumentList = _unitOfWork.ApplicationDocument.GetAll().Where(u => u.ApplicationUserId == StudentID);
            /*            var DocumentList = _unitOfWork.ApplicationDocument.GetAll();*/
            return Json(new { data = DocumentList });
        }

        [HttpDelete]
        public IActionResult DeleteDocument(int? id)
        {
            var obj = _unitOfWork.ApplicationDocument.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.FilePath.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.ApplicationDocument.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion



        #region API CALLS
        [HttpGet]
        public IActionResult GetAllDocuments()
        {
            var StudentID = _userManager.GetUserId(User);

            var DocumentList = _unitOfWork.ApplicationDocument.GetAll().Where(u => u.ApplicationUserId == StudentID);
            /*            var DocumentList = _unitOfWork.ApplicationDocument.GetAll();*/
            return Json(new { data = DocumentList });
        }

        [HttpDelete]
        public IActionResult DeleteDocument(int? id)
        {
            var obj = _unitOfWork.ApplicationDocument.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.FilePath.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.ApplicationDocument.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion



        #region API CALLS
        [HttpGet]
        public IActionResult GetAllDocuments()
        {
            var StudentID = _userManager.GetUserId(User);

            var DocumentList = _unitOfWork.ApplicationDocument.GetAll().Where(u => u.ApplicationUserId == StudentID);
            /*            var DocumentList = _unitOfWork.ApplicationDocument.GetAll();*/
            return Json(new { data = DocumentList });
        }

        [HttpDelete]
        public IActionResult DeleteDocument(int? id)
        {
            var obj = _unitOfWork.ApplicationDocument.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.FilePath.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.ApplicationDocument.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion



        #region API CALLS
        [HttpGet]
        public IActionResult GetAllDocuments()
        {
            var StudentID = _userManager.GetUserId(User);

            var DocumentList = _unitOfWork.ApplicationDocument.GetAll().Where(u => u.ApplicationUserId == StudentID);
            /*            var DocumentList = _unitOfWork.ApplicationDocument.GetAll();*/
            return Json(new { data = DocumentList });
        }

        [HttpDelete]
        public IActionResult DeleteDocument(int? id)
        {
            var obj = _unitOfWork.ApplicationDocument.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.FilePath.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.ApplicationDocument.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}
