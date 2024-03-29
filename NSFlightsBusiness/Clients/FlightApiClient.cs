﻿using Newtonsoft.Json;
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
                    // return the result of the API response into a JSON Object
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
