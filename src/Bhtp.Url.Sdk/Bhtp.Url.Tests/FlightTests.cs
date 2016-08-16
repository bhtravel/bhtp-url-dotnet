﻿using System;
using System.Collections.Generic;
using Xunit;

namespace Bhtp.Url.Tests
{
    public class FlightTestsBase
    {
        protected static DateTime DepartureDate { get { return new DateTime(2016, 6, 24); } }

        protected static string ExpectedDepartureDate { get { return DepartureDate.ToString("yyyy-MM-dd"); } }

        protected static int FlightNumber { get { return 1234; } }

        protected static string AirlineCode { get { return "DL"; } }

        protected static string DepartureAirportCode { get { return "PNS"; } }

        protected static string ArrivalAirportCode { get { return "ATL"; } }
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
            Assert.Equal($"d:{ExpectedDepartureDate};n:{FlightNumber};ac:{AirlineCode};da:{DepartureAirportCode};aa:{ArrivalAirportCode}", sut);
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
                    new object[] { "d", DepartureDate, null, null, null, null, ExpectedDepartureDate },
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