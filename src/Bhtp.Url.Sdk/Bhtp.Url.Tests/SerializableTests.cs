using System.Collections.Generic;
using Xunit;

namespace Bhtp.Url.Tests
{
    public class SerializableTestsBase
    {
        protected static Dictionary<string, string> SingleDictionary = new Dictionary<string, string> { { "one", "1" } };

        protected static Dictionary<string, string> DoubleDictionary = new Dictionary<string, string> { { "one", "1" }, { "two", "2" } };
    }

    public class Serializable_ShouldSerializeCorrectlyBasedOnProvidedDataAndDelimiterType : SerializableTestsBase
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                return new[]
                {
                    new object[] { DoubleDictionary, DelimiterType.Object, "one:1;two:2" },
                    new object[] { SingleDictionary, DelimiterType.Object, "one:1" },
                    new object[] { DoubleDictionary, DelimiterType.Link, "one=1&two=2" },
                    new object[] { SingleDictionary, DelimiterType.Link, "one=1" }
                };
            }
        }

        // This will only show up as a single test, instead of 4 tests because not all types are serializable
        // https://github.com/xunit/xunit/blob/4e39f2c784fae4e864c67743f825c5f811c04d42/src/common/XunitSerializationInfo.cs#L104-L176
        [Theory, MemberData("TestData")]
        public void Test(Dictionary<string, string> valuesToAdd, DelimiterType delimiterType, string expected)
        {
            // Arrange
            Serializable s = new Serializable();

            foreach (string key in valuesToAdd.Keys)
            {
                s.AddValue(key, valuesToAdd[key]);
            }

            // Act
            string sut = s.Serialize(delimiterType);

            // Assert
            Assert.NotNull(sut);
            Assert.Equal(expected, sut);
        }
    }
}