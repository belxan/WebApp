﻿namespace Application.DTOs.Auth;

public record LoginRequest(string Email, string Password);
public record RegisterRequest(string Email, string Password);
public record AuthResponse(string Token, string Email);