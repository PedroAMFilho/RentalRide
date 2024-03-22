using RentalRide.Shared.Entities;

namespace RentalRide.Domain.MotorcycleContext.Entities
{
    public class Motorcycle : Entity
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string LicensePlate { get; set; }
        public int TimesRented { get; set; }
    }
}
