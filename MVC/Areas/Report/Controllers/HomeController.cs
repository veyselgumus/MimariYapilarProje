using Business.Models.Report;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Areas.Report.Models;

namespace MVC.Areas.Report.Controllers
{
	[Area("Report")]
	public class HomeController : Controller
    {
        private readonly IReportService _reportservice;
        private readonly IMimarService _mimarService;
        private readonly ITurService _turService;

        public HomeController(IReportService reportservice, IMimarService mimarService, ITurService turService)
        {
            _reportservice = reportservice;
            _mimarService = mimarService;
            _turService = turService;
        }

        // GET: HomeController
        public ActionResult Index()
        {
            var query=_reportservice.RaporGetir(true);
            var viewModel = new HomeIndexViewModel()
            {
                Report = query,
                MimarSelectList =new SelectList(_mimarService.Query().ToList(),"Id", "AdSoyadGosterim"),
                TurSelectList=new MultiSelectList(_turService.Query().ToList(),"Id", "Adi")
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(FilterModel filter)
        {
            var query = _reportservice.RaporGetir(true,filter);
            return  PartialView("_Report",query);
        }
    }
}
