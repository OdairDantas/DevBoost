using System;
using System.Collections.Generic;
using System.Text;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Config
{
    public static class MongoConfig
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static bool IsSSL { get; set; }
    }
}
