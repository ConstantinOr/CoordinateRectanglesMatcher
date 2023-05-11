using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace CoordinateRectanglesMatcher;

public class BasicAuthenticationOptions : AuthenticationSchemeOptions
{
}

public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
{
    private readonly ICustomAuthenticationManager _customAuthenticationManager;

    public BasicAuthenticationHandler(
        IOptionsMonitor<BasicAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        ICustomAuthenticationManager customAuthenticationManager) :
        base(options, logger, encoder, clock)
    {
        _customAuthenticationManager = customAuthenticationManager;
    }
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.Fail("Unauthorized");
 
        string authorizationHeader = Request.Headers["Authorization"];
        if (string.IsNullOrEmpty(authorizationHeader))
        {
            return AuthenticateResult.NoResult();
        }
 
        if (!authorizationHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
        {
            return AuthenticateResult.Fail("Unauthorized");
        }
 
        string token = authorizationHeader.Substring("bearer".Length).Trim();
 
        if (string.IsNullOrEmpty(token))
        {
            return AuthenticateResult.Fail("Unauthorized");
        }
 
        try
        {
            return validateToken(token);
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail(ex.Message);
        }
    }
 
    private AuthenticateResult validateToken(string token)
    {
        var validatedToken = _customAuthenticationManager.Tokens.FirstOrDefault(t => t.Key == token);
        if (validatedToken.Key == null)
        {
            return AuthenticateResult.Fail("Unauthorized");
        }
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, validatedToken.Value),
        };
 
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new System.Security.Principal.GenericPrincipal(identity, new []{"admin"});
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return AuthenticateResult.Success(ticket);
    }

}