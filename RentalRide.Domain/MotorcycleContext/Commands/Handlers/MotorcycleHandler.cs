using FluentValidator;
using RentalRide.Domain.DeliveryContext.Commands.Inputs;
using RentalRide.Domain.MotorcycleContext.Commands.Inputs;
using RentalRide.Domain.MotorcycleContext.Commands.Outputs;
using RentalRide.Domain.MotorcycleContext.Repositories;
using RentalRide.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalRide.Domain.MotorcycleContext.Commands.Handlers
{
    public class MotorcycleHandler : Notifiable , ICommandHandler<CreateMotorcycleCommand>
    {
        private readonly IMotorcycleRepository _repository;
        public MotorcycleHandler(IMotorcycleRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(CreateMotorcycleCommand command)
        {

            if (!command.IsValidCommand())
                return new CommandResult(false, "Invalid request", command.Notifications);

            _repository.Create(command);

            return new CommandResult(true, "Motorcycle sucessfuly created.", new 
            { 
                
            });

        }

    }
}
