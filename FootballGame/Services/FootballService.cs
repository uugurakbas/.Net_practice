using System.Net.Http;
using System.Threading.Tasks;
 namespace FootballGame.Services;
public class FootballService
{
    private readonly HttpClient _httpClient;

    public FootballService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetCompetitionsAsync()
    {
        var apiUrl = "http://api.football-data.org/v4/competitions?plan=TIER_ONE";
        var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
        request.Headers.Add("X-Auth-Token", "5c4b605fee49455ca1a928c422c51cb6"); // API anahtarını buraya ekle

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseData = await response.Content.ReadAsStringAsync();
        return responseData;
    }
}
