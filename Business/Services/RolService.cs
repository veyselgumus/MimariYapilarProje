using AppCore.Business.Services.Bases;
using AppCore.DataAccess.EntityFramework.Bases;
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
    public interface IRolService : IService<RolModel>
    {

    }


    public class RolService : IRolService
    {
        private readonly RepoBase<Rol> _rolService;

        public RolService(RepoBase<Rol> rolService)
        {
            _rolService = rolService;
        }

        public Result Add(RolModel model)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _rolService.Dispose();
            GC.SuppressFinalize(this);
        }

        public IQueryable<RolModel> Query()
        {
            return _rolService.Query().Select(r => new RolModel
            {
                Adi = r.Adi,
                Id = r.Id,
                Guid = r.Guid,
            });
        }

        public Result Update(RolModel model)
        {
            throw new NotImplementedException();
        }
    }
}
