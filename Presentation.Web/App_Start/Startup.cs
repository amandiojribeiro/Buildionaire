using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Farfetch.Buildionaire.Presentation.Web.App_Start.Startup))]

namespace Farfetch.Buildionaire.Presentation.Web.App_Start
{
    using System.Configuration;

    public class Startup
    {
        private static string connectionString;

        private static string ConnectionString
        {
            get
            {
                if (!string.IsNullOrEmpty(connectionString))
                {
                    return connectionString;
                }

                var config = ConfigurationManager.ConnectionStrings["BuildionaireDomainContextConnectionString"];
                if (config == null || string.IsNullOrEmpty(config.ConnectionString))
                {
                    throw new ConfigurationErrorsException("There is no connection string with name RiskManagementConnectionString.");
                }

                connectionString = config.ConnectionString;
                return connectionString;
            }
        }



        public void Configuration(IAppBuilder app)
        {
            app.UseHangfire(config =>
            {
                // Basic setup required to process background jobs.
                config.UseSqlServerStorage(ConnectionString);
                config.UseServer(int.Parse(ConfigurationManager.AppSettings["HangfireWorkerCount"]), "importbuilds", "calculatebadges","importcodereview","importchangesets", "default");
                config.UseActivator(new UnityJobActivator(UnityConfig.Container));
                config.UseAuthorizationFilters();
            });
            
             
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });

        }
    }
}