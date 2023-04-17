using System.ComponentModel.DataAnnotations;

namespace BloodNetwork.Models
{
    public class Doctor
    {
        public int ID { get; set; }
        [Display(Name = "Poză profil")]
        public string ProfilePictureURL { get; set; }
        [Display(Name = "Nume Doctor")]
        public string DoctorName { get; set; }
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Telefonul trebuie sa fie de forma '0722-123-123' sau'0722.123.123' sau '0722 123 123'")]
        [Display(Name = "Telefon")]
        public string? Number { get; set; }
        public ICollection<Clinic>? Clinics { get; set; }
    }
}
