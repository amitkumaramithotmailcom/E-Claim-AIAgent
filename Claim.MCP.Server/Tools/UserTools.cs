using ModelContextProtocol.Server;
using Services;

[McpServerToolType]
public class UserTools
{

    private readonly UserApiClient _api;


    public UserTools(
        UserApiClient api)
    {
        _api = api;
    }


    [McpServerTool]
    public async Task<string> GetUser(
        int userId)
    {
        return await _api.GetUser(userId);
    }

    [McpServerTool]
    public async Task<string> GetUsers()
    {
        return await _api.GetUsers();
    }

}