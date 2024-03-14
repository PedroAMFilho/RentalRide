using RentalRide.Domain.MotorcycleContext.Entities;
using RentalRide.Domain.ReservationContext.Enums;

namespace RentalRide.Domain.ReservationContext.Commands.Entities
{
    public class Reservation
    {
        public string id { get; set; }
        public string start_date { get; set; }
        public string estimated_end_date { get; set; }
        public string end_date{ get; set; }
        public Motorcycle motorcycle { get; set; }
        public ReservationPlan reservation { get; set; }
        public EReservationStatus status { get; set; }
    }
}
