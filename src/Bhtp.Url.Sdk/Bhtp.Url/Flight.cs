using System;
using Bhtp.Url.Utility;

namespace Bhtp.Url
{
    /// <summary>
    /// The flight object holds all information about the flights to insure
    /// </summary>
    public class Flight
    {
        /// <summary>
        /// Creates an instance of Flight.
        /// </summary>
        /// <param name="departureDate">the date of the flight's departure in ISO 8601 format</param>
        /// <param name="flightNumber">The number of the flight</param>
        /// <param name="airlineCode">The IATA code of the airline that is servicing the flight (Example: Delta Air Lines Inc. = DL)</param>
        /// <param name="departureAirportCode">The IATA code of the airport the flight departs from (Example: O'Hare International Airport = ORD)</param>
        /// <param name="arrivalAirportCode">The IATA code of the airport the flight arrives at (Example: O'Hare International Airport = ORD)</param>
        public Flight(DateTime? departureDate, int? flightNumber, string airlineCode, string departureAirportCode, string arrivalAirportCode)
        {
            this.DepartureDate = departureDate;
            this.FlightNumber = flightNumber;
            this.AirlineCode = airlineCode;
            this.DepartureAirportCode = departureAirportCode;
            this.ArrivalAirportCode = arrivalAirportCode;
        }

        /// <summary>
        /// the date of the flight's departure in ISO 8601 format
        /// </summary>
        public DateTime? DepartureDate { get; set; }

        /// <summary>
        /// The number of the flight
        /// </summary>
        public int? FlightNumber { get; set; }

        /// <summary>
        /// The IATA code of the airline that is servicing the flight (Example: Delta Air Lines Inc. = DL)
        /// </summary>
        public string AirlineCode { get; set; }

        /// <summary>
        /// The IATA code of the airport the flight departs from (Example: O'Hare International Airport = ORD)
        /// </summary>
        public string DepartureAirportCode { get; set; }

        /// <summary>
        /// The IATA code of the airport the flight arrives at (Example: O'Hare International Airport = ORD)
        /// </summary>
        public string ArrivalAirportCode { get; set; }

        /// <summary>
        /// Creates a string represtening the flight that can be used in the final link
        /// </summary>
        /// <returns>a link usable string</returns>
        public string Serialize()
        {
            Serializable s = new Serializable();

            if (this.DepartureDate != null && this.DepartureDate.HasValue)
            {
                s.AddValue(Constants.FlightKeys.DepartureDate, this.DepartureDate.Value.ToIso8601());
            }

            if (this.FlightNumber != null)
            {
                s.AddValue(Constants.FlightKeys.FlightNumber, this.FlightNumber.ToString());
            }

            if (!string.IsNullOrEmpty(this.AirlineCode))
            {
                s.AddValue(Constants.FlightKeys.AirlineCode, this.AirlineCode);
            }

            if (!string.IsNullOrEmpty(this.DepartureAirportCode))
            {
                s.AddValue(Constants.FlightKeys.DepartureAirportCode, this.DepartureAirportCode);
            }

            if (!string.IsNullOrEmpty(this.ArrivalAirportCode))
            {
                s.AddValue(Constants.FlightKeys.ArrivalAirportCode, this.ArrivalAirportCode);
            }

            return s.Serialize(DelimiterType.Object);
        }
    }
}