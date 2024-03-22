using RentalRide.Domain.UserBaseContext.Enum;

namespace RentalRide.Domain.DelivererContext.Entities
{
    public class Deliverer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cnpj { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DriversLicense { get; set; }
        public string LicensePhotoUrl { get; set; }
        public ELicense LicenseType { get; set; }
    }
}
