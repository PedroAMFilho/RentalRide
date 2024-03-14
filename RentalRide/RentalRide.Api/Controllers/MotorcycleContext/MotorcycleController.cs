using Microsoft.AspNetCore.Mvc;
using RentalRide.Domain.MotorcycleContext.Commands.Inputs;
using RentalRide.Domain.MotorcycleContext.Entities;
using RentalRide.Domain.MotorcycleContext.Queries;
using RentalRide.Domain.MotorcycleContext.Repositories;
using RentalRide.Shared.Commands;

namespace RentalRide.Api.Controllers.MotorcycleContext
{
    public class MotorcycleController : Controller
    {
        private readonly IMotorcycleRepository _repository;

        public MotorcycleController(IMotorcycleRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("rentalride/get-motorcycle/{license}")]
        public IEnumerable<Motorcycle> GetByLicense(string license)
        {
            return _repository.GetByLicense(license);
        }

        [HttpPost]
        [Route("rentalride/create-motorcycle")]
        public void Create([FromBody] CreateMotorcycleCommand command) 
        {
            _repository.Create(command);
        }

        [HttpPost]
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
