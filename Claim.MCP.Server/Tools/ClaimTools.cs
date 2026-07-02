using ModelContextProtocol.Server;
using Services;

[McpServerToolType]
public class ClaimTools
{

    private readonly ClaimApiClient _api;


    public ClaimTools(
        ClaimApiClient api)
    {
        _api = api;
    }


    [McpServerTool]
    public async Task<string> GetClaim(
        int claimId)
    {
        return await _api.GetClaim(claimId);
    }

    [McpServerTool]
    public async Task<string> GetClaims()
    {
        return await _api.GetClaims();
    }

}