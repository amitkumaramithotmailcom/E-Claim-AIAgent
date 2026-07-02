using Microsoft.Extensions.Configuration;

namespace Services;

public class ClaimApiClient
{
  private readonly HttpClient _client;

  public ClaimApiClient(IConfiguration configuration)
  {
    _client = new HttpClient();
    var baseUrl = configuration["ApiBaseUrl"] ?? "http://localhost:5064/";
    _client.BaseAddress = new Uri(baseUrl);
  }


  public async Task<string> GetClaim(int id)
  {
    var response =
      await _client.GetAsync(
      $"api/Claim?id={id}");

    return await response.Content.ReadAsStringAsync();
  }

  public async Task<string> GetClaims()
  {
    var response =
      await _client.GetAsync(
      $"api/Claim/GetClaimDetails");

    return await response.Content.ReadAsStringAsync();
  }
}