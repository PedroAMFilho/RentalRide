using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalRide.Domain.ReservationContext.Commands.Outputs
{
    public class ConsultReservationQueryResult
    {
        public decimal reservationFinalPrice { get; set; }

        public decimal reservationBasePrice { get; set; }

        public decimal reservationFine { get; set; }
    }
}
