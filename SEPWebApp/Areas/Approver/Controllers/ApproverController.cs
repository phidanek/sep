using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEP.DataAccess.Repository.IRepository;
using SEP.Utility;
using SmartBreadcrumbs.Attributes;

namespace SEPWebApp.Areas.Approver.Controllers
{
    [Area("Approver")]
    [Authorize(Roles = SD.Role_Approver)]
    public class ApproverController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApproverController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //[DefaultBreadcrumb("Home", AreaName = "Approver")]
        [Breadcrumb("Index", AreaName = "Approver")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ReviewEmployers()
        {
            return View();
        }

        public IActionResult ReviewPosts()
        {
            return View();
        }
        [Breadcrumb("Stats", FromAction = "Index")]
        public IActionResult Stats()
        {
            return View();
        }

    }
}
