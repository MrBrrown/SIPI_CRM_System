using System;
using Microsoft.EntityFrameworkCore.Design;
using MySql.EntityFrameworkCore.Extensions;

namespace SIPI_CRM_System.Services
{
    public class MysqlEntityFrameworkDesignTimeServices : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddEntityFrameworkMySQL();
            new EntityFrameworkRelationalDesignServicesBuilder(serviceCollection)
                .TryAddCoreServices();
        }
    }
}

