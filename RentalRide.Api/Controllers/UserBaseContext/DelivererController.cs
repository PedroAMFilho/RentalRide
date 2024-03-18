using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalRide.Domain.DelivererContext.Commands.Handler;
using RentalRide.Domain.DelivererContext.Commands.Inputs;
using RentalRide.Domain.DelivererContext.Commands.Outputs;
using RentalRide.Domain.DelivererContext.Entities;
using RentalRide.Domain.DelivererContext.Repositories;
using RentalRide.Shared.Commands;

namespace RentalRide.Api.Controllers.UserBaseContext
{
    public class DelivererController : Controller
    {
        private readonly IDelivererRepository _repository;
        private readonly DelivererHandler _handler;

        public DelivererController(IDelivererRepository repository, DelivererHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpGet]
        [Route("rentalride/get-all-deliverers")]
        public IEnumerable<Deliverer> GetAllDeliverers() 
        {
            return _repository.GetAllDeliverers();
        }

        [HttpGet]
        [Route("rentalride/get-deliverer-by-name/{name}")]
        public IEnumerable<Deliverer> GetDelivererByName(string name)
        {
            return _repository.GetDelivererByName(name);
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("rentalride/create-deliverer")]
        public ICommandResult Create([FromBody]CreateDelivererCommand command,[FromForm] IFormFile imageFile) 
        {
            var result = (CommandResult)_handler.Handle(command);
            return result;
        }
    }
}
