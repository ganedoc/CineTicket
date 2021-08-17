using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using static CineTicket.Common.Constants;

namespace CineTicket
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var serviceProvider = ServiceSetup.GetServiceProvider();
                var process = serviceProvider.GetService<IProcess>();
                process.DoProcess();
            }
            catch(Exception ex)
            {
                EventLog.WriteEntry(EVENT_SOURCE_NAME, EXCEPTION_MAIN_HEADER + ex.ToString());
            }
        }
    }
}
