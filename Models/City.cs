namespace BloodNetwork.Models
{
    public class City
    {
        public int ID { get; set; }
        public string CityName { get; set; }
        public ICollection<Clinic>? Clinics { get; set; }
    }
}
