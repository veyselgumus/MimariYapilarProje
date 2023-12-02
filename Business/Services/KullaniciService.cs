using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
	public interface IKullaniciService : IService<KullaniciModel>
	{

	}

	public class KullaniciService : IKullaniciService
	{
		private readonly RepoBase<Kullanici> _kullaniciRepo;
		private readonly RepoBase<Rol> _rolRepo;

		public KullaniciService(RepoBase<Kullanici> kullaniciRepo, RepoBase<Rol> rolRepo)
		{
			_kullaniciRepo = kullaniciRepo;
			_rolRepo = rolRepo;
		}

		public Result Add(KullaniciModel model)
		{

			if (model != null)
			{
				var roller=_rolRepo.Query().ToList();
				if (_kullaniciRepo.Query().SingleOrDefault(k => k.UserName == model.UserName && k.Id != model.Id) == null)
				{
					Kullanici kullanici = new Kullanici()
					{
						AktifMi = true,
						UserName = model.UserName,
						Email= model.Email,
						Guid=Guid.NewGuid().ToString(),
						Rol=roller.SingleOrDefault(r=>r.Adi=="Kullanici"),
						Sifre = model.Sifre,
						Telefon= model.Telefon
					};
					_kullaniciRepo.Add(kullanici);
					return new SuccessResult("Added is Success");
				}

			}
			return new ErrorResult("There is a record with this name");
		}

		public Result Delete(int id)
		{
			Kullanici kullanici= _kullaniciRepo.Query().SingleOrDefault(k => k.Id == id);
			if(kullanici == null)
			{
				return new ErrorResult("No record found with this name");
			}
			else
			{
				_kullaniciRepo.Delete(k => k.Id == id);
				return new SuccessResult("Deleted is Success");
			}
		}

		public void Dispose()
		{
			_kullaniciRepo.Dispose();
			GC.SuppressFinalize(this);
		}

		public IQueryable<KullaniciModel> Query()
		{
			return _kullaniciRepo.Query().Select(k => new KullaniciModel
			{
				UserName = k.UserName,
				Telefon=k.Telefon,
				Sifre = k.Sifre,
				Id = k.Id,
				Email = k.Email,
				RolId = k.RolId,
				RolModel=new RolModel()
				{
					Adi=k.Rol.Adi,
					Id=k.Rol.Id,
					Guid=k.Rol.Guid
				},
				AktifMi=k.AktifMi,
				AktifMiGosterim= k.AktifMi==true ? "EVET":"HAYIR",
                Guid = k.Guid
			});
		}

		public Result Update(KullaniciModel model)
		{
			if(_kullaniciRepo.Query().SingleOrDefault(k=>k.Id!=model.Id && k.UserName==model.UserName)==null)
			{
				Kullanici kullanici = _kullaniciRepo.Query().SingleOrDefault(k => k.Id == model.Id);
				kullanici.AktifMi= model.AktifMi;
				kullanici.Sifre= model.Sifre;
				kullanici.UserName=model.UserName;
				kullanici.RolId= model.RolId;
				kullanici.Email= model.Email;
				kullanici.Telefon=model.Telefon;
				_kullaniciRepo.Update(kullanici);
				return new SuccessResult("Updated is Success");
            }
            return new ErrorResult("There is a record with this name");
        }
	}
}
