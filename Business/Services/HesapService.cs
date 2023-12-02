using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
	public interface IHesapService
	{
		public Result Login(HesapLoginModel model, KullaniciModel userLoginModel);
	}

	public class HesapService : IHesapService
	{

		private readonly IKullaniciService _kullaniciService;

		public HesapService(IKullaniciService kullaniciService)
		{
			_kullaniciService = kullaniciService;
		}

		public Result Login(HesapLoginModel model, KullaniciModel userLoginModel)
		{
			KullaniciModel kullanici = _kullaniciService.Query().SingleOrDefault(k => k.UserName == model.UserName && k.Sifre == model.Sifre && k.AktifMi);

			if (kullanici == null)
			{
				return new ErrorResult("Kullanıcı Adı veya Şifre Hatalı!");
			}
			userLoginModel.UserName = kullanici.UserName;
			userLoginModel.RolModel = new RolModel()
			{
				Adi = kullanici.RolModel.Adi
			};
			userLoginModel.Id = kullanici.Id;
			return new SuccessResult();

		}
	}
}
