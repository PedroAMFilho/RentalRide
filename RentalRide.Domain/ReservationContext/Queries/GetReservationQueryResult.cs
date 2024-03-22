using RentalRide.Domain.ReservationContext.Enums;


namespace RentalRide.Domain.ReservationContext.Queries
{
    public class GetReservationQueryResult
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string LicensePlate { get; set; }
        public string Name { get; set; }
        public int RentalDays { get; set; }
        public decimal DailyCost { get; set; }
        public decimal PercentageFine { get; set; }
        public EReservationStatus Status { get; set; }
    }
}
