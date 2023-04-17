using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BloodNetwork.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Display(Name = "Nume categorie")]
        public string CategoryName { get; set; }
        public ICollection<ClinicCategory>? ClinicCategories { get; set; }
    }
}
