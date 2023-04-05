using BloodNetwork.Migrations;

namespace BloodNetwork.Models
{
    public class ClinicData
    {
        public IEnumerable<Clinic> Clinics { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<ClinicCategory> ClinicCategories { get; set; }
    }
}
