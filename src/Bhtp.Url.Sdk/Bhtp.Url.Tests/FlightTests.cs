using System;
using System.Collections.Generic;
using Bhtp.Url.Utility;
using Xunit;

namespace Bhtp.Url.Tests
{
    public class FlightTestsBase
    {
        protected static DateTime DepartureDate = new DateTime(2016, 6, 24);
        protected static string DepartureDateString = DepartureDate.ToIso8601();
        protected static int FlightNumber = 1234;
        protected static string AirlineCode = "DL";
        protected static string DepartureAirportCode = "PNS";
        protected static string ArrivalAirportCode = "ATL";
    }

    public class Flight_ShouldSerializeEntireObject : FlightTestsBase
    {
        [Fact]
        public void Test()
        {
            // Arrange
            Flight flight = new Flight(DepartureDate, FlightNumber, AirlineCode, DepartureAirportCode, ArrivalAirportCode);

            // Act
            string sut = flight.Serialize();

            // Assert
            Assert.NotNull(sut);
            Assert.Equal($"d:{DepartureDateString};n:{FlightNumber};ac:{AirlineCode};da:{DepartureAirportCode};aa:{ArrivalAirportCode}", sut);
        }
    }

    public class Flight_ShouldOnlySerializeProvidedData : FlightTestsBase
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                return new[]
                {
                    new object[] { "d", DepartureDate, null, null, null, null, DepartureDateString },
                    new object[] { "n", null, FlightNumber, null, null, null, FlightNumber },
                    new object[] { "ac", null, null, AirlineCode, null, null, AirlineCode },
                    new object[] { "da", null, null, null, DepartureAirportCode, null, DepartureAirportCode },
                    new object[] { "aa", null, null, null, null, ArrivalAirportCode, ArrivalAirportCode }
                };
            }
        }

        [Theory, MemberData("TestData")]
        public void Test(string key, DateTime? departureDate, int? flightNumber, string airlineCode, string departureAirportCode, string arrivalAirportCode, object expected)
        {
            // Arrange
            Flight flight = new Flight(departureDate, flightNumber, airlineCode, departureAirportCode, arrivalAirportCode);

            // Act
            string sut = flight.Serialize();

            // Assert
            Assert.NotNull(sut);
            Assert.Equal($"{key}:{expected.ToString()}", sut);
        }
    }
}