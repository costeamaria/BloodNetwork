using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BloodNetwork.Models
{
    public class Adress
    {
        public int ID { get; set; }
        [Display(Name = "Nume adresă")]
        public string AdressName { get; set; }
        public ICollection<Clinic>? Clinics{ get; set; }
    }
}
