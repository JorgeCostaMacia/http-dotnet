using System.Net;
using JorgeCostaMacia.Http.ForwardedHeaders.Infrastructure;
using Microsoft.AspNetCore.Builder;
using ForwardedHeaderFlags = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders;
using IPNetwork = System.Net.IPNetwork;

namespace JorgeCostaMacia.Http.ForwardedHeaders.Tests.Infrastructure;

public sealed class ForwardedHeadersOptionsExtensionsTests
{
    [Fact]
    public void WithDefaults_OnNewOptions_ReturnsTheSameInstanceForChaining()
    {
        ForwardedHeadersOptions options = new ForwardedHeadersOptions();

        Assert.Same(options, options.WithDefaults());
    }

    [Fact]
    public void WithDefaults_OnNewOptions_HonoursForHostAndProto()
        => Assert.Equal(
            ForwardedHeaderFlags.XForwardedFor
                | ForwardedHeaderFlags.XForwardedHost
                | ForwardedHeaderFlags.XForwardedProto,
            new ForwardedHeadersOptions().WithDefaults().ForwardedHeaders);

    [Fact]
    public void WithDefaults_OnNewOptions_TrustsTheLoopbacks()
    {
        ForwardedHeadersOptions options = new ForwardedHeadersOptions().WithDefaults();

        Assert.Contains(IPAddress.Parse("127.0.0.1"), options.KnownProxies);
        Assert.Contains(IPAddress.Parse("127.0.1.1"), options.KnownProxies);
        Assert.Contains(IPAddress.IPv6Loopback, options.KnownProxies);
    }

    [Fact]
    public void WithDefaults_OnNewOptions_TrustsDockersDefaultBridge()
        => Assert.Contains(
            new IPNetwork(IPAddress.Parse("172.17.0.0"), 16),
            new ForwardedHeadersOptions().WithDefaults().KnownIPNetworks);

    // added to the framework's trusted addresses, never in their place
    [Fact]
    public void WithDefaults_OnOptionsWithTheFrameworksTrustedAddresses_KeepsThem()
    {
        ForwardedHeadersOptions framework = new ForwardedHeadersOptions();
        ForwardedHeadersOptions options = new ForwardedHeadersOptions().WithDefaults();

        Assert.All(framework.KnownProxies, proxy => Assert.Contains(proxy, options.KnownProxies));
        Assert.All(framework.KnownIPNetworks, network => Assert.Contains(network, options.KnownIPNetworks));
    }
}
