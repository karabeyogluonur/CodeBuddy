using Microsoft.Extensions.Configuration;

namespace CB.Data.Utilities
{
    public class Configuration
    {
        static public string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/CB.Web"));
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("CodeBuddySql");
            }
        }
    }
}
