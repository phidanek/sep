using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SEP.DataAccess;
using SEP.DataAccess.Repository.IRepository;
using SEP.Models;
using SEP.Models.ViewModels;
using SEP.Utility;
using SmartBreadcrumbs.Attributes;
using System.Data;

namespace SEPWebApp.Areas.Employer.Controllers
{
    [Area("Employer")]
    [Authorize(Roles = SD.Role_Employer)]
    public class JobPostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public JobPostController(IUnitOfWork unitOfWork, ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _db = applicationDbContext;
            _userManager = userManager;
        }
        [Breadcrumb("JobPosts", AreaName = "Employer")]
        public IActionResult Index()
        {
            /*            IEnumerable<JobPost> objJobPostList = _unitOfWork.JobPost.GetAll();
                        return View(objJobPostList);*/
            return View();
        }

        public JsonResult GetFaculties()
        {
            var faculties = _unitOfWork.Faculty.GetAll();

            return new JsonResult(faculties);
        }

        public JsonResult GetDepartments(int id)
        {
            var departments = _unitOfWork.Department.GetAll().Where(d => d.FacultyId == id);

            return new JsonResult(departments);
        }

        public JsonResult GetJobtype()
        {
            var jobtypes = _unitOfWork.JobType.GetAll();

            return new JsonResult(jobtypes);
        }

        public JsonResult GetWeekHour(int id)
        {
            var WeekHour = _unitOfWork.WeekHour.GetAll().Where(d => d.JobTypeId == id);

            return new JsonResult(WeekHour);
        }

        //GET
        [Breadcrumb("Update", AreaName = "JobPost")]
        public IActionResult Upsert(int? id)
        {
            //
            JobPostVM JobPostVM = new()
            {
                JobPost = new(),
                StatusList = _unitOfWork.Status.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            var EmployerId = _userManager.GetUserId(User);

            if (id == null || id == 0)
            {
                //create JobPost
                IEnumerable<Faculty> faculties = _db.Faculty;

                JobPostVM.FacultyList = faculties;

                IEnumerable<Department> departments = _db.Department;

                JobPostVM.DepartmentList = departments;

                IEnumerable<JobType> jobTypes = _db.JobType;
                JobPostVM.JobTypeList = jobTypes;

                IEnumerable<WeekHour> weekHour = _db.WeekHour;
                JobPostVM.WeekHourList = weekHour;

                JobPostVM.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == EmployerId);
                return View(JobPostVM);
            }
            else
            {
                //update JobPost
                JobPostVM.JobPost = _unitOfWork.JobPost.GetFirstOrDefault(u => u.Id == id);

                IEnumerable<Faculty> faculties = _db.Faculty;

                JobPostVM.FacultyList = faculties;

                IEnumerable<Department> departments = _db.Department.Where(d => d.FacultyId == JobPostVM.JobPost.FacultyId);

                JobPostVM.DepartmentList = departments;

                IEnumerable<JobType> jobTypes = _db.JobType;
                JobPostVM.JobTypeList = jobTypes;

                IEnumerable<WeekHour> weekHour = _db.WeekHour.Where(d => d.JobTypeId == JobPostVM.JobPost.JobTypeId);
                //IEnumerable<WeekHour> weekHour = _db.WeekHour;
                JobPostVM.WeekHourList = weekHour;

                JobPostVM.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == EmployerId);

                return View(JobPostVM);

            }

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(JobPostVM obj)
        {
            /*            if (obj.JobDescription == obj.KeyResponsibilities)
                        {
                            ModelState.AddModelError("JobDescription", "Description of job cannot exactly match the Key responsibilities.");
                        }*/

            var EmployerId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                if (obj.JobPost.Id == 0)
                {

                    obj.JobPost.ApplicationUserId = EmployerId;
                    _unitOfWork.JobPost.Add(obj.JobPost);
                }
                else
                {
                    //obj.JobPost.Status.Name = "Pending"; //every update to leave status=pending
                    _unitOfWork.JobPost.Update(obj.JobPost);
                }
                _unitOfWork.Save();
                TempData["success"] = "Job Post created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var EmployerId = _userManager.GetUserId(User);

            var JobPostList = _unitOfWork.JobPost.GetAll(includeProperties: "Faculty,Department,JobType,WeekHour,Status").Where(u => u.ApplicationUserId == EmployerId);
            //var JobPostList = _unitOfWork.JobPost.GetAll();
            return Json(new { data = JobPostList });
        }
        #endregion

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var JobPostFromDbFirst = _unitOfWork.JobPost.GetFirstOrDefault(u => u.Id == id);

            if (JobPostFromDbFirst == null)
            {
                return NotFound();
            }

            return View(JobPostFromDbFirst);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.JobPost.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.JobPost.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Job Post deleted successfully";
            return RedirectToAction("Index");

        }

    }
}
