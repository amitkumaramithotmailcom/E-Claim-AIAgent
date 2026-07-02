using Microsoft.Extensions.Configuration;

namespace Services;

public class UserApiClient
{
  private readonly HttpClient _client;

  public UserApiClient(IConfiguration configuration)
  {
    _client = new HttpClient();
    var baseUrl = configuration["ApiBaseUrl"] ?? "http://localhost:5064/";
    _client.BaseAddress = new Uri(baseUrl);
  }


  public async Task<string> GetUser(int id)
  {
    var response =
      await _client.GetAsync(
      $"api/Users/{id}");

    return await response.Content.ReadAsStringAsync();
  }

  public async Task<string> GetUsers()
  {
    var response =
      await _client.GetAsync(
      $"api/Users/GetUsers");

    return await response.Content.ReadAsStringAsync();
  }
}