#nullable disable

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
	public class FavoriModel
	{
		public FavoriModel(int yapiId, int kullaniciId, string yapiAdi, string yapiYapimYili, string yapiBulunduğuUlke,string imgSrcDisplay)
		{
			YapiId = yapiId;
			KullaniciId = kullaniciId;
			YapiAdi = yapiAdi;
			YapiYapimYili = yapiYapimYili;
			YapiBulunduğuUlke = yapiBulunduğuUlke;
			ImgSrcDisplay = imgSrcDisplay;
		}

		public int YapiId { get; set; }
        public string ImgSrcDisplay { get; set; }
        public int KullaniciId { get; set; }

		[DisplayName("Yapı Adı")]
		public string YapiAdi { get; set; }
		[DisplayName("Yapım Yılı")]
		public string YapiYapimYili { get; set; }

		[DisplayName("Bulunduğu Ülke")]
		public string YapiBulunduğuUlke { get; set; }
	}
}
