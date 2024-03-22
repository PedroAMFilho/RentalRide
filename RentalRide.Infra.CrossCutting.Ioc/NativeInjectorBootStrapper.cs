using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RentalRide.Domain.DelivererContext.Commands.Handler;
using RentalRide.Domain.DelivererContext.Repositories;
using RentalRide.Domain.DeliveryContext.Commands.Handler;
using RentalRide.Domain.DeliveryContext.Repositories;
using RentalRide.Domain.MotorcycleContext.Commands.Handlers;
using RentalRide.Domain.MotorcycleContext.Repositories;
using RentalRide.Domain.ReservationContext.Commands.Handler;
using RentalRide.Domain.ReservationContext.Repositories;
using RentalRide.Domain.UserBaseContext.Repositories;
using RentalRide.Infra.CrossCutting.AspNetFilters;
using RentalRide.Infra.Data.DataContext;
using RentalRide.Infra.Data.DelivererContext.Repositories;
using RentalRide.Infra.Data.DeliveryContext.DeliveryServices;
using RentalRide.Infra.Data.DeliveryContext.Repositories;
using RentalRide.Infra.Data.MessageContext;
using RentalRide.Infra.Data.MotorcycleContext.Repositories;
using RentalRide.Infra.Data.ReservationContext.Repositories;
using RentalRide.Infra.Data.UserBaseContext.Repositories;
namespace RentalRide.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services) 
        {
            services.AddTransient<MotorcycleHandler, MotorcycleHandler>();
            services.AddTransient<DeliveryHandler, DeliveryHandler>();
            services.AddTransient<ReservationHandler, ReservationHandler>();
            services.AddTransient<DelivererHandler, DelivererHandler>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IDeliveryRepository, DeliveryRepository>();
            services.AddTransient<IMotorcycleRepository, MotorcycleRepository>();
            services.AddTransient<IDelivererRepository, DelivererRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<IUserBaseRepository, UserBaseRepository>(); 
            services.AddTransient<IMotorcycleRepository, MotorcycleRepository>();

            // Infra - Data
            object value = services.AddScoped<RentalRideDataContext, RentalRideDataContext>();
            services.AddScoped<RentalRideMessageContext, RentalRideMessageContext>();

            // Infra - Filters
            services.AddScoped<ILogger<GlobalExceptionHandlingFilter>, Logger<GlobalExceptionHandlingFilter>>();
            services.AddScoped<GlobalExceptionHandlingFilter>();
        }
    }
}
