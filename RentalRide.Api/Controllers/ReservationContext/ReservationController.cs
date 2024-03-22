using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalRide.Domain.ReservationContext.Commands.Handler;
using RentalRide.Domain.ReservationContext.Commands.Inputs;
using RentalRide.Domain.ReservationContext.Repositories;
using RentalRide.Domain.UserBaseContext.Commands.Outputs;
using RentalRide.Shared.Commands;

namespace RentalRide.Api.Controllers.ReservationContext
{
    public class ReservationController : Controller
    {
        private readonly ReservationHandler _handler;

        public ReservationController(IReservationRepository repository, ReservationHandler handler)
        {
            _handler = handler;
        }

        [HttpPut]
        [Authorize]
        [Route("rentalride/create-reservation")]
        public ICommandResult Create([FromBody]CreateReservationCommand command) 
        {
            var result = (CommandResult)_handler.Handle(command);
            return result;
        }

        [HttpPost]
        [Route("rentalride/consult")]
        public ICommandResult Consult([FromBody] ConsultReservationCommand command)
        {
            var result = (CommandResult)_handler.Handle(command);
            return result;
        }

        [HttpPut]
        [Authorize]
        [Route("rentalride/create-reservation-plan")]
        public ICommandResult CreateReservationPlan([FromBody] CreateReservationPlanCommand command)
        {
            var result = (CommandResult)_handler.Handle(command);
            return result;
        }
    }
}
