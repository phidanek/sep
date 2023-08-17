using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SEP.DataAccess;
using SEP.DataAccess.Repository.IRepository;
using SEP.Models.ViewModels;
using SEP.Utility;
using SmartBreadcrumbs.Attributes;

namespace SEPWebApp.Areas.Approver.Controllers
{
    [Area("Approver")]
    [Authorize(Roles = SD.Role_Approver)]
    //[Authorize(Roles = SD.Role_Approver)]
    public class EmployerController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;

        public EmployerController(IUnitOfWork unitOfWork, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ApplicationDbContext applicationDbContext)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _userManager = userManager;
            _db = applicationDbContext;

        }

        [Breadcrumb("Review Employers", AreaName = "Approver")]
        public IActionResult Index()
        {
            return View();
            /*            var employerVM = new EmployerVM()
                        {
                            ApplicationUserList = _unitOfWork.ApplicationUser.GetAll(),
                            EmployerList = _unitOfWork.Employer.GetAll(),
                            StatusList = _unitOfWork.Status.GetAll().Select(i => new SelectListItem
                            {
                                Text = i.Name,
                                Value = i.Id.ToString()
                            }),
                        };

                        return View(employerVM);*/

        }

        //GET
        [Breadcrumb("Update", AreaName = "Approver")]
        public IActionResult Upsert(int? id)
        {

            EmployerVM EmployerVM = new()
            {
                ApplicationUser = new(),
                Employer = new(),
                BusinessTypeList = _unitOfWork.BusinessType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                StatusList = _unitOfWork.Status.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })

            };


            //update Employer
            // EmployerVM.ApplicationUser = _db.ApplicationUser.Where(u => u.Id == EmployerVM.Employer.ApplicationUserId);
            EmployerVM.Employer = _unitOfWork.Employer.GetFirstOrDefault(u => u.Id == id);
            EmployerVM.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == EmployerVM.Employer.ApplicationUserId);
            //EmployerVM.Employer = _unitOfWork.Employer.GetFirstOrDefault(u => u.ApplicationUserId == EmployerVM.ApplicationUser.Id);
            return View(EmployerVM);


        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(EmployerVM obj)
        {
            //EmployerVM.employer = _unitOfWork.Employer.GetFirstOrDefault(u => u.Id == obj.Employer.Id);

            if (ModelState.IsValid)
            {
                if (obj.Employer.Id == 0)
                {
                    _unitOfWork.Employer.Add(obj.Employer);
                }
                else
                {
                    _unitOfWork.Employer.Update(obj.Employer);
                }
                _unitOfWork.Save();
                TempData["success"] = "Employer Profile status updated";
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var EmployerList = _unitOfWork.Employer.GetAll(includeProperties: "ApplicationUser,Status");
            /*            var UserList = _unitOfWork.ApplicationUser.GetAll();
                        var combinedList = _unitOfWork.Concat(EmployerList, UserList);*/
            return Json(new { data = EmployerList });
        }
        #endregion

    }
}