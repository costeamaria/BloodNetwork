using System.ComponentModel.DataAnnotations;

namespace BloodNetwork.Models
{
    public class Appointment
    {
        public int ID { get; set; }
        [Display(Name = "Email")]
        public int? MemberID { get; set; }
        [Display(Name = "Membru")]
        public Member? Member { get; set; }
        [Display(Name = "Clinică")]
        public int? ClinicID { get; set; }
        [Display(Name = "Clinică")]
        public Clinic? Clinic { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Oră programare")]
        public DateTime AppointmentTime { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Dată programare")]
        public DateTime AppointmentDate { get; set; }
    }
}
