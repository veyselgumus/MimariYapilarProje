#nullable disable

using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
	public class Tur : RecordBase
	{
		[Required(ErrorMessage ="{0} is Required")]
		[StringLength(50)]
		public string Adi { get; set; }
		public List<YapiTur> YapiTurler { get; set; }
	}
}
