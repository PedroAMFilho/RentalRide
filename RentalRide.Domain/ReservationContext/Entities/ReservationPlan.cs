namespace RentalRide.Domain.ReservationContext.Commands.Entities
{
    public class ReservationPlan
    {
        public int id { get; set; }
        public string name { get; set; }
        public int rental_days { get; set; }
        public decimal daily_cost { get; set; }
        public decimal percentage_fine { get; set; }
    }
}
