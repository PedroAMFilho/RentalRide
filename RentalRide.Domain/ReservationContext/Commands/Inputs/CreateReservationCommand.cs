namespace RentalRide.Domain.ReservationContext.Commands.Inputs
{
    public class CreateReservationCommand
    {
        public DateTime start_date {get;set;}
        public DateTime estimated_end_date { get; set; }
        public int motorcycle_id { get; set; }
        public int reservation_plan_id { get; set; }
        public int deliverer_id { get; set; }
    }
} 
