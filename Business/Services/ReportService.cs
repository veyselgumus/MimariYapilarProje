using AppCore.DataAccess.EntityFramework.Bases;
using Business.Models.Report;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using System.Globalization;

namespace Business.Services
{
    public interface IReportService
    {
        public List<ReportItemModel> RaporGetir(bool useInnerJoın = true, FilterModel model = null);
    }

    public class ReportService : IReportService
    {
        private readonly RepoBase<Yapi> _yapiRepo;

        public ReportService(RepoBase<Yapi> yapiRepo)
        {
            _yapiRepo = yapiRepo;
        }

        public List<ReportItemModel> RaporGetir(bool useInnerJoın = true, FilterModel model = null)
        {
            #region Sorgu Oluşturma
            var yapiQuery = _yapiRepo.Query();
            var yapiTurQuery = _yapiRepo.Query<YapiTur>();
            var turQuery = _yapiRepo.Query<Tur>();
            var mimarQuery = _yapiRepo.Query<Mimar>();

            IQueryable<ReportItemModel> query;

            if (useInnerJoın==true)
            {
                query = from y in yapiQuery
                        join yt in yapiTurQuery
                        on y.Id equals yt.YapiId
                        join t in turQuery
                        on yt.TurId equals t.Id
                        join m in mimarQuery
                        on y.MimarId equals m.Id
                        select new ReportItemModel
                        {
                            MimarAdiSoyadi = m.Adi + " " + m.Soyadi,
                            MimarYasiyorMu = m.YasiyorMu == true ? "EVET" : "HAYIR",
                            TurAdi = t.Adi,
                            YapiAdi = y.Adi,
                            YapiBulunduguUlke = y.BulunduğuUlke,
                            YapiYapimYili = y.YapimYili.HasValue ? y.YapimYili.Value.ToString("yyyy/MM/dd", new CultureInfo("en-US")) : "",
                            MimarYasiyorMuu=m.YasiyorMu,
                            YapimYili=y.YapimYili,
                            MimarId=y.MimarId,
                            TurId=t.Id
                        };

                query = query.OrderBy(y => y.YapiAdi);

            }
            else
            {
                query = from y in yapiQuery
                        join yt in yapiTurQuery
                        on y.Id equals yt.YapiId into yapiTurJoin
                        from yapiTur in yapiTurJoin.DefaultIfEmpty()
                        join t in turQuery
                        on yapiTur.TurId equals t.Id into turJoin
                        from tur in turJoin.DefaultIfEmpty()
                        join m in mimarQuery
                        on y.MimarId equals m.Id into mimarJoin
                        from mimar in mimarJoin.DefaultIfEmpty()
                        select new ReportItemModel
                        {
                            MimarAdiSoyadi = mimar.Adi + " " + mimar.Soyadi,
                            MimarYasiyorMu = mimar.YasiyorMu == true ? "EVET" : "HAYIR",
                            MimarYasiyorMuu = mimar.YasiyorMu,
                            TurAdi = tur.Adi,
                            YapiAdi = y.Adi,
                            YapiBulunduguUlke = y.BulunduğuUlke,
                            YapiYapimYili = y.YapimYili.HasValue ? y.YapimYili.Value.ToString("yyyy/MM/dd", new CultureInfo("en-US")) : "",
                            YapimYili=y.YapimYili,
                            MimarId = y.MimarId,
                            TurId=tur.Id
                        };
                query = query.OrderBy(y => y.YapiAdi);

               
            };
            #region filtreleme
            if (model is not null)
            {
                if (!string.IsNullOrWhiteSpace(model.YapiAdi))
                {
                    query = query.Where(m => m.YapiAdi.ToLower().Contains(model.YapiAdi.ToLower().Trim()));
                }
                if (model.MimarId.HasValue)
                {
					query = query.Where(m => m.MimarId==model.MimarId);
				}
                if (model.TurId is not null && model.TurId.Any())
                {
					query = query.Where(m => model.TurId.Contains(m.TurId ?? 0));
				}
                if(model.YapiYapimYiliBaşlangıc.HasValue)
                {
                    query = query.Where(m => m.YapimYili >= model.YapiYapimYiliBaşlangıc);
                }
				if (model.YapiYapimYiliBitis.HasValue)
				{
					query = query.Where(m => m.YapimYili <= model.YapiYapimYiliBitis);
				}

				return query.ToList();
            }
            #endregion
            return query.ToList();
            #endregion
        }
    }
}
