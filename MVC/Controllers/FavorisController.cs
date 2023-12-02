using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Newtonsoft.Json;
using System.Drawing.Text;
using System.Security.Claims;

namespace MVC.Controllers
{
	[Authorize]
	public class FavorisController : Controller
	{
		private readonly IYapiService _yapiService;

		const string SESSIONKEY = "Favoriler";

		int _kullaniciId;

		public FavorisController(IYapiService yapiService)
		{
			_yapiService = yapiService;
		}

		public IActionResult FavorileriGetir()
		{
			_kullaniciId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);
			var favoriler = SessionGetir(_kullaniciId);
			return View("Favoriler",favoriler);
		}


		public IActionResult FavoriEkle(int YapiId)
		{
			_kullaniciId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);
			var favoriListesi = SessionGetir(_kullaniciId);
			var yapi = _yapiService.Query().SingleOrDefault(y => y.Id == YapiId);

			if (favoriListesi.Any(f=>f.KullaniciId==_kullaniciId && f.YapiId==YapiId))
			{
				TempData["Message"] = $"{yapi.Adi} Zaten Favorilere Eklendi.";
			}
			else
			{
				var favoriYapi = new FavoriModel(YapiId, _kullaniciId, yapi.Adi, yapi.YapimYiliGosterim, yapi.BulunduğuUlke,yapi.ImgSrcDisplay);
				favoriListesi.Add(favoriYapi);
				SessionGuncelle(favoriListesi);

				TempData["Message"] = $"{yapi.Adi} Favorilere Eklendi.";
			}
			return RedirectToAction("Index", "Yapis");
		}

		public IActionResult FavoriSil(int yapiId,int kullaniciId)
		{
			_kullaniciId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);

			var favoriler = SessionGetir(kullaniciId);

			favoriler.RemoveAll(f=>f.KullaniciId==kullaniciId && f.YapiId==yapiId);

			SessionGuncelle(favoriler);
			return RedirectToAction(nameof(FavorileriGetir));
		}

        public IActionResult FavorileriTemizle()
		{
            _kullaniciId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);
            var favoriler = SessionGetir(_kullaniciId);
			favoriler.RemoveAll(f => f.KullaniciId == _kullaniciId);
            SessionGuncelle(favoriler);
            return RedirectToAction(nameof(FavorileriGetir));
        }


        private List<FavoriModel> SessionGetir(int kullaniciId)
		{
			List<FavoriModel> favoriModels = new List<FavoriModel>();

			var favoriJson = HttpContext.Session.GetString(SESSIONKEY);

			if(!string.IsNullOrWhiteSpace(favoriJson))
			{
				favoriModels=JsonConvert.DeserializeObject<List<FavoriModel>>(favoriJson);
				favoriModels = favoriModels.Where(f => f.KullaniciId == kullaniciId).ToList();
			}

			return favoriModels;
		}

		private void SessionGuncelle(List<FavoriModel> favoriModels)
		{
			var favoriJson=JsonConvert.SerializeObject(favoriModels);

			HttpContext.Session.SetString(SESSIONKEY, favoriJson);
		}
	}
}
