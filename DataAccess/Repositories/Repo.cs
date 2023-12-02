using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Records.Bases;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class Repo<TEntity> : RepoBase<TEntity> where TEntity : RecordBase, new()
	{
		// new'lenebilen ve RecordBase'den miras alan tip olarak TEntity üzerinden entity tipini kullanacak,
		// RepoBase abstract class'ından miras alan ve veritabanı işlemlerini gerçekleştirecek somut class.
		public Repo(MimariYapilarContext dbContext) : base(dbContext) // projemizin MimariYapilarContext tipindeki dbContext'i Dependency Injection (Constructor Injection) ile
																	  // Repo'ya dolayısıyla da RepoBase'e dışarıdan new'lenerek enjekte edilecek.
		{

		}
	}
}
