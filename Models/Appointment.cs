using System.ComponentModel.DataAnnotations;

namespace BloodNetwork.Models
{
    public class Appointment
    {
        public int ID { get; set; }
        public int? MemberID { get; set; }
        public Member? Member { get; set; }
        public int? ClinicID { get; set; }
        public Clinic? Clinic { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }
    }
}
