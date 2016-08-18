using System;

namespace Bhtp.Url.Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Bhtp.Url.Link data = new Bhtp.Url.Link("AA0057", "TestPromo", "ExactCare", enableProdMode: false);

            // Trip
            data.Trip.DestinationCountryIsoCode2 = "GB";
            data.Trip.ResidencePostalCode = "54481";
            data.Trip.DepartureDate = new DateTime(2016, 9, 24);
            data.Trip.ReturnDate = new DateTime(2016, 10, 10);
            data.Trip.InitialPaymentDate = new DateTime(2016, 6, 15);
            data.Trip.PolicyHolderEmail = "sherlock.holmes@bhtp.com";
            data.Trip.TotalTravelerCount = 3;

            // Flights
            data.AddFlight(new Bhtp.Url.Flight(new DateTime(2016, 9, 24), 1234, "DL", "PNS", "ATL"));
            data.AddFlight(new Bhtp.Url.Flight(new DateTime(2016, 9, 27), 2665, "AA", "ATL", "LAX"));

            // Policyholder
            data.PolicyHolder.TripCost = 100m;
            data.PolicyHolder.Age = 34;

            // Travelers
            Bhtp.Url.Traveler traveler1 = new Bhtp.Url.Traveler();
            traveler1.TripCost = 100m;
            traveler1.Age = 28;
            data.AddTraveler(traveler1);

            Bhtp.Url.Traveler traveler2 = new Bhtp.Url.Traveler(200m);
            traveler2.BirthDate = new DateTime(1986, 9, 7);
            data.AddTraveler(traveler2);

            // Generate the link
            string link = data.GenerateLink();

            Console.WriteLine(link);
            Console.WriteLine("\n\nPress any key to exit...");
            Console.ReadLine();
        }
    }
}
