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
        public bool EnableProdMode { get; set; }

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

            // Main data
            Serializable s = new Serializable();

            if (!string.IsNullOrEmpty(this.AgentCode))
            {
                s.AddValue("utm_source", this.AgentCode);
                s.AddValue("utm_medium", "Partner");
            }

            if (!string.IsNullOrEmpty(this.CampaignId))
            {
                s.AddValue("campaign", this.CampaignId);
            }

            if (!string.IsNullOrEmpty(this.ProductId))
            {
                s.AddValue("package", this.ProductId);
            }

            link.Append(s.Serialize(DelimiterType.Link));

            // Trip
            if (this.Trip != null)
            {
                string tripString = this.Trip.Serialize();

                if (tripString.Length > 0 && link.Length > 0)
                {
                    link.Append("&");
                }

                link.Append(tripString);
            }

            // Travelers
            if (this.PolicyHolder != null)
            {
                string travelerString = this.PolicyHolder.Serialize();

                if (travelerString.Length > 0)
                {
                    link.Append("&ph=" + travelerString);
                }
            }

            if (this.Travelers != null && this.Travelers.Any())
            {
                foreach (Traveler traveler in this.Travelers)
                {
                    string travelerString = "&t=" + traveler.Serialize();
                    link.Append(travelerString);
                }
            }

            // Flights
            if (this.Flights != null && this.Flights.Any())
            {
                foreach (Flight flight in this.Flights)
                {
                    string flightString = "&f=" + flight.Serialize();
                    link.Append(flightString);
                }
            }

            if (link.Length > 0 && link[0] == '&')
            {
                link = link.Remove(0, 1);
            }

            string env = string.Empty;

            if (this.EnableProdMode != true)
            {
                env = "sbx-";
            }

            StringBuilder baseLink = new StringBuilder();
            baseLink.Append($"https://{env}www.bhtp.com/i");

            if (link.Length > 0)
            {
                baseLink.Append("?");
            }

            StringBuilder result = new StringBuilder();
            result.Append(baseLink.ToString());
            result.Append(link.ToString());

            return result.ToString();
        }
    }
}