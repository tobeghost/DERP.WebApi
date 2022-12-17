namespace DERP.WebApi.Domain.Dtos.Login;

public class SignInResponse
{
    public string Username { get; set; }
    public string Token { get; set; }
    public int ExpiryInMinutes { get; set; }
}