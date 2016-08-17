using Bhtp.Url.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bhtp.Url
{
    /// <summary>
    /// The link object holds all information about the integration and is the top level object
    /// </summary>
    public class Link
    {
        /// <summary>
        /// Creates an instance of Link.
        /// </summary>
        /// <param name="agentCode">AgentCode the id of the agent who is referring the user to the site. Used to track commission</param>
        /// <param name="campaignId">An id allowing the integrating system to uniquely identify where the reference is coming from</param>
        /// <param name="productId">Specifies a perferred product. If not supplied, the user will be preseted with the product selection page</param>
        /// <param name="enableProdMode">Indicates whether or not the final created link can be used for production or not</param>
        public Link(string agentCode, string campaignId, string productId, bool enableProdMode = false)
        {
            this.AgentCode = agentCode;
            this.CampaignId = campaignId;
            this.ProductId = productId;

            this.Trip = new Trip();
            this.PolicyHolder = new Traveler();
            this.Flights = new List<Flight>();
            this.Travelers = new List<Traveler>();

            this.EnableProdMode = enableProdMode;
        }

        /// <summary>
        /// Indicates whether or not the final created link can be used for production or not
        /// </summary>
        public bool EnableProdMode { get; private set; }

        /// <summary>
        /// The id of the agent who is referring the user to the site. Used to track commission
        /// </summary>
        public string AgentCode { get; set; }

        /// <summary>
        /// An id allowing the integrating system to uniquely identify where the reference is coming from
        /// </summary>
        public string CampaignId { get; set; }

        /// <summary>
        /// Specifies a perferred product. If not supplied, the user will be preseted with the product selection page
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// Information about the trip to insure
        /// </summary>
        public Trip Trip { get; set; }

        /// <summary>
        /// Information about the flights to insure
        /// </summary>
        public ICollection<Flight> Flights { get; set; }

        /// <summary>
        /// Information about the travelers to ensure except the traveler that is the policyholder
        /// </summary>
        public ICollection<Traveler> Travelers { get; set; }

        /// <summary>
        /// Information about the traveler that is the policyholder
        /// </summary>
        public Traveler PolicyHolder { get; set; }

        /// <summary>
        /// Add a flight to the link
        /// </summary>
        /// <param name="flight">The filled flight object to add to the link</param>
        public void AddFlight(Flight flight)
        {
            if (flight != null)
            {
                this.Flights.Add(flight);
            }
        }

        /// <summary>
        /// Add a traveler to the link
        /// </summary>
        /// <param name="traveler">The filled traveler object to add to the link</param>
        public void AddTraveler(Traveler traveler)
        {
            if (traveler != null)
            {
                this.Travelers.Add(traveler);
            }
        }

        /// <summary>
        /// Given all the information containined within the link and its sub-objects, a link is generated that can be provided to a user
        /// </summary>
        /// <returns>the usable link</returns>
        public string GenerateLink()
        {
            StringBuilder link = new StringBuilder();

            this.AppendAnalyticsData(link);

            this.AppendTripData(link);

            this.AppendPolicyHolderData(link);

            this.AppendTravelerData(link);

            this.AppendFlightData(link);

            this.RemovingLeadingDelimiter(link);

            string baseUrl = this.EnableProdMode ? Constants.ProdBaseUrl : Constants.PreProdBaseUrl;

            StringBuilder result = new StringBuilder();
            result.Append(baseUrl);

            if (link.Length > 0)
            {
                result.Append(Constants.QueryStringStart);
                result.Append(link.ToString());
            }

            return result.ToString();
        }

        private void AppendAnalyticsData(StringBuilder link)
        {
            if (link != null)
            {
                Serializable s = new Serializable();

                if (!string.IsNullOrEmpty(this.AgentCode))
                {
                    s.AddValue(Constants.AnalyticsKeys.Source, this.AgentCode);
                    s.AddValue(Constants.AnalyticsKeys.Medium, Constants.Partner);
                }

                if (!string.IsNullOrEmpty(this.CampaignId))
                {
                    s.AddValue(Constants.AnalyticsKeys.Campaign, this.CampaignId);
                }

                if (!string.IsNullOrEmpty(this.ProductId))
                {
                    s.AddValue(Constants.AnalyticsKeys.Package, this.ProductId);
                }

                link.Append(s.Serialize(DelimiterType.Link));
            }
        }

        private void AppendTripData(StringBuilder link)
        {
            if (link != null && this.Trip != null)
            {
                string tripString = this.Trip.Serialize();

                if (tripString.Length > 0 && link.Length > 0)
                {
                    link.Append(Constants.QueryStringPairDelimeter);
                }

                link.Append(tripString);
            }
        }

        private void AppendPolicyHolderData(StringBuilder link)
        {
            if (link != null && this.PolicyHolder != null)
            {
                string travelerString = this.PolicyHolder.Serialize();

                if (travelerString.Length > 0)
                {
                    link.Append(this.GetQueryStringKey(Constants.TripKeys.PolicyHolder) + travelerString);
                }
            }
        }

        private void AppendTravelerData(StringBuilder link)
        {
            if (link != null && this.Travelers != null && this.Travelers.Any())
            {
                foreach (Traveler traveler in this.Travelers)
                {
                    string travelerString = this.GetQueryStringKey(Constants.TripKeys.Travelers) + traveler.Serialize();
                    link.Append(travelerString);
                }
            }
        }

        private void AppendFlightData(StringBuilder link)
        {
            if (link != null && this.Flights != null && this.Flights.Any())
            {
                foreach (Flight flight in this.Flights)
                {
                    string flightString = this.GetQueryStringKey(Constants.TripKeys.Flights) + flight.Serialize();
                    link.Append(flightString);
                }
            }
        }

        /// <summary>
        /// If link starts with &, remove it
        /// </summary>
        /// <param name="link"></param>
        private void RemovingLeadingDelimiter(StringBuilder link)
        {
            if (link != null && link.Length > 0 && link[0].ToString() == Constants.QueryStringPairDelimeter)
            {
                link = link.Remove(0, 1);
            }
        }

        private string GetQueryStringKey(string key)
        {
            StringBuilder result = new StringBuilder();

            result.Append(Constants.QueryStringPairDelimeter);
            result.Append(key);
            result.Append(Constants.QueryStringValueDelimiter);

            return result.ToString();
        }
    }
}