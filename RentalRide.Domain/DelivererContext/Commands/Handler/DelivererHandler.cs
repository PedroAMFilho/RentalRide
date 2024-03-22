using FluentValidator;
using RentalRide.Domain.DelivererContext.Commands.Inputs;
using RentalRide.Domain.DelivererContext.Commands.Outputs;
using RentalRide.Domain.DelivererContext.Repositories;
using RentalRide.Domain.UserBaseContext.Commands.Inputs;
using RentalRide.Domain.UserBaseContext.Enum;
using RentalRide.Domain.UserBaseContext.Repositories;
using RentalRide.Shared.Commands;

namespace RentalRide.Domain.DelivererContext.Commands.Handler
{
    public class DelivererHandler : Notifiable, ICommandHandler<CreateDelivererCommand>
    {
        private readonly IDelivererRepository _repository;
        private readonly IUserBaseRepository _userBaseRepository;

        public DelivererHandler(IDelivererRepository repository, IUserBaseRepository userBaseRepository) 
        {
            _repository = repository;
            _userBaseRepository = userBaseRepository;
        }

        public ICommandResult Handle(CreateDelivererCommand command)
        {
            if (!command.IsValidCommand())
                return new CommandResult(false, "Invalid request, please verify the input fields.", new {command.Notifications});

            if (command.LicensePhotoUrl == null) 
            { 
                //if(command.imageFile.
            }

            var id = _repository.Create(command);
            
            _userBaseRepository.Create(CreateUserDeliverer(command,id));

            return new CommandResult(true, "Deliverer created with success", new
            {
            });
        }

        public CreateUserBaseCommand CreateUserDeliverer(CreateDelivererCommand command, int deliverer_id)
        {
            return new CreateUserBaseCommand()
            {
                username = command.Username,
                password = command.Password,
                access_level = EAccessLevel.deliverer,
                deliverer_id = deliverer_id,
                email = command.Email 
            };
        }
    }
}
