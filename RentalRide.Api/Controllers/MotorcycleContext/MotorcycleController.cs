using Microsoft.AspNetCore.Mvc;
using RentalRide.Domain.DeliveryContext.Commands.Handler;
using RentalRide.Domain.MotorcycleContext.Commands.Handlers;
using RentalRide.Domain.MotorcycleContext.Commands.Inputs;
using RentalRide.Domain.MotorcycleContext.Commands.Outputs;
using RentalRide.Domain.MotorcycleContext.Entities;
using RentalRide.Domain.MotorcycleContext.Queries;
using RentalRide.Domain.MotorcycleContext.Repositories;
using RentalRide.Shared.Commands;

namespace RentalRide.Api.Controllers.MotorcycleContext
{
    public class MotorcycleController : Controller
    {
        private readonly MotorcycleHandler _handler;
        private readonly IMotorcycleRepository _repository;

        public MotorcycleController(IMotorcycleRepository repository, MotorcycleHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpGet]
        [Route("rentalride/get-motorcycle/{license}")]
        public IEnumerable<Motorcycle> GetByLicense(string license)
        {
            return _repository.GetByLicense(license);
        }

        [HttpPut]
        [Route("rentalride/create-motorcycle")]
        public ICommandResult Create([FromBody] CreateMotorcycleCommand command) 
        {
            var result = (CommandResult)_handler.Handle(command);
            return result;
        }

        [HttpPatch]
        [Route("rentalride/edit-motorcycle")]
        public void Update([FromBody] UpdateMotorcycleCommand command) 
        {
           _repository.Update(command);
        }

        [HttpDelete]
        [Route("rentalride/delete-motorcycle")]
        public void Delete(int id) 
        {
            _repository.Delete(id);
        }

    }
}
