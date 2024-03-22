using RentalRide.Domain.DeliveryContext.Enum;

namespace RentalRide.Domain.DeliveryContext.Entities
{
    public class Delivery
    {
        public int Id { get; set; } 
        public int DelivererId { get; set; }
        public int ClientId { get; set; }
        public ECurrentStatus CurrentStatus { get; set; }
        public decimal DeliveryCost { get; set; } 
        public DateTime CreatedAt { get; set; }
    }
}
