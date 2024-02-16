using NSFlightsBusiness.Entities;

namespace NSFlightsDataAccess
{
    public class JourneyRepository : IJourneyRepository
    {
        private readonly DataContext _dbContext;

        public JourneyRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Journey> GetJourneyAsync(string origin, string destination)
        {
            throw new NotImplementedException();
        }

        public async Task SaveJourneyAsync(Journey journey)
        {
            _dbContext.Journeys.Add(journey);
            await _dbContext.SaveChangesAsync();
        }
    }
}
