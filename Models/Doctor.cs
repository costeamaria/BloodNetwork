namespace BloodNetwork.Models
{
    public class Doctor
    {
        public int ID { get; set; }
        public string DoctorName { get; set; }
        public ICollection<Clinic>? Clinics { get; set; }
    }
}
