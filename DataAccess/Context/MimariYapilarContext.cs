using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
	public class MimariYapilarContext : DbContext
	{
        public DbSet<Mimar> Mimarlar { get; set; }
        public DbSet<Yapi> Yapilar { get; set; }
        public DbSet<Tur> Turler { get; set; }
        public DbSet<YapiTur> YapiTurler { get; set; }
        public DbSet<YapiDetay> YapiDetaylar { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Rol> Roller { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<YapiTur>().HasKey(yt => new { yt.TurId, yt.YapiId });
		}


		public MimariYapilarContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
