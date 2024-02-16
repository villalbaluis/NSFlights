using NSFlightsBusiness.Entities;

namespace NSFlightsDataAccess
{
    public interface IJourneyRepository
    {
        Task<Journey> GetJourneyAsync(string origin, string destination);
        Task SaveJourneyAsync(Journey journey);
    }
}
