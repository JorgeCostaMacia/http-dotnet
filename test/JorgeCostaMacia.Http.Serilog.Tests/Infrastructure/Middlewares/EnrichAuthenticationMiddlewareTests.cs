using JorgeCostaMacia.Http.Serilog.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Http;

namespace JorgeCostaMacia.Http.Serilog.Tests.Infrastructure.Middlewares;

public sealed class EnrichAuthenticationMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_WithAnAnonymousUser_CallsNext()
    {
        bool nextCalled = false;
        EnrichAuthenticationMiddleware middleware = new EnrichAuthenticationMiddleware(context =>
        {
            nextCalled = true;

            return Task.CompletedTask;
        });

        await middleware.InvokeAsync(new DefaultHttpContext());

        Assert.True(nextCalled);
    }
}
