#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
	public class YapiTur 
	{
        [Key]
        [Column(Order = 0)]
        public int YapiId { get; set; }
        public Yapi Yapi { get; set; }


		[Key]
		[Column(Order = 1)]
		public int TurId { get; set; }
        public Tur Tur { get; set; }
    }
}
