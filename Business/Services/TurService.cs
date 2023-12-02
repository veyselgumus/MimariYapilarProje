using AppCore.Business.Services.Bases;
using AppCore.DataAccess.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Azure.Core;
using Business.Models;
using DataAccess.Entities;
using System.Net.Http.Headers;

namespace Business.Services
{
    public interface ITurService : IService<TurModel>
    {

    }

    public class TurService : ITurService
    {
        private readonly RepoBase<Tur> _turRepo;

        public TurService(RepoBase<Tur> turRepo)
        {
            _turRepo = turRepo;
        }

        public Result Add(TurModel model)
        {
            if (model !=null && _turRepo.Query().SingleOrDefault(t => t.Adi == model.Adi) == null)
            {
                Tur tur = new Tur()
                {
                    Adi=model.Adi,
                    Guid=Guid.NewGuid().ToString(),
                };
                _turRepo.Add(tur);
                return new SuccessResult("Added is Success");

            }
            return new ErrorResult("There is a record with this name");
        }

        public Result Delete(int id)
        {
            Tur tur=_turRepo.Query().SingleOrDefault(t=>t.Id==id);
            if (tur!=null)
            {
                _turRepo.Delete(t => t.Id == id);
                return new SuccessResult("Deleted is Success");
            }
            return new ErrorResult("Deleted is Failed");
        }

        public void Dispose()
        {
            _turRepo.Dispose();
            GC.SuppressFinalize(this);
        }

        public IQueryable<TurModel> Query()
        {
            return _turRepo.Query().Select(t => new TurModel()
            {
                Adi=t.Adi,
                Guid=t.Guid,
                Id=t.Id,
                Yapilar=t.YapiTurler.Select(yt=>new YapiModel()
                {
                    Adi=yt.Yapi.Adi,
                    BulunduğuUlke=yt.Yapi.BulunduğuUlke,
                    Guid=yt.Yapi.Guid,
                    Id = yt.Yapi.Id,
                    MimarId = yt.Yapi.MimarId,
                    YapimYili = yt.Yapi.YapimYili,
                }).ToList()
            });
        }

        public Result Update(TurModel model)
        {
            if(_turRepo.Query().SingleOrDefault(t=>t.Adi==model.Adi&&t.Id!=model.Id)==null)
            {
				Tur tur = _turRepo.Query().SingleOrDefault(t => t.Id == model.Id);
				if (tur != null)
				{
					tur.Adi = model.Adi;
					_turRepo.Update(tur);
					return new SuccessResult("Updated is Success");

				}
			}
            return new ErrorResult("There is a record with this name");
        }
    }
}
