using RentalRide.Shared.Entities;

namespace RentalRide.Domain.MotorcycleContext.Entities
{
    public class Motorcycle : Entity
    {
        public int id { get; set; }
        public string model { get; set; }
        public int year { get; set; }
        public string license_plate { get; set; }
        public int times_rented { get; set; }
    }
}
