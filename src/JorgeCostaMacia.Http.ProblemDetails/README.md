# JorgeCostaMacia.Http.ProblemDetails

**RFC 7807 ProblemDetails, enriched** — consistent error responses for ASP.NET Core that add `requestId`, `traceId`, `nodeId` and domain aggregate metadata (`aggregateId` / `aggregateCode` / `aggregateType`), plus per-field `errors` for validation and bad-request failures, all following your JSON naming policy.

[![NuGet](https://img.shields.io/nuget/v/JorgeCostaMacia.Http.ProblemDetails.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Http.ProblemDetails/)
[![Downloads](https://img.shields.io/nuget/dt/JorgeCostaMacia.Http.ProblemDetails.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Http.ProblemDetails/)
[![Build](https://github.com/JorgeCostaMacia/http-dotnet/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/JorgeCostaMacia/http-dotnet/actions/workflows/main.yml)
[![License](https://img.shields.io/github/license/JorgeCostaMacia/http-dotnet.svg)](https://github.com/JorgeCostaMacia/http-dotnet/blob/main/LICENSE.txt)

---

## Install

```bash
dotnet add package JorgeCostaMacia.Http.ProblemDetails
```

## Usage

```csharp
using JorgeCostaMacia.Http.ProblemDetails.Infrastructure;

builder.Services.AddProblemDetails(options => options.WithDefaults());
```

Every error response gets `requestId`, `traceId` and `nodeId`. A `DomainException` contributes its aggregate metadata (and, for a `ValidationException`, a per-field `errors` dictionary); a `BadHttpRequestException` contributes `errors` describing missing/invalid JSON fields. All keys follow the app's configured `JsonNamingPolicy`.

> **Note:** to have Minimal-API model-binding failures (missing/invalid request body) reach this handler as a `BadHttpRequestException` — instead of a bare `400` with no body — enable it on the app:
> ```csharp
> builder.Services.Configure<RouteHandlerOptions>(options => options.ThrowOnBadRequest = true);
> ```

The `ProblemDetails` record (implementing `IResponseHttp`) is also provided as the canonical response shape for documentation and typed clients.

## Requirements

The **.NET 10** SDK.

Depends on [JorgeCostaMacia.Http](https://www.nuget.org/packages/JorgeCostaMacia.Http/), [JorgeCostaMacia.DDD.Exception](https://www.nuget.org/packages/JorgeCostaMacia.DDD.Exception/) and the ASP.NET Core shared framework.

## About

`JorgeCostaMacia.Http.ProblemDetails` is part of **[http-dotnet](https://github.com/JorgeCostaMacia/http-dotnet)** — extensions for ASP.NET Core, each scoped to a single concern and reusable across your services. An independent project, not affiliated with Microsoft or the ASP.NET Core project.

- **Repository:** [github.com/JorgeCostaMacia/http-dotnet](https://github.com/JorgeCostaMacia/http-dotnet)
- **Issues & requests:** [open an issue](https://github.com/JorgeCostaMacia/http-dotnet/issues)
- **Contributing:** [CONTRIBUTING.md](https://github.com/JorgeCostaMacia/http-dotnet/blob/main/CONTRIBUTING.md)
- **Security:** [report a vulnerability](https://github.com/JorgeCostaMacia/http-dotnet/security/advisories/new)

**Author:** Jorge Costa Maciá

- [LinkedIn](https://www.linkedin.com/in/jorge-costa-macia-842817164/)
- [GitHub](https://github.com/JorgeCostaMacia/)
- [Bitbucket](https://bitbucket.org/jorgecostamacia/)
