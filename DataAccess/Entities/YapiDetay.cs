#nullable disable

using AppCore.Records.Bases;
using DataAccess.Enums;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
	public class YapiDetay : RecordBase
	{
		public int? InsaatAlani { get; set; }
		[Required]
		[StringLength(1000)]
		public string Konsepti { get; set; }
		public TasiyiciSistem TasiyiciSistem { get; set; }
		public Yapi Yapi { get; set; }
		public int YapiId { get; set; }
	}
}
