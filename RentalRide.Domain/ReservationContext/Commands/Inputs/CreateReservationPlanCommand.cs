namespace RentalRide.Domain.ReservationContext.Commands.Inputs
{
    public class CreateReservationPlanCommand
    {
        public string? Name { get; set; }
        public int RentalDays { get; set; }
        public decimal DailyCost { get; set; }
        public decimal PercentageFine { get; set; }
    }
}
