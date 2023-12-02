#nullable disable

using System.ComponentModel;

namespace Business.Models.Report
{
    public class FilterModel
    {
        [DisplayName("YAPI ADI")]
        public string YapiAdi { get; set; }

		[DisplayName("MİMAR ADI")]
		public string MimarAdi { get; set; }
		[DisplayName("TÜR ADI")]
		public string TurAdi { get; set; }
        public int? MimarId { get; set; }
        public List<int> TurId { get; set; }
        [DisplayName("YAPIM YILI")]
        public DateTime? YapiYapimYiliBaşlangıc { get; set; }
        public DateTime? YapiYapimYiliBitis { get; set; }
    }
}
