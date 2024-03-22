using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalRide.Domain.ReservationContext.Commands.Outputs
{
    public class ConsultReservationQueryResult
    {
        public decimal ReservationFinalPrice { get; set; }

        public decimal ReservationBasePrice { get; set; }

        public decimal ReservationFine { get; set; }
    }
}
