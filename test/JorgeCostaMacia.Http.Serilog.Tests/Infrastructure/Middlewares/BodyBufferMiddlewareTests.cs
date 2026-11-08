using JorgeCostaMacia.Http.Serilog.Infrastructure.Middlewares;
using JorgeCostaMacia.Http.Serilog.Tests.Support;
using Microsoft.AspNetCore.Http;

namespace JorgeCostaMacia.Http.Serilog.Tests.Infrastructure.Middlewares;

public sealed class BodyBufferMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_WithANonSeekableBody_MakesItSeekableThenCallsNext()
    {
        bool nextCalled = false;
        BodyBufferMiddleware middleware = new BodyBufferMiddleware(context =>
        {
            nextCalled = true;

            return Task.CompletedTask;
        });
        DefaultHttpContext httpContext = new DefaultHttpContext();
        httpContext.Request.Body = new NonSeekableStream();

        await middleware.InvokeAsync(httpContext);

        Assert.True(httpContext.Request.Body.CanSeek);
        Assert.True(nextCalled);
    }
}
