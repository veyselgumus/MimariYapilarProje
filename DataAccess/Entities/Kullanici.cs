#nullable disable

using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
	public class Kullanici : RecordBase
	{
		[Required(ErrorMessage ="{0} is Required")]
		[StringLength(50 , ErrorMessage ="{0} must be min. {1} character")]
		public string UserName { get; set; }
		[Required(ErrorMessage ="{0} is Required")]
		[StringLength(50, ErrorMessage = "{0} must be min. {1} character")]
		public string Sifre { get; set; }
		public bool AktifMi { get; set; }
		public string Email { get; set; }
        public string Telefon { get; set; }
        
		public Rol Rol { get; set; }
		public int RolId { get; set; }
	}
}
