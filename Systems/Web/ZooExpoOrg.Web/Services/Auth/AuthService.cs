﻿using System.Net.Http.Headers;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using ZooExpoOrg.Web.Providers;

namespace ZooExpoOrg.Web.Services.Auth;

public class AuthService : IAuthService
{
    private const string LocalStorageAuthTokenKey = "authToken";
    private const string LocalStorageRefreshTokenKey = "refreshToken";
    
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthService(HttpClient httpClient,
                       AuthenticationStateProvider authenticationStateProvider,
                       ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
        _localStorage = localStorage;
    }

    public async Task<LoginResult> Login(LoginModel loginModel)
    {
        var url = $"{Settings.IdentityRoot}/connect/token";

        var request_body = new[] 
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", Settings.ClientId),
            new KeyValuePair<string, string>("client_secret", Settings.ClientSecret),
            new KeyValuePair<string, string>("username", loginModel.UserName!),
            new KeyValuePair<string, string>("password", loginModel.Password!)
        };

        var requestContent = new FormUrlEncodedContent(request_body);

        var response = await _httpClient.PostAsync(url, requestContent);

        var content = await response.Content.ReadAsStringAsync();

        var loginResult = JsonSerializer.Deserialize<LoginResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new LoginResult();
        
        if (!response.IsSuccessStatusCode)
        {
            loginResult.Successful = false;
            loginResult.ErrorMesage = "Authorization error.";

            return loginResult;
        }

        loginResult.Successful = true;

        await _localStorage.SetItemAsync(LocalStorageAuthTokenKey, loginResult.AccessToken);
        await _localStorage.SetItemAsync(LocalStorageRefreshTokenKey, loginResult.RefreshToken);

        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.UserName!);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.AccessToken);

        return loginResult;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync(LocalStorageAuthTokenKey);
        await _localStorage.RemoveItemAsync(LocalStorageRefreshTokenKey);

        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();

        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
}