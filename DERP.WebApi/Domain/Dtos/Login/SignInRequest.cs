﻿namespace DERP.WebApi.Domain.Dtos.Login;

public class SignInRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}