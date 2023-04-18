using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Security.Policy;

namespace BloodNetwork.Models
{
    public class Clinic
    {
        public int ID { get; set; }

        [Display(Name = "Nume Clinică")]
        public string Name { get; set; }
        [Display(Name = "Cantitate")]
        public decimal Quantity { get; set; }
        public int? AdressID { get; set; }
        [Display(Name = "Locație")]
        public Adress? Adress { get; set; }
        public int? DoctorID { get; set; }
        [Display(Name = "Doctor")]
        public Doctor? Doctor { get; set; }
        [Display(Name = "Categorie")]
        public ICollection<ClinicCategory>? ClinicCategories { get; set; }
    }
}
