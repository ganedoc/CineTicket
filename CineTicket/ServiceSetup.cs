using System;
using CineTicket.Core.Serrvices;
using Microsoft.Extensions.DependencyInjection;

namespace CineTicket
{
    internal static class ServiceSetup
    {
        public static ServiceProvider GetServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IProcess, Process>();
            serviceCollection.AddScoped<IBooker, Booker>();
            return serviceCollection.BuildServiceProvider();
        }
    }
}
