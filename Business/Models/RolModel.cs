#nullable disable

using AppCore.Records.Bases;
using DataAccess.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
	public class RolModel :RecordBase
	{
		[Required]
		[StringLength(50)]
		[DisplayName("Rolü")]
		public string Adi { get; set; }

	}
}
