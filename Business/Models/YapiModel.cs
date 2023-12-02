#nullable disable

using AppCore.Records.Bases;
using DataAccess.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
	public class YapiModel : RecordBase
	{
		#region entity'den gelen özellikler

		[Required]
		[StringLength(50)]
		[DisplayName("YAPI ADI")]
		public string Adi { get; set; }
		[DisplayName("YAPIM YILI")]
		public DateTime? YapimYili { get; set; }
		[DisplayName("BULUNDUĞU ÜLKE")]
		public string BulunduğuUlke { get; set; }
		[DisplayName("MİMAR")]
		public Mimar Mimar { get; set; }
		[DisplayName("MİMAR")]
		public int MimarId { get; set; }
		public List<YapiTur> YapiTurler { get; set; }

		#endregion

		#region Gösterim için kullanılacak özellikler

		[DisplayName("YAPIM YILI")]
		public string YapimYiliGosterim { get; set; }

		[DisplayName("MİMAR")]
		public string MimarGosterim { get; set; }
        [DisplayName("TÜRLERİ")]
        public List<TurModel> Turs { get; set; }
		[DisplayName("TÜRLERİ")]
		[Required]
		public List<int> TurIds { get; set; }
        public YapiDetayModel YapiDetayGosterim { get; set; }

		#endregion

		#region binary data
		public byte[] Image { get; set; }

		[StringLength(5)]
		public string ImageExtension { get; set; }

		[DisplayName("RESİM")]
        public string ImgSrcDisplay { get; set; }
        #endregion

    }
}
