using Microsoft.AspNetCore.Mvc;
using RentalRide.Domain.DeliveryContext.Commands.Outputs;
using RentalRide.Domain.DeliveryContext.Commands.Handler;
using RentalRide.Domain.DeliveryContext.Commands.Inputs;
using RentalRide.Domain.DeliveryContext.Repositories;
using RentalRide.Shared.Commands;

namespace RentalRide.Api.Controllers.DeliveryContext
{
    public class DeliveryController : Controller
    {
        private readonly IDeliveryRepository _repository;
        private readonly DeliveryHandler _handler;

        public DeliveryController(IDeliveryRepository repository, DeliveryHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpPut]
        [Route("rentalride/create-delivery")]
        public ICommandResult Create([FromBody] CreateDeliveryCommand command)
        {
            var result = (CommandResult)_handler.Handle(command);
            return result;
        }

        [HttpGet]
        [Route("rentalride/get-delivery")]
        public ICommandResult GetDelivery(int delivery_id, int deliverer_id)
        {
            var result = _repository.GetDelivery(delivery_id, deliverer_id);
            return result;
        }

        [HttpPatch]
        [Route("rentalride/accept-delivery")]
        public ICommandResult AcceptDelivery(AcceptDeliveryComand command)
        {
            var result = (CommandResult)_handler.Handle(command);
            return result;
        }

        [HttpPatch]
        [Route("rentalride/end-delivery")]
        public ICommandResult EndDelivery(int delivery_id) 
        {
            var result = _repository.EndDelivery(delivery_id);
            return result;
        }

    }
}
