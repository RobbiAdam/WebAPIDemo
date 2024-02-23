namespace WebAPIDemo.Authority
{
    public class AppRepository
    {
        private static List<Application> _applications = new List<Application>()
        {
            new Application()
            {
                ApplicationId = 1,
                ApplicationName = "MVCWebApp",
                ClientId = "FECBA483-29FC-4EBD-BCD5-99082FEA6D66",
                Secret = "F6F23714-446F-4D9C-AF2A-0F99D532162C",
                Scopes = "read,write"
            }
        };

        public static bool Authenticate(string clientId, string secret)
        {
            return _applications.Any(x => x.ClientId == clientId && x.Secret == secret);
        }

        public static Application? GetApplicationByClientId(string clientId)
        {
            return _applications.FirstOrDefault(x => x.ClientId == clientId);
        }


    }
}
