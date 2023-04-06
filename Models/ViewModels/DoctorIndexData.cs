using System.Security.Policy;
using BloodNetwork.Models;


namespace BloodNetwork.Models.ViewModels
{
    public class DoctorIndexData
    {
        public IEnumerable<Doctor> Doctors { get; set; }
        public IEnumerable<Clinic> Clinics { get; set; }
    }
}
