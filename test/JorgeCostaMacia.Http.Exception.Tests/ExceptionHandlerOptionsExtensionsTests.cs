using System.Net;
using FluentValidation.Results;
using JorgeCostaMacia.DDD.Exception.Domain;
using JorgeCostaMacia.Http.Exception.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace JorgeCostaMacia.Http.Exception.Tests;

file sealed class TestNotFoundException : NotFoundException
{
    public TestNotFoundException() : base(
        null,
        null,
        null,
        null,
        null,
        "missing",
        null
    )
    { }
}

file sealed class TestExistException : ExistException
{
    public TestExistException() : base(
        null,
        null,
        null,
        null,
        null,
        "duplicate",
        null
    )
    { }
}

file sealed class TestValidationException : ValidationException
{
    public TestValidationException() : base(
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        new List<ValidationFailure>()
    )
    { }
}

public sealed class ExceptionHandlerOptionsExtensionsTests
{
    private static async Task<HttpResponseMessage> Request(System.Exception exception)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        builder.WebHost.UseTestServer();
        builder.Logging.ClearProviders();
        builder.Services.AddProblemDetails();

        await using WebApplication app = builder.Build();
        app.UseExceptionHandler(new ExceptionHandlerOptions().WithDefaultStatusCodes());
        app.MapGet("/throw", () => { throw exception; });

        await app.StartAsync(TestContext.Current.CancellationToken);

        return await app.GetTestClient().GetAsync("/throw", TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task WithDefaultStatusCodes_WithANotFoundException_Returns404()
        => Assert.Equal(HttpStatusCode.NotFound, (await Request(new TestNotFoundException())).StatusCode);

    [Fact]
    public async Task WithDefaultStatusCodes_WithAnExistException_Returns409()
        => Assert.Equal(HttpStatusCode.Conflict, (await Request(new TestExistException())).StatusCode);

    [Fact]
    public async Task WithDefaultStatusCodes_WithADomainValidationException_Returns400()
        => Assert.Equal(HttpStatusCode.BadRequest, (await Request(new TestValidationException())).StatusCode);

    [Fact]
    public async Task WithDefaultStatusCodes_WithAFluentValidationException_Returns400()
        => Assert.Equal(
            HttpStatusCode.BadRequest,
            (await Request(new FluentValidation.ValidationException(new List<ValidationFailure>()))).StatusCode);

    [Fact]
    public async Task WithDefaultStatusCodes_WithABadHttpRequestException_ReturnsItsOwnStatusCode()
        => Assert.Equal(
            HttpStatusCode.UnsupportedMediaType,
            (await Request(new BadHttpRequestException(
                "bad",
                StatusCodes.Status415UnsupportedMediaType))).StatusCode);

    [Fact]
    public async Task WithDefaultStatusCodes_WithAnUnauthorizedAccessException_Returns403()
        => Assert.Equal(HttpStatusCode.Forbidden, (await Request(new UnauthorizedAccessException())).StatusCode);

    [Fact]
    public async Task WithDefaultStatusCodes_WithANotImplementedException_Returns501()
        => Assert.Equal(HttpStatusCode.NotImplemented, (await Request(new NotImplementedException())).StatusCode);

    [Fact]
    public async Task WithDefaultStatusCodes_WithAnOperationCanceledException_Returns499()
        => Assert.Equal((HttpStatusCode)499, (await Request(new OperationCanceledException())).StatusCode);

    [Fact]
    public async Task WithDefaultStatusCodes_WithATaskCanceledException_Returns499ViaItsOperationCanceledBase()
        => Assert.Equal((HttpStatusCode)499, (await Request(new TaskCanceledException())).StatusCode);

    [Fact]
    public async Task WithDefaultStatusCodes_WithAnUnexpectedException_Returns500()
        => Assert.Equal(
            HttpStatusCode.InternalServerError,
            (await Request(new InvalidOperationException("crash"))).StatusCode);
}
