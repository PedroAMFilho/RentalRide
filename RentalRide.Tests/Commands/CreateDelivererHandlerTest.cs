﻿using Moq;
using RentalRide.Domain.DelivererContext.Commands.Handler;
using RentalRide.Domain.DelivererContext.Commands.Inputs;
using RentalRide.Domain.DelivererContext.Repositories;
using RentalRide.Domain.UserBaseContext.Commands.Inputs;
using RentalRide.Domain.UserBaseContext.Repositories;

namespace RentalRide.Tests.Commands
{
    public class CreateDelivererHandlerTest
    {
        private readonly Mock<IDelivererRepository> _repository;
        private readonly Mock<IUserBaseRepository> _userBaseRepository;

        public CreateDelivererHandlerTest() 
        {
            _repository = new();
            _userBaseRepository = new();
        }

        [Fact]
        public void Handle_Should_Create_Deliverer() 
        {
            var command = new CreateDelivererCommand()
            {
                Cnpj = "22.916.501/0001-55",
                DateOfBirth = new DateTime(2000, 1, 1),
                DriversLicense = "01141162593",
                Username = "Test User",
                Email = "test@test.com",
                FirstName = "John",
                LastName = "Doe",
                Password = "password"
            };

            _repository.Setup(x => x.Create(command)).Returns(5);
            _userBaseRepository.Setup(x => x.Create(It.IsAny<CreateUserBaseCommand>()));

            var handler = new DelivererHandler(_repository.Object, _userBaseRepository.Object);

            var result = handler.Handle(command);

            Assert.True(result.Success);
        }

    }
}
