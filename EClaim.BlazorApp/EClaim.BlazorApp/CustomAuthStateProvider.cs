using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _js;
    private ClaimsPrincipal _cachedUser = new ClaimsPrincipal(new ClaimsIdentity());

    public CustomAuthStateProvider(IJSRuntime js)
    {
        _js = js;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Return the cached user (initially unauthenticated)
        return Task.FromResult(new AuthenticationState(_cachedUser));
    }

    public async Task TryInitializeAsync()
    {
        try
        {
            var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");

            if (!string.IsNullOrWhiteSpace(token))
            {
                var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
                _cachedUser = new ClaimsPrincipal(identity);
            }
            else
            {
                _cachedUser = new ClaimsPrincipal(new ClaimsIdentity());
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_cachedUser)));
        }
        catch
        {
            _cachedUser = new ClaimsPrincipal(new ClaimsIdentity());
        }
    }

    public void NotifyUserAuthentication(string token)
    {
        var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
        var user = new ClaimsPrincipal(identity);

        // Notify the system about the updated user
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void NotifyUserLogout()
    {
        _cachedUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_cachedUser)));
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);
        return token.Claims;
    }
}
