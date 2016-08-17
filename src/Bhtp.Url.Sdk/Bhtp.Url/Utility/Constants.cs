namespace Bhtp.Url.Utility
{
    internal static class Constants
    {
        private static string dateFormat = "yyyy-MM-dd";

        private static string customValueDelimiter = ":";
        private static string queryStringValueDelimiter = "=";
        private static string customPairDelimeter = ";";
        private static string queryStringPairDelimeter = "&";
        private static string queryStringStart = "?";

        private static string preProdBaseUrl = "https://sbx-www.bhtp.com/i";
        private static string prodBaseUrl = "https://www.bhtp.com/i";

        private static string partner = "Partner";

        public static string DateFormat
        {
            get { return dateFormat; }
        }

        public static string CustomValueDelimiter
        {
            get { return customValueDelimiter; }
        }

        public static string QueryStringValueDelimiter
        {
            get { return queryStringValueDelimiter; }
        }

        public static string CustomPairDelimeter
        {
            get { return customPairDelimeter; }
        }

        public static string QueryStringPairDelimeter
        {
            get { return queryStringPairDelimeter; }
        }

        public static string QueryStringStart
        {
            get { return queryStringStart; }
        }

        public static string PreProdBaseUrl
        {
            get { return preProdBaseUrl; }
        }

        public static string ProdBaseUrl
        {
            get { return prodBaseUrl; }
        }

        public static string Partner
        {
            get { return partner; }
        }

        public static class AnalyticsKeys
        {
            private static string source = "utm_source";
            private static string medium = "utm_medium";
            private static string campaign = "campaign";
            private static string package = "package";

            public static string Source
            {
                get { return source; }
            }

            public static string Medium
            {
                get { return medium; }
            }

            public static string Campaign
            {
                get { return campaign; }
            }

            public static string Package
            {
                get { return package; }
            }
        }

        public static class TravelerKeys
        {
            private static string birthDate = "db";
            private static string age = "a";
            private static string tripCost = "tc";

            public static string BirthDate
            {
                get { return birthDate; }
            }

            public static string Age
            {
                get { return age; }
            }

            public static string TripCost
            {
                get { return tripCost; }
            }
        }

        public static class FlightKeys
        {
            private static string departureDate = "d";
            private static string flightNumber = "n";
            private static string airlineCode = "ac";
            private static string departureAirportCode = "da";
            private static string arrivalAirportCode = "aa";

            public static string DepartureDate
            {
                get { return departureDate; }
            }

            public static string FlightNumber
            {
                get { return flightNumber; }
            }

            public static string AirlineCode
            {
                get { return airlineCode; }
            }

            public static string DepartureAirportCode
            {
                get { return departureAirportCode; }
            }

            public static string ArrivalAirportCode
            {
                get { return arrivalAirportCode; }
            }
        }

        public static class TripKeys
        {
            private static string destinationCountryIsoCode2 = "dc";
            private static string residence = "rs";
            private static string departureDate = "dd";
            private static string returnDate = "rd";
            private static string initialPaymentDate = "pd";
            private static string policyHolderEmail = "e";
            private static string totalTravelerCount = "tt";
            private static string policyHolder = "ph";
            private static string travelers = "t";
            private static string flights = "f";

            public static string DestinationCountryIsoCode2
            {
                get { return destinationCountryIsoCode2; }
            }

            public static string Residence
            {
                get { return residence; }
            }

            public static string DepartureDate
            {
                get { return departureDate; }
            }

            public static string ReturnDate
            {
                get { return returnDate; }
            }

            public static string InitialPaymentDate
            {
                get { return initialPaymentDate; }
            }

            public static string PolicyHolderEmail
            {
                get { return policyHolderEmail; }
            }

            public static string TotalTravelerCount
            {
                get { return totalTravelerCount; }
            }

            public static string PolicyHolder
            {
                get { return policyHolder; }
            }

            public static string Travelers
            {
                get { return travelers; }
            }

            public static string Flights
            {
                get { return flights; }
            }
        }
    }
}