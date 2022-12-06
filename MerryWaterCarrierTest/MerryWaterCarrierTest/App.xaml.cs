using MerryWaterCarrierTest.DBContexts;
using MerryWaterCarrierTest.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MerryWaterCarrierTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddDbContext<WaterCarrierDBContext>();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }


        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<WaterCarrierService>();
        }

        public static T GetService<T>()
        {
            return serviceProvider.GetService<T>();
        }
    }
}
