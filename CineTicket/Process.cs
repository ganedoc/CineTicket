using CineTicket.Core.Entities;
using CineTicket.Core.Serrvices;
using System;
using System.IO;
using System.Linq;
using static CineTicket.Common.Constants;
using CineTicket.Common;

namespace CineTicket
{
    public class Process : IProcess
    {
        private readonly IBooker booker;
        public Process(IBooker booker)
            => this.booker = booker;
        public void DoProcess()
        {
            try
            {
                var filePath = Configurations.GetConfigValue(FILE_PATH);
                var bookingRequests = File.ReadAllLines(filePath)
                    .Select(line => line.Split(new char[] { COMMA, COLON }, StringSplitOptions.RemoveEmptyEntries))
                    .Select(data => new BookingRequest
                    {
                        BookingId = long.Parse(data[0].TrimStart(OPEN_BRACE)),
                        FirstSeatRowNumber = byte.Parse(data[1]),
                        FirstSeatNumber = byte.Parse(data[2]),
                        LastSeatRowNumber = byte.Parse(data[3]),
                        LastSeatNumber = byte.Parse(data[4].TrimEnd(CLOSE_BRACE))
                    }).ToList();
                var bookingResponse = booker.BulkProcess(bookingRequests);

                Logger.ConsoleLogSummary(filePath,bookingRequests, bookingResponse);
            }
            catch
            {
                throw;
            }
        }
    }
}
