using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Domain.Interfaces.TokenServices;

namespace TicTacToe.Application.Services.Token;

public class TokenBlacklistService(IMemoryCache cache) : ITokenBlacklistService
{
    private readonly IMemoryCache _cache = cache;
    
    public Task AddToBlacklistAsync(string token)
    {
        _cache.Set(token, true, TimeSpan.FromHours(1));
        return Task.CompletedTask;
    }

    public Task<bool> IsTokenBlacklistedAsync(string token)
    {
        return Task.FromResult(_cache.TryGetValue(token, out _));
    }
}