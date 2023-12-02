#nullable disable

using AppCore.Records.Bases;
using DataAccess.Entities;
using DataAccess.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
	public class YapiDetayModel : RecordBase
	{
		#region entity den gelen özellikler
		[DisplayName("İNŞAAT ALANI")]
		public int? InsaatAlani { get; set; }
		[Required(ErrorMessage = "{0} is Required")]
		[StringLength(1000)]
		[DisplayName("KONSEPT")]
		public string Konsepti { get; set; }
		[Required(ErrorMessage = "{0} is Required")]
		[DisplayName("TAŞIYICI SİSTEM")]
		public TasiyiciSistem TasiyiciSistem { get; set; }
		public int YapiId { get; set; }
		#endregion

		#region
		[DisplayName("İNŞAAT ALANI")]
        public string InsaatAlaniGosterim { get; set; }
		[DisplayName("TAŞIYICI SİSTEM")]
		public string TasiyiciSistemGosterim { get; set; }

        #endregion
    }
}
