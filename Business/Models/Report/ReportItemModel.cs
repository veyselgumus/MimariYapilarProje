#nullable disable

using System.ComponentModel;

namespace Business.Models.Report
{
    public class ReportItemModel
    {
        [DisplayName("YAPI ADI")]
        public string YapiAdi { get; set; }
        [DisplayName("YAPI YAPIM YILI")]
        public string YapiYapimYili { get; set; }
        [DisplayName("YAPI BULUNDUĞU ÜLKE")]
        public string YapiBulunduguUlke { get; set; }
        [DisplayName("MİMAR ADI")]
        public string MimarAdiSoyadi { get; set; }
        [DisplayName("MİMAR YAŞIYOR MU?")]
        public string MimarYasiyorMu { get; set; }
        [DisplayName("TÜRÜ")]
        public string TurAdi { get; set; }
        [DisplayName("MİMAR YAŞIYOR MU?")]
        public bool? MimarYasiyorMuu { get; set; }

        public DateTime? YapimYili { get; set; }
        public int? MimarId { get; set; }
        public int? TurId { get; set; }



    }
}
