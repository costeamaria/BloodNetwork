using System.Security.Policy;
using BloodNetwork.Models;


namespace BloodNetwork.Models.ViewModels
{
    public class AdressIndexData
    {
        public IEnumerable<Adress> Adresses { get; set; }
        public IEnumerable<Clinic> Clinics { get; set; }
    }
}
