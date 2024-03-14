using RentalRide.Domain.UserBaseContext.Enum;

namespace RentalRide.Domain.DelivererContext.Entities
{
    public class Deliverer
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime date_of_birth { get; set; }
        public string drivers_license { get; set; }
        public string license_photo_url { get; set; }
        public ELicense license_type { get; set; }
    }
}
