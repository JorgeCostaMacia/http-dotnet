# JorgeCostaMacia.Http

**HTTP request/response base contracts** — traceable `abstract record`s and marker interfaces for messages that cross the HTTP layer, carrying a stable id, a correlation id and a UTC timestamp for consistent tracing across distributed systems.

[![NuGet](https://img.shields.io/nuget/v/JorgeCostaMacia.Http.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Http/)
[![Downloads](https://img.shields.io/nuget/dt/JorgeCostaMacia.Http.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Http/)
[![Build](https://github.com/JorgeCostaMacia/http-dotnet/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/JorgeCostaMacia/http-dotnet/actions/workflows/main.yml)
[![License](https://img.shields.io/github/license/JorgeCostaMacia/http-dotnet.svg)](https://github.com/JorgeCostaMacia/http-dotnet/blob/main/LICENSE.txt)

---

## Install

```bash
dotnet add package JorgeCostaMacia.Http
```

## Types

| Type | For |
| --- | --- |
| `IRequestHttp` | marker for incoming HTTP request contracts |
| `IResponseHttp` | marker for outgoing HTTP response contracts |
| `RequestHttp` | `abstract record` request base with `AggregateId` / `AggregateCorrelationId` / `AggregateOccurredAt` |
| `ResponseHttp` | `abstract record` response base with the same traceability metadata |

## Usage

```csharp
using JorgeCostaMacia.Http.Domain;

public sealed record CreateCustomerRequest(string Name)
    : RequestHttp(aggregateId: null, aggregateCorrelationId: null, aggregateOccurredAt: null);

// metadata is auto-filled: AggregateId (a time-ordered UUIDv7),
// AggregateCorrelationId (defaults to AggregateId), AggregateOccurredAt (UTC).
```

## Requirements

The **.NET 10** SDK.


## About

`JorgeCostaMacia.Http` is part of **[http-dotnet](https://github.com/JorgeCostaMacia/http-dotnet)** — extensions for ASP.NET Core, each scoped to a single concern and reusable across your services. An independent project, not affiliated with Microsoft or the ASP.NET Core project.

- **Repository:** [github.com/JorgeCostaMacia/http-dotnet](https://github.com/JorgeCostaMacia/http-dotnet)
- **Issues & requests:** [open an issue](https://github.com/JorgeCostaMacia/http-dotnet/issues)
- **Contributing:** [CONTRIBUTING.md](https://github.com/JorgeCostaMacia/http-dotnet/blob/main/CONTRIBUTING.md)
- **Security:** [report a vulnerability](https://github.com/JorgeCostaMacia/http-dotnet/security/advisories/new)

**Author:** Jorge Costa Maciá

- [LinkedIn](https://www.linkedin.com/in/jorge-costa-macia-842817164/)
- [GitHub](https://github.com/JorgeCostaMacia/)
- [Bitbucket](https://bitbucket.org/jorgecostamacia/)
