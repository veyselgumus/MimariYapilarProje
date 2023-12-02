using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Context
{
	public class MimariYapilarContextFactory : IDesignTimeDbContextFactory<MimariYapilarContext>
	{
		public MimariYapilarContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<MimariYapilarContext>();
			optionsBuilder.UseSqlServer("server=.\\SQLEXPRESS;database=MimariYapilarProjeDb;user id=sa;password=sa;multipleactiveresultsets=true;trustservercertificate=true;");
			return new MimariYapilarContext(optionsBuilder.Options);
		}
	}
}
