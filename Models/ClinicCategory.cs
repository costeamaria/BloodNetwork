namespace BloodNetwork.Models
{
    public class ClinicCategory
    {
        public int ID { get; set; }
        public int ClinicID { get; set; }
        public Clinic Clinic { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
