using System;
using System.Collections.Generic;
using Xunit;

namespace Bhtp.Url.Tests
{
    public class TravelerTestsBase
    {
        protected static decimal TripCost = 1000m;
        protected static int Age = 27;
        protected static DateTime BirthDate = new DateTime(1987, 08, 25);
        protected static string ExpectedBirthDate = BirthDate.ToString("yyyy-MM-dd");
    }

    public class Traveler_ShouldOnlySerializeAgeWhenBirthDateNotProvided : TravelerTestsBase
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                return new[]
                {
                    new object[] { "a", Age, null, TripCost, Age },
                    new object[] { "db", null, BirthDate, TripCost, ExpectedBirthDate },
                    new object[] { "db", Age, BirthDate, TripCost, ExpectedBirthDate }
                };
            }
        }

        [Theory, MemberData("TestData")]
        public void Test(string key, int? age, DateTime? birthDate, decimal? tripCost, object expected)
        {
            // Arrange
            Traveler traveler = new Traveler(tripCost);
            traveler.Age = age;
            traveler.BirthDate = birthDate;

            // Act
            string sut = traveler.Serialize();

            // Assert
            Assert.NotNull(sut);
            Assert.Equal($"{key}:{expected.ToString()};tc:{tripCost}", sut);
        }
    }

    public class Traveler_ShouldOnlySerializeProvidedData : TravelerTestsBase
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                return new[]
                {
                    new object[] { "a", Age, null, null, Age },
                    new object[] { "db", null, BirthDate, null, ExpectedBirthDate },
                    new object[] { "tc", null, null, TripCost, TripCost }
                };
            }
        }

        [Theory, MemberData("TestData")]
        public void Test(string key, int? age, DateTime? birthDate, decimal? tripCost, object expected)
        {
            // Arrange
            Traveler traveler = new Traveler(tripCost);
            traveler.Age = age;
            traveler.BirthDate = birthDate;

            // Act
            string sut = traveler.Serialize();

            // Assert
            Assert.NotNull(sut);
            Assert.Equal($"{key}:{expected.ToString()}", sut);
        }
    }
}