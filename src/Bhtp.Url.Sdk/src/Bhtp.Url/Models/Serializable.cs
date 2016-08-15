using System.Collections.Generic;
using System.Text;

namespace Bhtp.Url.Models
{
    public enum DelimiterType
    {
        Link, // & and =
        Object // ; and :
    }

    /// <summary>
    /// Internal class used to serialize data to include in the link
    /// </summary>
    internal class Serializable
    {
        private readonly string CustomValueDelimiter = ":";
        private readonly string QueryStringValueDelimiter = "=";
        private readonly string CustomPairDelimeter = ";";
        private readonly string QueryStringPairDelimeter = "&";

        /// <summary>
        /// Internal dictionary used to hold the keys and values to serialize
        /// </summary>
        private Dictionary<string, string> dictionary = new Dictionary<string, string>();

        /// <summary>
        /// Add a value to the dictionary of values to serialize
        /// </summary>
        /// <param name="key">the key to add</param>
        /// <param name="value">the value to add</param>
        public void AddValue(string key, string value)
        {
            this.dictionary.Add(key, value);
        }

        /// <summary>
        /// Serializes the dictionary into key/value pairs based on the delimeter type
        /// </summary>
        /// <param name="delimType">indicates if it should serialize the data on the object level or the link level</param>
        /// <returns>the serialized string that can be used in the final link</returns>
        public string Serialize(DelimiterType delimType)
        {
            // Determine delim strings
            string valueDelim = delimType == DelimiterType.Object ? this.CustomValueDelimiter : this.QueryStringValueDelimiter;
            string pairDelim = delimType == DelimiterType.Object ? this.CustomPairDelimeter : this.QueryStringPairDelimeter;

            StringBuilder result = new StringBuilder();

            // put serialized result together
            foreach (string key in this.dictionary.Keys)
            {
                if (result.Length > 0)
                {
                    result.Append(pairDelim);
                }

                result.Append(this.GetDelimitedChunk(key, valueDelim, this.dictionary[key]));
            }

            return result.ToString();
        }

        private string GetDelimitedChunk(string key, string delimiter, string value)
        {
            StringBuilder result = new StringBuilder();

            result.Append(key);
            result.Append(delimiter);
            result.Append(value);

            return result.ToString();
        }
    }
}