#nullable disable

using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
	public class HesapLoginModel
	{
		[Required(ErrorMessage = "{0} is Required")]
		[StringLength(50, ErrorMessage = "{0} must be min. {1} character")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "{0} is Required")]
		[StringLength(50, ErrorMessage = "{0} must be min. {1} character")]
		public string Sifre { get; set; }
		public string ReturnUrl { get; set; }
	}
}
