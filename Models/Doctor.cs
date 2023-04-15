using System.ComponentModel.DataAnnotations;

namespace BloodNetwork.Models
{
    public class Doctor
    {
        public int ID { get; set; }
        public string DoctorName { get; set; }
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Telefonul trebuie sa fie de forma '0722-123-123' sau'0722.123.123' sau '0722 123 123'")]
        public string? Number { get; set; }
        public ICollection<Clinic>? Clinics { get; set; }
    }
}
