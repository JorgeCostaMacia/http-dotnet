using System.Text.Json;
using FluentValidation.Results;
using JorgeCostaMacia.DDD.Exception.Domain;
using JorgeCostaMacia.Http.ProblemDetails.Infrastructure.Handlers;
using Microsoft.AspNetCore.Http;

namespace JorgeCostaMacia.Http.ProblemDetails.Tests.Infrastructure.Handlers;

file sealed class TestDomainException : DomainException
{
    public TestDomainException(string? message = null) : base(
        null,
        null,
        null,
        null,
        null,
        message,
        null
    )
    { }
}

file sealed class TestValidationException : ValidationException
{
    public TestValidationException(IEnumerable<ValidationFailure> validations) : base(
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        validations
    )
    { }
}

public sealed class DomainExceptionHandlerTests
{
    private static Microsoft.AspNetCore.Http.ProblemDetailsContext Context()
        => new Microsoft.AspNetCore.Http.ProblemDetailsContext() { HttpContext = new DefaultHttpContext() };

    [Fact]
    public void Handle_WithADomainException_AddsAggregateExtensionsWithConvertedKeysAndRawTypeValue()
    {
        Microsoft.AspNetCore.Http.ProblemDetailsContext ctx = Context();
        TestDomainException exception = new TestDomainException("boom");

        DomainExceptionHandler.Handle(ctx, exception, JsonNamingPolicy.CamelCase);

        Assert.Equal(exception.AggregateId, ctx.ProblemDetails.Extensions["aggregateId"]);
        Assert.Equal(exception.AggregateCode, ctx.ProblemDetails.Extensions["aggregateCode"]);
        // the VALUE travels raw, keys are camelCased
        Assert.Equal("DomainException", ctx.ProblemDetails.Extensions["aggregateType"]);
    }

    [Fact]
    public void Handle_WithANonValidationException_KeepsErrorsPresentAndNull()
    {
        Microsoft.AspNetCore.Http.ProblemDetailsContext ctx = Context();

        DomainExceptionHandler.Handle(ctx, new TestDomainException("boom"), JsonNamingPolicy.CamelCase);

        Assert.True(ctx.ProblemDetails.Extensions.ContainsKey("errors"));
        Assert.Null(ctx.ProblemDetails.Extensions["errors"]);
    }

    [Fact]
    public void Handle_WithAValidationException_GroupsErrorsByPropertyNameRewritingMessages()
    {
        Microsoft.AspNetCore.Http.ProblemDetailsContext ctx = Context();
        TestValidationException exception = new TestValidationException(
            new ValidationFailure[] { new ValidationFailure("Name", "'Name' is required.") });

        DomainExceptionHandler.Handle(ctx, exception, JsonNamingPolicy.CamelCase);

        Dictionary<string, string[]> errors =
            Assert.IsType<Dictionary<string, string[]>>(ctx.ProblemDetails.Extensions["errors"]);
        Assert.Equal("'name' is required.", Assert.Single(errors["name"]));
    }

    [Fact]
    public void Handle_WithPropertiesThatCollideUnderThePolicy_MergesTheirErrors()
    {
        // "Id" and "ID" both camelCase to "id": they must merge, not blow up ToDictionary.
        Microsoft.AspNetCore.Http.ProblemDetailsContext ctx = Context();
        TestValidationException exception = new TestValidationException(
            new ValidationFailure[] { new ValidationFailure("Id", "first"), new ValidationFailure("ID", "second") });

        DomainExceptionHandler.Handle(ctx, exception, JsonNamingPolicy.CamelCase);

        Dictionary<string, string[]> errors =
            Assert.IsType<Dictionary<string, string[]>>(ctx.ProblemDetails.Extensions["errors"]);
        Assert.Equal(2, errors["id"].Length);
    }
}
