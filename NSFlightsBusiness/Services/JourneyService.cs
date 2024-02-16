using NSFlightsBusiness.Entities;
using System.Collections.Generic;
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
                // List to save all the elements with the entities structure
                List<Flight> _lsFlights = new List<Flight>();

                // Get the data from the NSApi
                var flights = await _flightApiClient.GetFlightsAsync(apiUrl);

                // Loop trough all elements of the response
                // to set the information into the entitie structure.
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

                // Find the flights wich match with the info given in the request.
                var journeyFlights = FindJourneyFlights(_lsFlights, origin, destination);

                // If there's any Flight matched, sum all the prices, and
                // return an object with the Journey structure.
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
                Console.WriteLine($"Error in the execution of the API, error: {ex.Message}");
                return null;
            }
        }

        private List<Flight> FindJourneyFlights(List<Flight> flights, string origin, string destination)
        {

            try
            {
                var result = new List<Flight>();

                // Filter to find all the Direct Flights with the parameters given.
                var directFlights = flights.Where(f => f.Origin == origin && f.Destination == destination).ToList();

                // If there any direct Flight, add to the list of Flights
                if (directFlights.Any())
                {
                    result.AddRange(directFlights);
                }
                else
                {
                    // In case there's no Direct Flights, take a loop trough all possible intermediate routes.
                    // First of all, take the Flights wich Origin is the same as given.
                    var possibleRoutes = flights.Where(f => f.Origin == origin).ToList();

                    foreach (var route in possibleRoutes)
                    {
                        var intermediateFlights = FindIntermediateFlights(flights, route.Destination!, destination);

                        // If there's any intermediate routes, add to the scope.
                        if (intermediateFlights.Any())
                        {
                            result.Add(route);
                            result.AddRange(intermediateFlights);
                            break;
                        }
                    }
                }

                return result;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return null;
            }
        }

        private List<Flight> FindIntermediateFlights(List<Flight> flights, string currentDestination, string finalDestination)
        {
            var result = new List<Flight>();
            var visitedRoutes = new HashSet<string>(); // Avoid duplicate routes

            var currentLocation = currentDestination;

            while (currentLocation != finalDestination)
            {
                // Var for save the already listed route
                var currentRoute = $"{currentLocation}-{finalDestination}";

                if (visitedRoutes.Contains(currentRoute))
                {
                    break;
                }

                // Add the route to the routes already visited
                visitedRoutes.Add(currentRoute);

                // Find any direct fligths from the origin to the final destination
                var directFlights = flights
                    .Where(f => 
                        f.Origin == currentLocation && 
                        f.Destination == finalDestination 
                        && !result.Contains(f)
                    ).ToList();

                // Add direct flights to the result set
                if (directFlights.Any())
                {
                    result.AddRange(directFlights);
                    break;
                }

                // Search for intermediate routes, with the same Origin and Location
                var possibleRoutes = flights
                    .Where(f => f.Origin == currentLocation && !result.Contains(f))
                    .ToList();

                if (!possibleRoutes.Any())
                {
                    break;
                }

                // Take the first route to avoid duplicate
                var nextRoute = possibleRoutes.First();

                // Add the nextRoute to the result and set the new location.
                result.Add(nextRoute);
                currentLocation = nextRoute.Destination;
            }

            return result;
        }

    }
}
