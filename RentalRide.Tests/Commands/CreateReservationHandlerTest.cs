using Moq;
using RentalRide.Domain.DelivererContext.Entities;
using RentalRide.Domain.DelivererContext.Repositories;
using RentalRide.Domain.MotorcycleContext.Repositories;
using RentalRide.Domain.ReservationContext.Commands.Handler;
using RentalRide.Domain.ReservationContext.Commands.Inputs;
using RentalRide.Domain.ReservationContext.Repositories;
using RentalRide.Domain.UserBaseContext.Enum;

namespace RentalRide.Tests.Commands
{
    public class CreateReservationHandlerTest
    {
        private readonly Mock<IReservationRepository> _reservtionRepository;
        private readonly Mock<IMotorcycleRepository> _motorcycleRepository;
        private readonly Mock<IDelivererRepository> _delivererRepository;

        public CreateReservationHandlerTest()
        {
            _reservtionRepository = new();
            _motorcycleRepository = new();
            _delivererRepository = new();
        }

        [Fact]
        public void Handle_Should_Create_Reservaion()
        {
            var command = new CreateReservationCommand()
            {
                start_date = DateTime.Now,
                estimated_end_date = DateTime.Now.AddDays(5),
                motorcycle_id = 2,
                reservation_plan_id = 3,
                deliverer_id = 2
            };

            _motorcycleRepository.Setup(x => x.MotorcycleIsAvailable(It.IsAny<int>())).Returns(true);
            _delivererRepository.Setup(x => x.GetDelivererById(It.IsAny<int>())).Returns(new Deliverer() { LicenseType = ELicense.A });
            _reservtionRepository.Setup(x => x.Create(It.IsAny<CreateReservationCommand>())).Returns(15);

            var handler = new ReservationHandler(_reservtionRepository.Object,
                _motorcycleRepository.Object, _delivererRepository.Object);

            var result = handler.Handle(command);

            Assert.True(result.Success);
        }

        [Fact]
        public void Handle_Should_ReturnFailureResult_When_ExpectedDate_before_CreationDate()
        {
            var command = new CreateReservationCommand()
            {
                start_date = DateTime.Now,
                estimated_end_date = DateTime.Now.AddDays(-5),
                motorcycle_id = 2,
                reservation_plan_id = 3,
                deliverer_id = 2
            };

            var handler = new ReservationHandler(_reservtionRepository.Object,
                _motorcycleRepository.Object, _delivererRepository.Object);

            var result = handler.Handle(command);

            Assert.False(result.Success);
        }

        [Fact]
        public void Handle_Should_ReturnFailureResult_When_Motorcycle_isNot_Available()
        {
            var command = new CreateReservationCommand()
            {
                start_date = DateTime.Now,
                estimated_end_date = DateTime.Now.AddDays(5),
                motorcycle_id = 2,
                reservation_plan_id = 3,
                deliverer_id = 2
            };

            _motorcycleRepository.Setup(x => x.MotorcycleIsAvailable(It.IsAny<int>())).Returns(false);

            var handler = new ReservationHandler(_reservtionRepository.Object,
                _motorcycleRepository.Object, _delivererRepository.Object);

            var result = handler.Handle(command);

            Assert.False(result.Success);
        }

        [Fact]
        public void Handle_Should_ReturnFailureResult_When_Deliverer_DosentHave_ALicenseType()
        {
            var command = new CreateReservationCommand()
            {
                start_date = DateTime.Now,
                estimated_end_date = DateTime.Now.AddDays(5),
                motorcycle_id = 2,
                reservation_plan_id = 3,
                deliverer_id = 2
            };

            _delivererRepository.Setup(x => x.GetDelivererById(It.IsAny<int>())).Returns(new Deliverer() { LicenseType = ELicense.B });

            var handler = new ReservationHandler(_reservtionRepository.Object,
                _motorcycleRepository.Object, _delivererRepository.Object);

            var result = handler.Handle(command);

            Assert.False(result.Success);
        }

        [Fact]
        public void Handle_Should_ReturnFailureResult_When_Reservation_IsNot_Created()
        {
            var command = new CreateReservationCommand()
            {
                start_date = DateTime.Now,
                estimated_end_date = DateTime.Now.AddDays(5),
                motorcycle_id = 2,
                reservation_plan_id = 3,
                deliverer_id = 2
            };

            _reservtionRepository.Setup(x => x.Create(It.IsAny<CreateReservationCommand>())).Returns(0);

            var handler = new ReservationHandler(_reservtionRepository.Object,
                _motorcycleRepository.Object, _delivererRepository.Object);

            var result = handler.Handle(command);

            Assert.False(result.Success);
        }
    }
}
