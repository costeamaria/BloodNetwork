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
        [Display(Name ="Dată și oră")]
        public DateTime StartTime { get; set; }
        [Display(Name = "Durată")]
        public DateTime EndTime { get { return StartTime.AddMinutes(30); } }
        public int DurationInMinutes { get { return 30; } }

    }
}
