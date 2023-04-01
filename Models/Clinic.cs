using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Security.Policy;

namespace BloodNetwork.Models
{
    public class Clinic
    {
        public int ID { get; set; }

        [Display(Name = "Clinic Name")]
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal Phone { get; set; }
        public int? AdressID { get; set; }
        public Adress? Adress { get; set; }
        public int? DoctorID { get; set; }
        public Doctor? Doctor { get; set; }
        public ICollection<ClinicCategory>? ClinicCategories { get; set; }
    }
}
