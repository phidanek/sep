using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SEP.DataAccess.Repository.IRepository;
using SEP.Models;
using SEP.Models.ViewModels;
using SEP.Utility;
using SEPWebApp.Areas.Home.Controllers;
using SmartBreadcrumbs.Attributes;
using System.Data;

namespace SEPWebApp.Controllers
{
    [Area("Employer")]
    [Authorize(Roles = SD.Role_Employer)]
    //[Authorize(Roles = SD.Role_Approver)]
    public class EmployerController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public EmployerController(IUnitOfWork unitOfWork, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //[DefaultBreadcrumb("Home",AreaName ="Employer")]
        [Breadcrumb("Index", AreaName ="Employer")]
        public IActionResult Index()
        {
            var EmployerId = _userManager.GetUserId(User);
            Employer employer = _unitOfWork.Employer.GetFirstOrDefault(e => e.ApplicationUserId == EmployerId);

            if (employer == null)
            {
                return RedirectToAction("Upsert", "Employer", new { area = "Employer" });
            }
            return View();
        }

        //GET
        [Breadcrumb("Update", AreaName = "Employer")]
        //[Breadcrumb("Update", AreaName = "Home")]
        public IActionResult Upsert()
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
                }),

            };
            var EmployerId = _userManager.GetUserId(User);
            ApplicationUser user = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == EmployerId);
            Employer employer = _unitOfWork.Employer.GetFirstOrDefault(e => e.ApplicationUserId == EmployerId);



            //create Employer
            if (employer == null)
            {
                EmployerVM.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == EmployerId);
                return View(EmployerVM);
            }
            else
            {
                //update Employer
                EmployerVM.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == EmployerId);
                EmployerVM.Employer = _unitOfWork.Employer.GetFirstOrDefault(u => u.ApplicationUserId == EmployerId);
                return View(EmployerVM);

            }

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(EmployerVM obj)
        {

            var EmployerId = _userManager.GetUserId(User);
            ApplicationUser user = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == EmployerId);
            Employer employer = _unitOfWork.Employer.GetFirstOrDefault(e => e.ApplicationUserId == EmployerId);


            if (ModelState.IsValid)
            {

                if (employer == null)
                {
                    obj.Employer.ApplicationUserId = EmployerId;
                    _unitOfWork.Employer.Add(obj.Employer);
                }
                else
                {
                    employer.ApplicationUser = user;
                    employer.JobTitle = obj.Employer.JobTitle;
                    employer.CompanyRegNo = obj.Employer.CompanyRegNo;
                    employer.BusinessName = obj.Employer.BusinessName;
                    employer.TradingName = obj.Employer.TradingName;
                    employer.BusinessTypeId = obj.Employer.BusinessTypeId;
                    employer.RegisteredAddress = obj.Employer.RegisteredAddress;
                    employer.StatusId = obj.Employer.StatusId;

                    _unitOfWork.Employer.Update(employer);
                }


                _unitOfWork.Save();
                TempData["success"] = "Profile updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }


    }
}