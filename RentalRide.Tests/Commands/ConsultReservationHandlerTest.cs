using Moq;
using RentalRide.Domain.DelivererContext.Repositories;
using RentalRide.Domain.MotorcycleContext.Repositories;
using RentalRide.Domain.ReservationContext.Commands.Handler;
using RentalRide.Domain.ReservationContext.Commands.Inputs;
using RentalRide.Domain.ReservationContext.Queries;
using RentalRide.Domain.ReservationContext.Repositories;

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


        [Fact]
        public void Handle_Should_ApplyFine_When_ConsultingDelayed_Resevation()
        {
            var command = new ConsultReservationCommand()
            {
                ReservationId = 3,
                EstimatedEndDate = DateTime.Now.AddDays(9)
            };

            _reservtionRepository.Setup(x => x.GetReservation(It.IsAny<int>())).Returns(new GetReservationQueryResult() 
            {  
                Id = 3,
                RentalDays = 7,
                PercentageFine = 20,
                EstimatedEndDate = DateTime.Now.AddDays(9),
                StartDate = DateTime.Now,
                DailyCost = 30
            });

            var handler = new ReservationHandler(_reservtionRepository.Object,
                _motorcycleRepository.Object, _delivererRepository.Object);

            var result = handler.Handle(command);

            Assert.True(result.Success);

            var fineProperty = result.Data.GetType();
            decimal fine = (decimal)fineProperty.GetProperty("ReservationFine").GetValue(result.Data, null);

            Assert.True(fine != 0);
        }
    }
}
