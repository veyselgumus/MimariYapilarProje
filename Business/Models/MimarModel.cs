#nullable disable

using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class MimarModel : RecordBase
    {
        #region entity özellikleri
        [Required(ErrorMessage = "{0} is Required")]
        [StringLength(50)]
        [DisplayName("ADI")]
        public string Adi { get; set; }

        [Required(ErrorMessage = "{0} is Required")]
        [StringLength(50)]
        [DisplayName("SOYADI")]
        public string Soyadi { get; set; }
        [DisplayName("DOĞUM TARİHİ")]
        public DateTime? DogumTarihi { get; set; }
		[DisplayName("YAŞIYOR MU?")]
		public bool? YasiyorMu { get; set; }
        #endregion
        #region gösterim için kullanılacaklar
        [DisplayName("ADI SOYADI")]
        public string AdSoyadGosterim { get; set; }
        [DisplayName("DOĞUM TARİHİ")]
        public string DogumTarihiGosterim { get; set; }
        [DisplayName("YAŞIYOR MU?")]
        public string YasıyorMuGosterim { get; set; }
		#endregion

		#region binary data

		[DisplayName("RESİM")]
		public string ImgSrcDisplay { get; set; }
		public byte[] Image { get; set; }

		[StringLength(5)]
		public string ImageExtension { get; set; }
		#endregion

		public List<YapiModel> Yapilar { get; set; }
    }
}
