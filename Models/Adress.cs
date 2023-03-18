namespace BloodNetwork.Models
{
    public class Adress
    {
        public int ID { get; set; }
        public string AdressName { get; set; }
        public ICollection<Clinic>? Clinics{ get; set; }
    }
}
