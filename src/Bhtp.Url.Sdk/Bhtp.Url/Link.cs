using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bhtp.Url
{
    /// <summary>
    /// 
    /// </summary>
    public class Link
    {

        /// <summary>
        /// 
        /// </summary>
        public bool EnableProdMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AgentCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CampaignId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Trip Trip { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Flight> Flights { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Traveler> Travelers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Traveler PolicyHolder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentCode"></param>
        /// <param name="campaignId"></param>
        /// <param name="productId"></param>
        /// <param name="enableProdMode"></param>
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
        /// 
        /// </summary>
        /// <param name="flight"></param>
        public void AddFlight(Flight flight)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="traveler"></param>
        public void AddTraveler(Traveler traveler)
        {

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
            string tripString = this.Trip.Serialize();

            if (tripString.Length > 0 && link.Length > 0)
            {
                link.Append("&");
            }

            link.Append(tripString);

            // Flights
            if (this.Flights != null && this.Flights.Any())
            {
                foreach (Flight flight in this.Flights)
                {
                    string flightString = "&f=" + flight.Serialize();
                    link.Append(flightString);
                }
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

            if (link[0] == '&')
            {
                link = link.Remove(0, 1);
            }

            string env = "";

            if (this.EnableProdMode != true)
            {
                env = "sbx-";
            }

            StringBuilder baseLink = new StringBuilder();
            baseLink.Append($"https://${env}www.bhtp.com/i");

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