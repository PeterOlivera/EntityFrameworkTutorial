using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace ContosoUniversity.DAL
{
    public class SchoolConfiguration : DbConfiguration
    {
        public SchoolConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());

            // Intrceptos can be added here, currently they are been added on Global.asax
            //DbInterception.Add(new SchoolInterceptorTransientErrors());
            //DbInterception.Add(new SchoolInterceptorLogging());
        }
    }
}