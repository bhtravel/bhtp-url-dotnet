# bhtp-url-dotnet
A package to integrate with Berkshire Hathaway Travel Protection's consumer website.

## Things to Know
- Any parameters that do not have a value, will not be added to the integration url when using this package
- All dates follow [ISO 8601](https://en.wikipedia.org/wiki/ISO_8601#Dates) standard
- All countries and states follow [ISO 3166-2](https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2) standard
- All flight information is identified by [IATA codes](https://en.wikipedia.org/wiki/International_Air_Transport_Association)

# API

This library is a Portable Class Library (PCL), targeting .NET 4.5, .NET Core 1.0, Windows Apps, and Xamarin iOS & Android

## The **Link** object
The link object holds all information about the integration and is the top level object in this package. It contains the following:

```csharp
public Link(string agentCode, string campaignId, string productId, bool enableProdMode = false);

public string AgentCode { get; set; }
public string CampaignId { get; set; }
public string ProductId { get; set; }

public bool EnableProdMode { get; private set; }

public Trip Trip { get; set; }
public ICollection<Flight> Flights { get; set; }
public ICollection<Traveler> Travelers { get; set; }
public Traveler PolicyHolder { get; set; }

public void AddFlight(Flight flight);
public void AddTraveler(Traveler traveler);

public string GenerateLink();
```
The Link constructor accepts the AgentCode, CampaignId, ProductId, and optionally EnableProdMode. EnableProdMode is set to `false` by default.

This information is used to identify the integration

- **AgentCode**: the id of the agent who is referring the user to the site. Used to track commission.
- **CampaignId**: an optional id allowing the integrating system to uniquely identify where there the reference is coming from.
- **ProductId**: if a specific product is requested, it can be specified here.
- **EnableProdMode**: when finished testing, set this to true via the Link constructor.

This is the insurable information

- **Trip**: information about the trip to insure. Description of the model is below.
- **Flights**: information about the flights to insure. Description of the model is below.
- **Travelers**: information about the travelers to insure -- this does not include the policyholder. Description of the model is below.
- **Policyholder**: information about the traveler holding the policy. Description of the model is below.

#### Generating a useable link

```csharp
Bhtp.Url.Link data = new Bhtp.Url.Link("AA0057", "TestPromo", "ExactCare", enableProdMode: false);

// Trip
data.Trip.DestinationCountryIsoCode2 = "GB";
data.Trip.ResidencePostalCode = "54481";
data.Trip.DepartureDate = new DateTime(2016, 9, 24);
data.Trip.ReturnDate = new DateTime(2016, 10, 10);
data.Trip.InitialPaymentDate = new DateTime(2016, 6, 15);
data.Trip.PolicyholderEmail = "sherlock.holmes@bhtp.com";
data.Trip.TotalTravelerCount = 3;

// Flights
data.AddFlight(new Bhtp.Url.Flight(new DateTime(2016, 9, 24), 1234, "DL", "PNS", "ATL"));
data.AddFlight(new Bhtp.Url.Flight(new DateTime(2016, 9, 27), 2665, "AA", "ATL", "LAX"));

// Policyholder
data.Policyholder.TripCost = 100m;
data.Policyholder.Age = 34;

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

// https://sbx-www.bhtp.com/i?utm_source=AAgent&utm_medium=Partner&campaign=TestPromo&package=ExactCare&dc=GB&rs=54481&dd=2016-09-24&rd=2016-10-10&pd=2016-06-15&e=sherlock.holmes@bhtp.com&tt=3&f=d:2016-09-24;n:1234;ac:DL;da:PNS;aa:ATL&f=d:2016-09-27;n:2665;ac:AA;da:ATL;aa:LAX&ph=a:34;tc:100&t=a:28;tc:100&t=db:1986-09-07;tc:200
```

The constructor will create a new link object and it will initialize the flight and traveler arrays, and the trip and policyholder object.

---

## The **Trip** object
The trip object holds all information about the trip to insure. It contains the following:

```csharp
public string DestinationCountryIsoCode2 { get; set; }
public string ResidenceStateIsoCode2 { get; set; }
public string ResidencePostalCode { get; set; }

public DateTime? DepartureDate { get; set; }
public DateTime? ReturnDate { get; set; }
public DateTime? InitialPaymentDate { get; set; }

public string PolicyHolderEmail { get; set; }
public int? TotalTravelerCount { get; set; }
```

- **DestinationCountryIsoCode2**: The [ISO 3166-2](https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2) code for the destination country that will be visited. If there is more than one destination country, pass only one.
- **ResidenceStateIsoCode2**: The [ISO 3166-2:US](https://en.wikipedia.org/wiki/ISO_3166-2:US) code for the US state of residence without the US- portion in the beginning (Example: Wisconson = WI). If ResidencePostalCode is supplied, this will be ignored.
- **ResidencePostalCode**: The postal code of the US address of residence of the policyholder. This takes precendence over ResidenceStateIsoCode2

- **DepartureDate**: The local date of departure in [ISO 8601](https://en.wikipedia.org/wiki/ISO_8601#Dates) format.
- **ReturnDate**: The local date of return from the trip in [ISO 8601](https://en.wikipedia.org/wiki/ISO_8601#Dates) format.
- **InitialPaymentDate**: The local date the first payment toward the trip was made in [ISO 8601](https://en.wikipedia.org/wiki/ISO_8601#Dates) format.

- **PolicyholderEmail**: the email of the policyholder
- **TotalTravelerCount**: An optional field identifying how many travelers, **including the policyholder**, that will be on the policy. This may be omitted in lieu of specifying a policyholder and travelers which are documented below.

---

## The **Traveler** and **Policyholder** objects
The traveler object holds all information about the traveler to insure. It contains the following:

```csharp
public Traveler(decimal? tripCost = null);

public int? Age { get; set; }
public DateTime? BirthDate { get; set; }
public decimal? TripCost { get; set; }
```
The Traveler constructor optionally accepts a trip cost, defaulted to null if no parameter is supplied.

- **BirthDate**: the date of birth of the traveler in [ISO 8601](https://en.wikipedia.org/wiki/ISO_8601#Dates) format.
- **Age**: The age of the traveler. If the BirthDate is specified, this value is ignored.
- **TripCost**: The cost of the trip for this traveler in US dollars.

---

## The **Flight** object
The flight object holds all information about the flights to insure. This is for AirCare products only. It contains the following:

```csharp
public Flight(DateTime? departureDate, int? flightNumber, string airlineCode, string departureAirportCode, string arrivalAirportCode);

public DateTime? DepartureDate { get; set; }
public int? FlightNumber { get; set; }
public string AirlineCode { get; set; }
public string DepartureAirportCode { get; set; }
public string ArrivalAirportCode { get; set; }
```

The Flight constructor accepts the departure date, flight number, airline code, departure airport code, and arrival airport code;

- **departureDate**: the date of the flight's departure in [ISO 8601](https://en.wikipedia.org/wiki/ISO_8601#Dates) format.
- **flightNumber**: The number of the flight.
- **airlineCode**: The [IATA code](http://www.iata.org/about/members/Pages/airline-list.aspx?All=true) of the airline that is servicing the flight (Example: Delta Air Lines Inc. = DL).
- **departureAirportCode**: The [IATA code](https://www.world-airport-codes.com/) of the airport the flight departs from (Example: O'Hare International Airport = ORD).
- **arrivalAirportCode**: The [IATA code](https://www.world-airport-codes.com/) of the airport the flight arrives at.