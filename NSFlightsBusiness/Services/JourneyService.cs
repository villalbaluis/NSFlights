using NSFlightsBusiness.Entities;
namespace NSFlightsBusiness
{
    public class JourneyService
    {
        private readonly FlightApiClient _flightApiClient;

        public JourneyService(FlightApiClient flightApiClient)
        {
            _flightApiClient = flightApiClient;
        }

        public async Task<Journey> GetJourneyAsync(string origin, string destination, string apiUrl)
        {
            try
            {
                List<Flight> _lsFlights = new List<Flight>();

                var flights = await _flightApiClient.GetFlightsAsync(apiUrl);
                flights.ToList().ForEach(flight =>
                {

                    Flight fl = new Flight()
                    {
                        Transport = new Transport()
                        {
                            FlightCarrier = flight.flightCarrier,
                            FlightNumber = flight.flightNumber
                        },
                        Origin = flight.departureStation,
                        Destination = flight.arrivalStation,
                        Price = Convert.ToDouble(flight.price)
                    };
                    _lsFlights.Add(fl);
                });

                var journeyFlights = FindJourneyFlights(_lsFlights, origin, destination);

                if (journeyFlights.Any())
                {
                    var totalPrice = journeyFlights.Sum(f => f.Price);

                    var journey = new Journey
                    {
                        Flights = journeyFlights,
                        Origin = origin,
                        Destination = destination,
                        Price = totalPrice
                    };

                    return journey;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private List<Flight> FindJourneyFlights(List<Flight> flights, string origin, string destination)
        {
            return flights.Where(f => f.Origin == origin && f.Destination == destination).ToList();
        }
    }
}
