using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RentalRide.Domain.DelivererContext.Commands.Handler;
using RentalRide.Domain.DelivererContext.Repositories;
using RentalRide.Domain.MotorcycleContext.Repositories;
using RentalRide.Domain.ReservationContext.Commands.Handler;
using RentalRide.Domain.ReservationContext.Repositories;
using RentalRide.Domain.UserBaseContext.Repositories;
using RentalRide.Infra.CrossCutting.AspNetFilters;
using RentalRide.Infra.Data.DataContext;
using RentalRide.Infra.Data.DelivererContext.Repositories;
using RentalRide.Infra.Data.MotorcycleContext.Repositories;
using RentalRide.Infra.Data.ReservationContext.Repositories;
using RentalRide.Infra.Data.UserBaseContext.Repositories;
namespace RentalRide.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services) 
        {

            services.AddTransient<ReservationHandler, ReservationHandler>();
            services.AddTransient<DelivererHandler, DelivererHandler>();
            services.AddTransient<IMotorcycleRepository, MotorcycleRepository>();
            services.AddTransient<IDelivererRepository, DelivererRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<IUserBaseRepository, UserBaseRepository>(); 
            services.AddTransient<IMotorcycleRepository, MotorcycleRepository>();

            // Infra - Data
            object value = services.AddScoped<RentalRideDataContext, RentalRideDataContext>();

            // Infra - Filters
            services.AddScoped<ILogger<GlobalExceptionHandlingFilter>, Logger<GlobalExceptionHandlingFilter>>();
            services.AddScoped<GlobalExceptionHandlingFilter>();
        }
    }
}
