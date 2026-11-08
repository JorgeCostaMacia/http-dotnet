# JorgeCostaMacia.Http.ForwardedHeaders

**Forwarded-headers defaults for a service behind a reverse proxy** — an options extension that trusts the proxy on the same host or on Docker's default bridge and honours its `X-Forwarded-*` headers, keeping the framework's `Configure<ForwardedHeadersOptions>` and `UseForwardedHeaders` calls visible in your `Program`.

[![NuGet](https://img.shields.io/nuget/v/JorgeCostaMacia.Http.ForwardedHeaders.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Http.ForwardedHeaders/)
[![Downloads](https://img.shields.io/nuget/dt/JorgeCostaMacia.Http.ForwardedHeaders.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Http.ForwardedHeaders/)
[![Build](https://github.com/JorgeCostaMacia/http-dotnet/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/JorgeCostaMacia/http-dotnet/actions/workflows/main.yml)
[![License](https://img.shields.io/github/license/JorgeCostaMacia/http-dotnet.svg)](https://github.com/JorgeCostaMacia/http-dotnet/blob/main/LICENSE.txt)

---

## Install

```bash
dotnet add package JorgeCostaMacia.Http.ForwardedHeaders
```

## Usage

```csharp
using JorgeCostaMacia.Http.ForwardedHeaders.Infrastructure;

builder.Services.Configure<ForwardedHeadersOptions>(options => options.WithDefaults());

var app = builder.Build();

app.UseForwardedHeaders();   // first in the pipeline, so everything after sees the client's address and scheme
```

Honours `X-Forwarded-For`, `X-Forwarded-Proto` and `X-Forwarded-Host`, and only from a trusted address: the loopbacks (`127.0.0.1`, `127.0.1.1`, `::1`) and Docker's default bridge (`172.17.0.0/16`), the network a proxy on the host reaches a published container from. Headers sent by anyone else are ignored, so a client cannot forge its address or scheme.

The trusted addresses are added to the framework's own. A service behind another proxy adds it after the defaults:

```csharp
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.WithDefaults();
    options.KnownProxies.Add(IPAddress.Parse("10.0.0.5"));
});
```

## Requirements

The **.NET 10** SDK.

Depends on the ASP.NET Core shared framework only.

## About

`JorgeCostaMacia.Http.ForwardedHeaders` is part of **[http-dotnet](https://github.com/JorgeCostaMacia/http-dotnet)** — extensions for ASP.NET Core, each scoped to a single concern and reusable across your services. An independent project, not affiliated with Microsoft or the ASP.NET Core project.

- **Repository:** [github.com/JorgeCostaMacia/http-dotnet](https://github.com/JorgeCostaMacia/http-dotnet)
- **Issues & requests:** [open an issue](https://github.com/JorgeCostaMacia/http-dotnet/issues)
- **Contributing:** [CONTRIBUTING.md](https://github.com/JorgeCostaMacia/http-dotnet/blob/main/CONTRIBUTING.md)
- **Security:** [report a vulnerability](https://github.com/JorgeCostaMacia/http-dotnet/security/advisories/new)

**Author:** Jorge Costa Maciá

- [LinkedIn](https://www.linkedin.com/in/jorge-costa-macia-842817164/)
- [GitHub](https://github.com/JorgeCostaMacia/)
- [Bitbucket](https://bitbucket.org/jorgecostamacia/)
