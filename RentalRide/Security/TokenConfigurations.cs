namespace RentalRide.Api.Security
{
    public class TokenConfigurations
    {
        public string ISSUER { get; }
        public string AUDIENCE { get; }
        public string SECRET_KEY { get; }
        public int SECONDS { get; }

        public TokenConfigurations()
        {
            ISSUER = "974b89d4";
            AUDIENCE = "f095c15bf5f5";
            SECRET_KEY = "974b89d4-8a24-4691-bc9f-f095c15bf5f5";
            SECONDS = 86400;
        }
    }
}
