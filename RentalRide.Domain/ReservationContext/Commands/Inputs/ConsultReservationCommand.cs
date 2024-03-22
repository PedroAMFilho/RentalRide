using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalRide.Domain.ReservationContext.Commands.Inputs
{
    public class ConsultReservationCommand
    {
        public int ReservationId { get; set; }
        public DateTime EstimatedEndDate { get; set; }
    }
}
