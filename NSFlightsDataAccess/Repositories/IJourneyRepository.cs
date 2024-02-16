using NSFlightsBusiness.Entities;

namespace NSFlightsDataAccess
{
    public interface IJourneyRepository
    {
        // Interface to save and get the data of the Flights previously requested
        Task<Journey> GetJourneyAsync(string origin, string destination);
        Task SaveJourneyAsync(Journey journey);
    }
}
