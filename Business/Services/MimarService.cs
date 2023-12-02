using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IMimarService : IService<MimarModel>
    {
        public Result ResimSil(int id);

	}
    public class MimarService : IMimarService
    {
        private readonly RepoBase<Mimar> _mimarRepo;

        public MimarService(RepoBase<Mimar> mimarRepo)
        {
            _mimarRepo = mimarRepo;
        }

        public Result Add(MimarModel model)
        {
            if(model!=null)
            {
                if(_mimarRepo.Query().SingleOrDefault(m=>m.Adi==model.Adi&&m.Id!=model.Id)==null)
                {
					Mimar mimar = new Mimar()
					{
						Adi = model.Adi,
						DogumTarihi = model.DogumTarihi,
						Guid = Guid.NewGuid().ToString(),
						Soyadi = model.Soyadi,
						YasiyorMu = model.YasiyorMu,
                        Image=model.Image,
                        ImageExtension= model.ImageExtension,
					};
					_mimarRepo.Add(mimar);
					return new SuccessResult("Added is Success");
				}
                
            }
            return new ErrorResult("There is a record with this name");
        }

        public Result Delete(int id)
        {
            Mimar mimar=_mimarRepo.Query().SingleOrDefault(m=>m.Id==id);
            if(mimar!=null)
            {
                _mimarRepo.Delete(m => m.Id == id);
                return new SuccessResult("Deleted is Success");
            }
            return new ErrorResult("Deleted is Failed");
        }

        public void Dispose()
        {
            _mimarRepo.Dispose();
            GC.SuppressFinalize(this);
        }

        public IQueryable<MimarModel> Query()
        {
            return _mimarRepo.Query().Select(m => new MimarModel()
            {
                Adi=m.Adi,
                AdSoyadGosterim=m.Adi+" "+m.Soyadi,
                Soyadi=m.Soyadi,
                DogumTarihi=m.DogumTarihi,
                DogumTarihiGosterim = m.DogumTarihi.HasValue?m.DogumTarihi.Value.ToString("yyyy/MM/dd",new CultureInfo("en-US")):"",
                Id=m.Id,
                Guid=m.Guid,
                YasiyorMu= m.YasiyorMu,
                Image=m.Image,
                ImageExtension=m.ImageExtension,
                YasıyorMuGosterim=m.YasiyorMu ==true?"EVET":"HAYIR",
                Yapilar=m.Yapilari.Select(m => new YapiModel()
                {
                    Id=m.Id,
                    Adi=m.Adi,
                    BulunduğuUlke=m.BulunduğuUlke,
                    Guid=m.Guid,
                    Mimar = m.Mimar,
                    MimarId = m.MimarId,
                    YapimYili = m.YapimYili,
                }).ToList(),
                ImgSrcDisplay= m.Image != null ?
					(
						m.ImageExtension == ".jpg" || m.ImageExtension == ".jpeg" ?
						"data:image/jpeg;base64,"
						: "data:image/png;base64,"
					) + Convert.ToBase64String(m.Image) : null

			});
        }

        public Result Update(MimarModel model)
        {
            if(_mimarRepo.Query().SingleOrDefault(m=>m.Adi == model.Adi && m.Id!=model.Id)==null)
            {
                Mimar mimar = _mimarRepo.Query().SingleOrDefault(m => m.Id == model.Id);
                mimar.Adi= model.Adi;
                mimar.DogumTarihi= model.DogumTarihi;
                mimar.YasiyorMu= model.YasiyorMu;
                mimar.Soyadi= model.Soyadi;
                if(model.Image is not null)
                {
                    mimar.ImageExtension = model.ImageExtension;
                    mimar.Image = model.Image;
                }
               
                _mimarRepo.Update(mimar);
                return new SuccessResult("Updated is Success");
            };
            return new ErrorResult("There is a record with this name");
        }
		public Result ResimSil(int id)
		{
			if (_mimarRepo.Query().SingleOrDefault(y => y.Id == id) != null)
			{
				Mimar mimar = _mimarRepo.Query().SingleOrDefault(y => y.Id == id);
				mimar.Image = null;
				mimar.ImageExtension = null;
				_mimarRepo.Update(mimar);
				return new SuccessResult("Picture is Deleted");
			}
			return new ErrorResult("Picture is not Deleted");
		}
	}
}
