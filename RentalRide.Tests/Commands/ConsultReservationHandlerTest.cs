using Moq;
using RentalRide.Domain.DelivererContext.Repositories;
using RentalRide.Domain.MotorcycleContext.Repositories;
using RentalRide.Domain.ReservationContext.Commands.Handler;
using RentalRide.Domain.ReservationContext.Commands.Inputs;
using RentalRide.Domain.ReservationContext.Queries;
using RentalRide.Domain.ReservationContext.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalRide.Tests.Commands
{
    public class ConsultReservationHandlerTest
    {
        private readonly Mock<IReservationRepository> _reservtionRepository;
        private readonly Mock<IMotorcycleRepository> _motorcycleRepository;
        private readonly Mock<IDelivererRepository> _delivererRepository;

        public ConsultReservationHandlerTest()
        {
            _reservtionRepository = new();
            _motorcycleRepository = new();
            _delivererRepository = new();
        }

        [Fact]
        public void Handle_Should_ReturnFailureResult_When_ReservationNotFound()
        {
            var command = new ConsultReservationCommand()
            {
                ReservationId = 99999,
                EstimatedEndDate = DateTime.Now.AddDays(5)
            };

            _reservtionRepository.Setup(x => x.GetReservation(It.IsAny<int>())).Returns(new GetReservationQueryResult());

            var handler = new ReservationHandler(_reservtionRepository.Object,
                _motorcycleRepository.Object, _delivererRepository.Object);

            var result = handler.Handle(command);

            Assert.False(result.Success);
        }

    }
}
