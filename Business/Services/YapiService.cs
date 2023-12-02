using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Records.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using DataAccess.Enums;
using System.Globalization;

namespace Business.Services
{
    public interface IYapiService : IService<YapiModel>
    {
        public Result ResimSil(int id);
    }

    public class YapiService : IYapiService
    {
        private readonly RepoBase<Yapi> _yapiRepo;
        public TasiyiciSistem tasiyiciSistem = TasiyiciSistem.Betonarme;


        public YapiService(RepoBase<Yapi> yapiRepo)
        {
            _yapiRepo = yapiRepo;
        }

        public IQueryable<YapiModel> Query()
        {
            var querymodel = _yapiRepo.Query().Select(y => new YapiModel()
            {
                Adi = y.Adi,
                TurIds = y.YapiTurler.Select(yt => yt.TurId).ToList(),
                BulunduğuUlke = y.BulunduğuUlke,
                Guid = y.Guid,
                Id = y.Id,
                Mimar = y.Mimar,
                MimarId = y.MimarId,
                YapiDetayGosterim = new YapiDetayModel()
                {
                    InsaatAlani = y.YapiDetay.InsaatAlani,
                    Id = y.YapiDetay.Id,
                    Guid = y.YapiDetay.Guid,
                    InsaatAlaniGosterim = y.YapiDetay.InsaatAlani == null ? "0" + " m²" : y.YapiDetay.InsaatAlani.ToString() + " m²",
                    Konsepti = y.YapiDetay.Konsepti,
                    TasiyiciSistem = y.YapiDetay.TasiyiciSistem,
                    TasiyiciSistemGosterim = y.YapiDetay.TasiyiciSistem.ToString(),
                    YapiId = y.YapiDetay.YapiId
                },
                YapimYili = y.YapimYili,
                YapiTurler = y.YapiTurler,
                MimarGosterim = y.Mimar.Adi + " " + y.Mimar.Soyadi,
                YapimYiliGosterim = y.YapimYili.HasValue ? y.YapimYili.Value.ToString("yyyy/MM/dd", new CultureInfo("en-US")) : "",
                Turs = y.YapiTurler.Select(t => new TurModel()
                {
                    Adi = t.Tur.Adi,
                    Guid = t.Tur.Guid,
                    Id = t.Tur.Id
                }).ToList(),
                Image=y.Image,
                ImageExtension=y.ImageExtension,
                ImgSrcDisplay = y.Image != null ?
                    (
                        y.ImageExtension == ".jpg" || y.ImageExtension == ".jpeg"?
                        "data:image/jpeg;base64,"
                        : "data:image/png;base64,"
                    )+Convert.ToBase64String(y.Image) :null


            });
            return querymodel;
        }

        public Result Add(YapiModel model)
        {
            if (model != null)
            {
                if (_yapiRepo.Query().SingleOrDefault(y => y.Adi == model.Adi) == null)
                {
                    Yapi yapi = new Yapi()
                    {
                        Adi = model.Adi,
                        Guid = Guid.NewGuid().ToString(),
                        BulunduğuUlke = model.BulunduğuUlke,
                        Mimar = model.Mimar,
                        MimarId = model.MimarId,
                        YapimYili = model.YapimYili,
                        YapiDetay = new YapiDetay()
                        {
                            Guid = Guid.NewGuid().ToString(),
                            InsaatAlani = model.YapiDetayGosterim.InsaatAlani,
                            Konsepti = model.YapiDetayGosterim.Konsepti,
                            TasiyiciSistem = model.YapiDetayGosterim.TasiyiciSistem
                        },
                        YapiTurler = model.TurIds.Select(yt => new YapiTur()
                        {
                            TurId = yt

                        }).ToList(),
                        Image=model.Image,
                        ImageExtension=model.ImageExtension,
                    };
                    _yapiRepo.Add(yapi);
                    _yapiRepo.Save();
                    return new SuccessResult("Added is Success");
                }
            }
            return new ErrorResult("There is a record with this name");

        }

        public Result Delete(int id)
        {
            _yapiRepo.Delete<YapiTur>(y => y.YapiId == id);
            _yapiRepo.Delete(y => y.Id == id);
            return new SuccessResult();
        }


        public Result Update(YapiModel model)
        {
            if (_yapiRepo.Query().SingleOrDefault(y => y.Id == model.Id) == null)
            {
                return new ErrorResult("Can't Found Building!");
            }
            if (_yapiRepo.Query().SingleOrDefault(y => y.Adi == model.Adi && y.Id != model.Id) == null)
            {
                _yapiRepo.Delete<YapiTur>(y => y.YapiId == model.Id);
                _yapiRepo.Delete<YapiDetay>(y => y.YapiId == model.Id);
                Yapi yapi = _yapiRepo.Query().SingleOrDefault(y => y.Id == model.Id);
                yapi.Adi = model.Adi;
                yapi.BulunduğuUlke = model.BulunduğuUlke;
                yapi.Mimar = model.Mimar;
                yapi.MimarId = model.MimarId;
                if(model.Image is not null)
                {
                    yapi.Image = model.Image;
                    yapi.ImageExtension = model.ImageExtension;
                }
                yapi.YapimYili = model.YapimYili;
                yapi.YapiDetay = new YapiDetay()
                {
                    Guid = Guid.NewGuid().ToString(),
                    InsaatAlani = model.YapiDetayGosterim.InsaatAlani,
                    Konsepti = model.YapiDetayGosterim.Konsepti,
                    TasiyiciSistem = model.YapiDetayGosterim.TasiyiciSistem
                };
                yapi.YapiTurler = model.TurIds.Select(yt => new YapiTur()
                {
                    TurId = yt

                }).ToList();
                _yapiRepo.Update(yapi);
                return new SuccessResult("Update is Success");
            }
            return new ErrorResult("There is a record with this name");


        }

        public void Dispose()
        {
            _yapiRepo.Dispose();
            GC.SuppressFinalize(this);
        }

		public Result ResimSil(int id)
		{
			if (_yapiRepo.Query().SingleOrDefault(y =>  y.Id == id) != null)
            {
                Yapi yapi = _yapiRepo.Query().SingleOrDefault(y=>y.Id==id);
                yapi.Image = null;
                yapi.ImageExtension = null;
                _yapiRepo.Update(yapi);
                return new SuccessResult("Picture is Deleted");
            }
            return new ErrorResult("Picture is not Deleted");
		}
	}
}
