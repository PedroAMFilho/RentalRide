using RentalRide.Domain.ReservationContext.Enums;


namespace RentalRide.Domain.ReservationContext.Queries
{
    public class GetReservationQueryResult
    {
        public string id { get; set; }
        public DateTime start_date { get; set; }
        public DateTime estimated_end_date { get; set; }
        public DateTime end_date { get; set; }
        public string model { get; set; }
        public int year { get; set; }
        public string license_plate { get; set; }
        public string name { get; set; }
        public int rental_days { get; set; }
        public decimal daily_cost { get; set; }
        public decimal percentage_fine { get; set; }
        public EReservationStatus status { get; set; }
    }
}
