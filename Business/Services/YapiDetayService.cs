using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using System.Globalization;

namespace Business.Services
{
	public interface IYapiDetayService : IService<YapiDetayModel>
	{

	}

	public class YapiDetayService : IYapiDetayService
	{
		private readonly RepoBase<YapiDetay> _yapiDetayRepo;

		public YapiDetayService(RepoBase<YapiDetay> yapiDetayRepo)
		{
			_yapiDetayRepo = yapiDetayRepo;
		}

		public Result Add(YapiDetayModel model)
		{
			throw new NotImplementedException();
		}

		public Result Delete(int id)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			_yapiDetayRepo.Dispose();
			GC.SuppressFinalize(this);
		}

		public IQueryable<YapiDetayModel> Query()
		{
			return _yapiDetayRepo.Query().Select(yd => new YapiDetayModel()
			{
				Guid = yd.Guid,
				Id = yd.Id,
				InsaatAlani = yd.InsaatAlani,
				Konsepti = yd.Konsepti,
				TasiyiciSistem = yd.TasiyiciSistem,
				YapiId = yd.YapiId,
				InsaatAlaniGosterim = yd.InsaatAlani.HasValue? yd.InsaatAlani.Value.ToString("NO", new CultureInfo("en-US")) + " m²" : "",
				TasiyiciSistemGosterim = yd.TasiyiciSistem.ToString()
			});
		}

		public Result Update(YapiDetayModel model)
		{
			throw new NotImplementedException();
		}
	}
}
