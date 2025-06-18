using System.Text;
using System.Text.Json;
using Application.DTOs.FlightSearch;

namespace Infrastructure.Services;

public class FlightSearchService
{
    private readonly HttpClient _httpClient;
    private readonly SignatureService _signatureService;
    private const string Token = "3f0ccd05806df92e6ea6ba8ed700fa8a";
    private const string maker = "638462";

    public FlightSearchService(HttpClient httpClient, SignatureService signatureService)
    {
        _httpClient = httpClient;
        _signatureService = signatureService;
    }

    public async Task<string> SearchRoundTripAsync(RoundTripRequest request)
    {
        request.marker = maker;
        request.signature = _signatureService.GenerateSignature(Token, request);
        
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("https://api.travelpayouts.com/v1/flight_search", content);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}
