using Asp.Versioning;
using JorgeCostaMacia.Http.MinimalApi.Versioning.Infrastructure;

namespace JorgeCostaMacia.Http.MinimalApi.Versioning.Tests.Infrastructure;

public sealed class ApiVersioningOptionsExtensionsTests
{
    [Fact]
    public void WithDefaults_OnNewOptions_ReturnsTheSameInstanceForChaining()
    {
        ApiVersioningOptions options = new ApiVersioningOptions();

        Assert.Same(options, options.WithDefaults(2));
    }

    [Fact]
    public void WithDefaults_WithAMajorVersion_ConfiguresUrlSegmentVersioningFromIt()
    {
        ApiVersioningOptions options = new ApiVersioningOptions().WithDefaults(2);

        Assert.Equal(new ApiVersion(2), options.DefaultApiVersion);
        Assert.True(options.ReportApiVersions);
        Assert.True(options.AssumeDefaultVersionWhenUnspecified);
        Assert.IsType<UrlSegmentApiVersionReader>(options.ApiVersionReader);
    }

    [Fact]
    public void WithDefaults_WithNoVersion_DefaultsToVersionOne()
        => Assert.Equal(new ApiVersion(1), new ApiVersioningOptions().WithDefaults().DefaultApiVersion);

    [Fact]
    public void WithDefaults_WithANullVersion_DefaultsToVersionOne()
        => Assert.Equal(new ApiVersion(1), new ApiVersioningOptions().WithDefaults(null).DefaultApiVersion);

    [Fact]
    public void WithDefaults_WithAMinorVersion_SetsMajorAndMinor()
        => Assert.Equal(new ApiVersion(2, 1), new ApiVersioningOptions().WithDefaults(2, 1).DefaultApiVersion);
}
