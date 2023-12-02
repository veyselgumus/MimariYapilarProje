using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Enums;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
	public class DbController : Controller
	{
		private readonly MimariYapilarContext _db;

		public DbController(MimariYapilarContext db)
		{
			_db = db;
		}

		public IActionResult Seed()
		{
			#region Delete

			var yapiTurler = _db.YapiTurler.ToList();
			_db.YapiTurler.RemoveRange(yapiTurler);

			var turler = _db.Turler.ToList();
			_db.Turler.RemoveRange(turler);

			var yapiDetaylar = _db.YapiDetaylar.ToList();
			_db.YapiDetaylar.RemoveRange(yapiDetaylar);

			var yapilar = _db.Yapilar.ToList();
			_db.Yapilar.RemoveRange(yapilar);

			var mimarlar = _db.Mimarlar.ToList();
			_db.Mimarlar.RemoveRange(mimarlar);

			_db.SaveChanges();

			#endregion

			#region Insert

			_db.Mimarlar.Add(new Mimar
			{
				Adi = "Tadao",
				DogumTarihi = new DateTime(1941, 09, 13),
				Soyadi = "Ando",
				YasiyorMu = true
			});

			_db.Mimarlar.Add(new Mimar
			{
				Adi = "Zaha",
				DogumTarihi = new DateTime(1950, 10, 31),
				Soyadi = "Hadid",
				YasiyorMu = false
			});

			_db.Mimarlar.Add(new Mimar
			{
				Adi = "Emre",
				DogumTarihi = new DateTime(1963, 01, 01),
				Soyadi = "Arolat",
				YasiyorMu = true
			});

			_db.Turler.Add(new Tur
			{
				Adi = "Konut"
			});

			_db.Turler.Add(new Tur
			{
				Adi = "Ticari&Ofisler"
			});

			_db.Turler.Add(new Tur
			{
				Adi = "Eğitim Mimarisi"
			});

			_db.Turler.Add(new Tur
			{
				Adi = "Müzeler ve Sergiler"
			});

			_db.Turler.Add(new Tur
			{
				Adi = "Kültürel Mimarlık"
			});

			_db.Turler.Add(new Tur
			{
				Adi = "Tadilat"
			});

			_db.Turler.Add(new Tur
			{
				Adi = "Dini Yapılar"
			});

			_db.SaveChanges();

			_db.Yapilar.Add(new Yapi
			{
				Adi = "Lighthouse Church",
				BulunduğuUlke = "Japonya",
				YapimYili = new DateTime(1989, 01, 01),
				MimarId = _db.Mimarlar.SingleOrDefault(m => m.Adi == "Tadao").Id,
				YapiTurler = new List<YapiTur>()
				{
					new YapiTur
					{
						TurId=_db.Turler.SingleOrDefault(t=>t.Adi=="Müzeler ve Sergiler").Id
					},

					new YapiTur
					{
						TurId=_db.Turler.SingleOrDefault(t=>t.Adi=="Kültürel Mimarlık").Id
					}
				},
				YapiDetay = new YapiDetay
				{
					TasiyiciSistem = TasiyiciSistem.Betonarme,
					InsaatAlani = 3500,
					Konsepti = "Işık Kilisesi, Japonya Osaka’da İbaraki kentinde bulunuyor. Tadao Ando’nun modern mimarlığa imzalarından biri " +
					"olarak değerlendirilen kilise, Ando’nun doğa ve mimarlık arasında felsefik çalışmalarını kucaklayan bir yaklaşımla tasarlanmış." +
					" Bunu ışıkla ve yeni mekansal algılarla tanımlıyor yapı. Işık Kilisesi, Ibaraki’deki eski bir hıristiyan kompleksinin yenilenmesiyle" +
					" ortaya çıkmış. Alandaki yenilemenin ilk aşaması olan Tadao Ando imzalı kilise 1999’da tamamlanmış.",
				}
			});

			_db.Yapilar.Add(new Yapi
			{
				Adi = "Haydar Aliyev Center",
				BulunduğuUlke = "Azerbaycan",
				YapimYili = new DateTime(2013, 01, 01),
				MimarId = _db.Mimarlar.SingleOrDefault(m => m.Adi == "Zaha").Id,
				YapiTurler = new List<YapiTur>()
				{
					new YapiTur
					{
						TurId=_db.Turler.SingleOrDefault(t=>t.Adi=="Müzeler ve Sergiler").Id
					},

					new YapiTur
					{
						TurId=_db.Turler.SingleOrDefault(t=>t.Adi=="Kültürel Mimarlık").Id
					}
				},
				YapiDetay = new YapiDetay
				{
					TasiyiciSistem = TasiyiciSistem.Betonarme,
					InsaatAlani = 101000,
					Konsepti = "Azerbaycan’ın en önemli yapılarından biri olarak gösterilen Haydar Aliyev Kültür Merkezi, 2007’de düzenlenen bir " +
					"yarışmanın sonucunda Zaha Hadid tarafından tasarlandı. Azerbaycan Cumhuriyeti tarafından yaptırılan bu eser, modern, sıradışı " +
					"ve futuristik mimarisi ile Azeri toplumunun geleceğe yönelik yaklaşımının da bir sembolü olarak görülmektedir."
				}
			});

			_db.Yapilar.Add(new Yapi
			{
				Adi = "Sancaklar Camii",
				BulunduğuUlke = "Türkiye",
				YapimYili = new DateTime(2013, 01, 01),
				MimarId = _db.Mimarlar.SingleOrDefault(m => m.Adi == "Emre").Id,
				YapiTurler = new List<YapiTur>()
				{
					new YapiTur
					{
						TurId=_db.Turler.SingleOrDefault(t=>t.Adi=="Dini Yapılar").Id
					},

					new YapiTur
					{
						TurId=_db.Turler.SingleOrDefault(t=>t.Adi=="Kültürel Mimarlık").Id
					}
				},
				YapiDetay = new YapiDetay
				{
					TasiyiciSistem = TasiyiciSistem.Betonarme,
					InsaatAlani = 21000,
					Konsepti = "Emre Arolat tarafından Büyükçekmece’de, şehrin dışında sayılabilecek bir bölgeye tasarlanan Sancaklar Camii," +
					" modern islami mimarinin en önemli temsilcilerinden biri olarak dikkat çekmektedir. Osmanlı mimarisinin, günümüz inşaat" +
					" teknikleri ve teknolojik gelişmeler sonucunda çağ dışı kalması sebebiyle, bu kalıpların dışına çıkmak isteyen Emre Arolat," +
					" alışılmışın dışına çıkarak İstanbul’a sade ve etkileyici bir camii kazandırmıştır."
				}
			});

			_db.Yapilar.Add(new Yapi
			{
				Adi = "Hyogo Prefectural Museum of Art",
				BulunduğuUlke = "Japonya",
				YapimYili = new DateTime(2001, 01, 01),
				MimarId = _db.Mimarlar.SingleOrDefault(m => m.Adi == "Tadao").Id,
				YapiTurler = new List<YapiTur>()
				{
					new YapiTur
					{
						TurId=_db.Turler.SingleOrDefault(t=>t.Adi=="Müzeler ve Sergiler").Id
					},

					new YapiTur
					{
						TurId=_db.Turler.SingleOrDefault(t=>t.Adi=="Kültürel Mimarlık").Id
					}
				},
				YapiDetay = new YapiDetay
				{
					TasiyiciSistem = TasiyiciSistem.Betonarme,
					InsaatAlani = 45000,
					Konsepti = "Hyogo Valiliği Sanat Müzesi, Japonya'nın Hyogo Eyaleti, Kobe, Nada-ku'da özel olarak inşa edilmiş bir belediye" +
					" sanat galerisidir. Tadao Ando tarafından tasarlandı ve 2002 yılında açıldı.Müzenin ana koleksiyonları yabancı ve Japon " +
					"heykelleri, yabancı ve Japon baskıları, Hyogo Eyaleti ile ilişkili Batı tarzı ve Japon tarzı tablolar, modern çağdaki Japon" +
					" harika eserleri ve çağdaş sanattır."
				}
			});
			_db.SaveChanges();

			return View();

			#endregion
		}
	}
}
