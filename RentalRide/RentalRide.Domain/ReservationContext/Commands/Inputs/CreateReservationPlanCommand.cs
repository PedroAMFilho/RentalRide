namespace RentalRide.Domain.ReservationContext.Commands.Inputs
{
    public class CreateReservationPlanCommand
    {
        public string name { get; set; }
        public int rental_days { get; set; }
        public decimal daily_cost { get; set; }
    }
}
