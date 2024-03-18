using RentalRide.Domain.DeliveryContext.Enum;

namespace RentalRide.Domain.DeliveryContext.Entities
{
    public class Delivery
    {
        public int id { get; set; } 
        public int deliverer_id { get; set; }
        public int client_id { get; set; }
        public ECurrentStatus current_status { get; set; }
        public decimal delivery_cost { get; set; } 
        public DateTime created_at { get; set; }
    }
}
