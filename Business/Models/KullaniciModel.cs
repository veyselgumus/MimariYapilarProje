#nullable disable

using AppCore.Records.Bases;
using DataAccess.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
	public class KullaniciModel : RecordBase
	{
		[Required(ErrorMessage = "{0} is Required")]
		[StringLength(50, ErrorMessage = "{0} must be min. {1} character")]
		[DisplayName("User Name")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "{0} is Required")]
		[StringLength(50, ErrorMessage = "{0} must be min. {1} character")]
        [DisplayName("Şifre")]
        public string Sifre { get; set; }
        [DisplayName("Aktif Mi")]
        public bool AktifMi { get; set; }
        [DisplayName("E-Mail")]
        public string Email { get; set; }
		public string Telefon { get; set; }
        [DisplayName("Rolü")]
        public int RolId { get; set; }
        public string IdGosterim { get; set; }

        #region viewlarda gösterim için kullanılan özellikler

        public RolModel RolModel { get; set; }
        [DisplayName("Aktif Mi")]
        public string AktifMiGosterim { get; set; }

        #endregion

    }
}
