﻿using Bhtp.Url.Utility;
using System;

namespace Bhtp.Url
{
    /// <summary>
    /// The traveler object holds all information about the traveler to insure
    /// </summary>
    public class Traveler : ISerializable
    {
        /// <summary>
        /// Creates an instance of Traveler
        /// </summary>
        /// <param name="tripCost">The cost of the trip for this traveler in US dollars</param>
        public Traveler(decimal? tripCost = null)
        {
            this.TripCost = tripCost;
        }

        /// <summary>
        /// The age of the traveler. If the birthdate is specified, this value is ignored
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// The date of birth of the traveler in ISO 8601 format
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// The cost of the trip for this traveler in US dollars
        /// </summary>
        public decimal? TripCost { get; set; }

        /// <summary>
        /// Creates a string represtening the traveler that can be used in the final link
        /// </summary>
        /// <returns>A link usable string</returns>
        public string Serialize()
        {
            Serializable s = new Serializable();

            if (this.BirthDate != null && this.BirthDate.HasValue)
            {
                s.AddValue(Constants.TravelerKeys.BirthDate, this.BirthDate.Value.ToIso8601());
            }
            else if (this.Age != null)
            {
                s.AddValue(Constants.TravelerKeys.Age, this.Age.ToString());
            }

            if (this.TripCost != null && this.TripCost >= 0)
            {
                s.AddValue(Constants.TravelerKeys.TripCost, this.TripCost.ToString());
            }

            return s.Serialize(DelimiterType.Object);
        }
    }
}