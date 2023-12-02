#nullable disable

using Business.Models.Report;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Areas.Report.Models
{
    public class HomeIndexViewModel
    {
        public FilterModel Filter { get; set; }

        public IEnumerable<ReportItemModel> Report { get; set; }
        public SelectList MimarSelectList { get; set; }
        public MultiSelectList TurSelectList { get; set; }

    }
}
