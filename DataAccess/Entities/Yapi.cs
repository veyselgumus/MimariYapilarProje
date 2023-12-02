#nullable disable

using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
	public class Yapi : RecordBase
	{
		[Required]
		[StringLength(50)]
		public string Adi { get; set; }
		public DateTime? YapimYili { get; set; }
		public string BulunduğuUlke { get; set; }
		public Mimar Mimar { get; set; }
		public int MimarId { get; set; }
		public YapiDetay YapiDetay { get; set; }
		public List<YapiTur> YapiTurler { get; set; }

		#region Binary Data
		public byte[] Image { get; set; }

		[StringLength(5)]
        public string ImageExtension { get; set; }

        #endregion
    }
}
