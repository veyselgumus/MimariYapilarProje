#nullable disable

using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class TurModel : RecordBase
    {
        [Required(ErrorMessage = "{0} is Required")]
        [StringLength(50)]
        [DisplayName("ADI")]
        public string Adi { get; set; }

        public List<YapiModel> Yapilar { get; set; }
    }
}
