using System.Data;
using Npgsql;
using Dapper;

namespace RentalRide.Infra.Data.DataContext
{
    public class RentalRideDataContext : IDisposable
    {
        public NpgsqlConnection Connection { get; set; }

        public RentalRideDataContext() 
        {
            Connection = new NpgsqlConnection("Host=rentalride.database;Username=postgres;Password=admin;Database=rentalride");
            Connection.Open();
        }

        public void Dispose() 
        {
            if (Connection.State != ConnectionState.Closed)
            Connection.Close();
        }
    }
}
