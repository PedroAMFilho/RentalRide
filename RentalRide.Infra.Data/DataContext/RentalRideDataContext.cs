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
            Connection = new NpgsqlConnection("Host=localhost:5432;Username=postgres;Password=rentalrideadmin;Database=RentalRide");
            Connection.Open();
        }

        public void Dispose() 
        {
            if (Connection.State != ConnectionState.Closed)
            Connection.Close();
        }
    }
}
