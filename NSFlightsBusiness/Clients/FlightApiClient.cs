using Newtonsoft.Json;
using NSFlightsBusiness.Entities;
using NSFlightsBusiness.Models;

namespace NSFlightsBusiness
{
    public class FlightApiClient
    {
        private readonly HttpClient _httpClient;

        public FlightApiClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<FlightsDto>> GetFlightsAsync(string apiUrl)
        {
            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ///var flightResponse = JsonConvert.DeserializeObject<FlightsDto>(content);
                    return JsonConvert.DeserializeObject<List<FlightsDto>>(content)!;
                }
                else
                {
                    throw new Exception($"Bad Request. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error with the API request, error: {ex}");
            }
        }
    }
}
