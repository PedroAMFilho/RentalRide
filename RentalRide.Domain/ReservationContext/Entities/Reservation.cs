using RentalRide.Domain.MotorcycleContext.Entities;
using RentalRide.Domain.ReservationContext.Enums;

namespace RentalRide.Domain.ReservationContext.Commands.Entities
{
    public class Reservation
    {
        public string id { get; set; }
        public DateTime start_date { get; set; }
        public DateTime estimated_end_date { get; set; }
        public DateTime end_date{ get; set; }
        public Motorcycle motorcycle { get; set; }
        public ReservationPlan reservation_plan { get; set; }
        public EReservationStatus status { get; set; }
    }
}
