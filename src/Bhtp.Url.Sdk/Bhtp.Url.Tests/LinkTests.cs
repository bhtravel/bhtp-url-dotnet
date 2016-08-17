using System;
using System.Collections.Generic;
using Xunit;

namespace Bhtp.Url.Tests
{
    public class LinkTestsBase
    {
        protected static class Values
        {
            public static string AgentCode = "AAgent";
            public static string Medium = "Partner";
            public static string Campaign = "TestPromo";
            public static string Package = "AirCare";

            public static string DestinationCountry = "GB";
            public static Trip Trip = new Trip { DestinationCountryIsoCode2 = DestinationCountry };

            public static decimal PolicyHolderTripCost = 100m;
            public static int PolicyHolderAge = 34;
            public static Traveler PolicyHolder = new Traveler { TripCost = PolicyHolderTripCost, Age = PolicyHolderAge };

            public static decimal Traveler1TripCost = 100m;
            public static int Traveler1Age = 27;
            public static Traveler Traveler1 = new Traveler { TripCost = Traveler1TripCost, Age = Traveler1Age };
            public static IEnumerable<Traveler> Travelers = new List<Traveler> { Traveler1 };

            public static DateTime DepartureDate = new DateTime(2016, 6, 24);
            public static string DepartureDateString = DepartureDate.ToString("yyyy-MM-dd");
            public static int FlightNumber = 1234;
            public static string AirlineCode = "DL";
            public static string DepartureAirportCode = "PNS";
            public static string ArrivalAirportCode = "ATL";
            public static Flight Flight = new Flight(DepartureDate, FlightNumber, AirlineCode, DepartureAirportCode, ArrivalAirportCode);
            public static IEnumerable<Flight> Flights = new List<Flight> { Flight };
        }

        protected static class Expected
        {
            public static string NonProdBaseUrl = "https://sbx-www.bhtp.com/i";
            public static string ProdBaseUrl = "https://www.bhtp.com/i";
            public static string QueryStringStart = "?";
            public static string QueryStringSeparator = "&";
            public static string SourceAndMedium = $"utm_source={ Values.AgentCode }&utm_medium={ Values.Medium }";
            public static string Campaign = $"campaign={ Values.Campaign }";
            public static string Package = $"package={ Values.Package }";
            public static string Trip = $"dc={ Values.DestinationCountry }";
            public static string PolicyHolder = $"ph=a:{ Values.PolicyHolderAge };tc:{ Values.PolicyHolderTripCost }";
            public static string Travelers = $"t=a:{ Values.Traveler1Age };tc:{ Values.Traveler1TripCost }";
            public static string Flights = $"f=d:{ Values.DepartureDateString };n:{ Values.FlightNumber };ac:{ Values.AirlineCode };da:{ Values.DepartureAirportCode };aa:{ Values.ArrivalAirportCode }";
            public static string Complete = NonProdBaseUrl + QueryStringStart + SourceAndMedium + QueryStringSeparator + Campaign + QueryStringSeparator + Package + QueryStringSeparator + Trip + QueryStringSeparator + PolicyHolder + QueryStringSeparator + Travelers + QueryStringSeparator + Flights;
        }
    }

    public class Link_ShouldCreateLinkForCorrectEnvironment : LinkTestsBase
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                return new[]
                {
                    new object[] { true, Expected.ProdBaseUrl },
                    new object[] { false, Expected.NonProdBaseUrl }
                };
            }
        }

        [Theory, MemberData("TestData")]
        public void Test(bool enableProd, string expected)
        {
            // Arrange
            Link link = new Link(null, null, null, enableProd);

            // Act
            string sut = link.GenerateLink();

            // Assert
            Assert.NotNull(sut);
            Assert.Equal(expected, sut);
        }
    }

    public class Link_ShouldOnlyAppendProvidedData : LinkTestsBase
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                return new[]
                {
                    new object[] { Values.AgentCode, Values.Medium, null, null, null, null, null, null, Expected.NonProdBaseUrl + Expected.QueryStringStart + Expected.SourceAndMedium },
                    new object[] { null, null, Values.Campaign, null, null, null, null, null, Expected.NonProdBaseUrl + Expected.QueryStringStart + Expected.Campaign },
                    new object[] { null, null, null, Values.Package, null, null, null, null, Expected.NonProdBaseUrl + Expected.QueryStringStart + Expected.Package },
                    new object[] { null, null, null, null, Values.Trip, null, null, null, Expected.NonProdBaseUrl + Expected.QueryStringStart + Expected.Trip },
                    new object[] { null, null, null, null, null, Values.PolicyHolder, null, null, Expected.NonProdBaseUrl + Expected.QueryStringStart + Expected.PolicyHolder },
                    new object[] { null, null, null, null, null, null, Values.Travelers, null, Expected.NonProdBaseUrl + Expected.QueryStringStart + Expected.Travelers },
                    new object[] { null, null, null, null, null, null, null, Values.Flights, Expected.NonProdBaseUrl + Expected.QueryStringStart + Expected.Flights },
                    new object[] { Values.AgentCode, Values.Medium, Values.Campaign, Values.Package, Values.Trip, Values.PolicyHolder, Values.Travelers, Values.Flights, Expected.Complete },
                };
            }
        }

        [Theory, MemberData("TestData")]
        public void Test(string agentCode, string medium, string campaign, string package, Trip trip, Traveler policyHolder, ICollection<Traveler> travelers, ICollection<Flight> flights, string expected)
        {
            // Arrange
            Link link = new Link(agentCode, campaign, package);
            link.Trip = trip;
            link.PolicyHolder = policyHolder;
            link.Travelers = travelers;
            link.Flights = flights;

            // Act
            string sut = link.GenerateLink();

            // Assert
            Assert.NotNull(sut);
            Assert.Equal(expected, sut);
        }
    }

    public class Link_AddFlight : LinkTestsBase
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                return new[]
                {
                    new object[] { Values.Flight, 1 },
                    new object[] { null, 0 },
                };
            }
        }

        [Theory, MemberData("TestData")]
        public void Test(Flight flight, int expectedCount)
        {
            // Arrange
            Link link = new Link(null, null, null);

            // Act
            link.AddFlight(flight);

            // Assert
            Assert.Equal(expectedCount, link.Flights.Count);
        }
    }

    public class Link_AddTraveler : LinkTestsBase
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                return new[]
                {
                    new object[] { Values.Traveler1, 1 },
                    new object[] { null, 0 },
                };
            }
        }

        [Theory, MemberData("TestData")]
        public void Test(Traveler traveler, int expectedCount)
        {
            // Arrange
            Link link = new Link(null, null, null);

            // Act
            link.AddTraveler(traveler);

            // Assert
            Assert.Equal(expectedCount, link.Travelers.Count);
        }
    }
}