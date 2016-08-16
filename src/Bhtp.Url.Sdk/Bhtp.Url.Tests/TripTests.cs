using System;
using System.Collections.Generic;
using Xunit;

namespace Bhtp.Url.Tests
{
    public class TripTestsBase
    {
        public static class Values
        {
            public static string DestinationCountryIsoCode2 = "GB";
            public static string ResidenceStateIsoCode2 = "WI";
            public static string ResidencePostalCode = "54481";

            public static DateTime? DepartureDate = DateTime.Today.AddDays(30);
            public static string DepartureDateString = DepartureDate.Value.ToString("yyyy-MM-dd");

            public static DateTime? ReturnDate = DateTime.Today.AddDays(40);
            public static string ReturnDateString = ReturnDate.Value.ToString("yyyy-MM-dd");

            public static DateTime? InitialPaymentDate = DateTime.Today.AddDays(-15);
            public static string InitialPaymentDateString = InitialPaymentDate.Value.ToString("yyyy-MM-dd");

            public static string PolicyHolderEmail = "rickharrison@pawnshop.com";
            public static int? TotalTravelerCount = 5;
        }

        public static class Expected
        {
            public static string Destination = $"dc={Values.DestinationCountryIsoCode2}";
            public static string ResidenceStateOnly = $"rs={Values.ResidenceStateIsoCode2}";
            public static string ResidencePostalCodeOnly = $"rs={Values.ResidencePostalCode}";
            public static string ResidenceBoth = ResidencePostalCodeOnly;
            public static string DepartureDate = $"dd={Values.DepartureDateString}";
            public static string ReturnDate = $"rd={Values.ReturnDateString}";
            public static string InitialPaymentDate = $"pd={Values.InitialPaymentDateString}";
            public static string PolicyHolderEmail = $"e={Values.PolicyHolderEmail}";
            public static string TotalTravelerCount = $"tt={Values.TotalTravelerCount}";
            public static string Complete = $"{Destination}&{ResidencePostalCodeOnly}&{DepartureDate}&{ReturnDate}&{InitialPaymentDate}&{PolicyHolderEmail}&{TotalTravelerCount}";
        }
    }

    public class Trip_ShouldOnlySerializeProvidedData : TripTestsBase
    {

        public static IEnumerable<object[]> TestData
        {
            get
            {
                return new[]
                {
                    new object[] { Values.DestinationCountryIsoCode2, Values.ResidenceStateIsoCode2, Values.ResidencePostalCode, Values.DepartureDate, Values.ReturnDate, Values.InitialPaymentDate, Values.PolicyHolderEmail, Values.TotalTravelerCount, Expected.Complete },
                    new object[] { Values.DestinationCountryIsoCode2, null, null, null, null, null, null, null, Expected.Destination },
                    new object[] { null, Values.ResidenceStateIsoCode2, null, null, null, null, null, null, Expected.ResidenceStateOnly },
                    new object[] { null, null, Values.ResidencePostalCode, null, null, null, null, null, Expected.ResidencePostalCodeOnly },
                    new object[] { null, Values.ResidenceStateIsoCode2, Values.ResidencePostalCode, null, null, null, null, null, Expected.ResidenceBoth },
                    new object[] { null, null, null, Values.DepartureDate, null, null, null, null, Expected.DepartureDate },
                    new object[] { null, null, null, null, Values.ReturnDate, null, null, null, Expected.ReturnDate },
                    new object[] { null, null, null, null, null, Values.InitialPaymentDate, null, null, Expected.InitialPaymentDate },
                    new object[] { null, null, null, null, null, null, Values.PolicyHolderEmail, null, Expected.PolicyHolderEmail },
                    new object[] { null, null, null, null, null, null, null, Values.TotalTravelerCount, Expected.TotalTravelerCount }
                };
            }
        }

        [Theory, MemberData("TestData")]
        public void Test(string destination, string residenceState, string residencePostal, DateTime? departureDate, DateTime? returnDate, DateTime? initialPaymentDate, string email, int? travelerCount, string expected)
        {
            // Arrange
            Trip trip = new Trip
            {
                DestinationCountryIsoCode2 = destination,
                ResidenceStateIsoCode2 = residenceState,
                ResidencePostalCode = residencePostal,
                DepartureDate = departureDate,
                ReturnDate = returnDate,
                InitialPaymentDate = initialPaymentDate,
                PolicyHolderEmail = email,
                TotalTravelerCount = travelerCount
            };

            // Act
            string sut = trip.Serialize();

            // Assert
            Assert.NotNull(sut);
            Assert.Equal(expected, sut);
        }
    }
}