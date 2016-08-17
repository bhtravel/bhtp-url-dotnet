using Bhtp.Url.Utility;
using System;

namespace Bhtp.Url
{
    /// <summary>
    /// The trip object holds all information about the trip to insure
    /// </summary>
    public class Trip : ISerializable
    {
        /// <summary>
        /// The ISO 3166-2 code for the destination country that will be visited. If there is more than one destination country, pass only one
        /// </summary>
        public string DestinationCountryIsoCode2 { get; set; }

        /// <summary>
        /// The ISO 3166-2:US code for the US state of residence of the policy without the US- portion in the beginning. If ResidencePostalCode is supplied, this will be ignored
        /// </summary>
        public string ResidenceStateIsoCode2 { get; set; }

        /// <summary>
        /// The postal code of the US address of residence of the policyholder. This takes precendence over ResidenceStateIsoCode2
        /// </summary>
        public string ResidencePostalCode { get; set; }

        /// <summary>
        /// The date of departure in ISO 8601 format
        /// </summary>
        public DateTime? DepartureDate { get; set; }

        /// <summary>
        /// The date of return from the trip in ISO 8601 format
        /// </summary>
        public DateTime? ReturnDate { get; set; }

        /// <summary>
        /// The date the first payment toward the trip was made in ISO 8601 format
        /// </summary>
        public DateTime? InitialPaymentDate { get; set; }

        /// <summary>
        /// The email of the policyholder
        /// </summary>
        public string PolicyHolderEmail { get; set; }

        /// <summary>
        /// An optional field identifying how many travelers, including the policyholder, that will be on the policy.
        /// This may be omitted in lieu of specifying a policyholder and travelers
        /// </summary>
        public int? TotalTravelerCount { get; set; }

        /// <summary>
        /// Creates a string represtening the trip that can be used in the final link
        /// </summary>
        /// <returns>a link usable string</returns>
        public string Serialize()
        {
            Serializable s = new Serializable();

            if (!string.IsNullOrEmpty(this.DestinationCountryIsoCode2))
            {
                s.AddValue(Constants.TripKeys.DestinationCountryIsoCode2, this.DestinationCountryIsoCode2);
            }

            if (!string.IsNullOrEmpty(this.ResidencePostalCode))
            {
                s.AddValue(Constants.TripKeys.Residence, this.ResidencePostalCode);
            }
            else if (!string.IsNullOrEmpty(this.ResidenceStateIsoCode2))
            {
                s.AddValue(Constants.TripKeys.Residence, this.ResidenceStateIsoCode2);
            }

            if (this.DepartureDate != null && this.DepartureDate.HasValue)
            {
                s.AddValue(Constants.TripKeys.DepartureDate, this.DepartureDate.Value.ToIso8601());
            }

            if (this.ReturnDate != null && this.ReturnDate.HasValue)
            {
                s.AddValue(Constants.TripKeys.ReturnDate, this.ReturnDate.Value.ToIso8601());
            }

            if (this.InitialPaymentDate != null && this.InitialPaymentDate.HasValue)
            {
                s.AddValue(Constants.TripKeys.InitialPaymentDate, this.InitialPaymentDate.Value.ToIso8601());
            }

            if (!string.IsNullOrEmpty(this.PolicyHolderEmail))
            {
                s.AddValue(Constants.TripKeys.PolicyHolderEmail, this.PolicyHolderEmail);
            }

            if (this.TotalTravelerCount != null)
            {
                s.AddValue(Constants.TripKeys.TotalTravelerCount, this.TotalTravelerCount.ToString());
            }

            return s.Serialize(DelimiterType.Link);
        }
    }
}