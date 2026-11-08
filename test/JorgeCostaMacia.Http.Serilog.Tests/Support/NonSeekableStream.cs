namespace JorgeCostaMacia.Http.Serilog.Tests.Support;

/// <summary>
/// A request body that cannot seek, as one read off the network arrives: <c>DefaultHttpContext</c>'s own body is
/// <c>Stream.Null</c>, which already seeks, so a test asserting on <c>CanSeek</c> needs this one to prove anything.
/// </summary>
internal sealed class NonSeekableStream : MemoryStream
{
    public override bool CanSeek => false;
}
