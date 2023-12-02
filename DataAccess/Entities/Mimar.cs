#nullable disable

using AppCore.DataAccess.EntityFramework.Bases;
using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
	public class Mimar : RecordBase
	{
		[Required(ErrorMessage ="{0} is Required")]
		[StringLength(50)]
		public string Adi { get; set; }

		[Required(ErrorMessage ="{0} is Required")]
		[StringLength(50)]
		public string Soyadi { get; set; }
		public DateTime? DogumTarihi { get; set; }
		public bool? YasiyorMu { get; set; }
		public List<Yapi> Yapilari { get; set; }

        #region Binary Data
        public byte[] Image { get; set; }

        [StringLength(5)]
        public string ImageExtension { get; set; }

        #endregion
    }
}
