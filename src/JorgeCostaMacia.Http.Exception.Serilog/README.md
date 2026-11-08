# JorgeCostaMacia.Http.Exception.Serilog

**Serilog exception logging** for ASP.NET Core — an `IExceptionHandler` that logs every unhandled exception (enriched with domain aggregate metadata and the authenticated user), while leaving the response itself to your status-code mapping.

[![NuGet](https://img.shields.io/nuget/v/JorgeCostaMacia.Http.Exception.Serilog.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Http.Exception.Serilog/)
[![Downloads](https://img.shields.io/nuget/dt/JorgeCostaMacia.Http.Exception.Serilog.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Http.Exception.Serilog/)
[![Build](https://github.com/JorgeCostaMacia/http-dotnet/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/JorgeCostaMacia/http-dotnet/actions/workflows/main.yml)
[![License](https://img.shields.io/github/license/JorgeCostaMacia/http-dotnet.svg)](https://github.com/JorgeCostaMacia/http-dotnet/blob/main/LICENSE.txt)

---

## Install

```bash
dotnet add package JorgeCostaMacia.Http.Exception.Serilog
```

## Usage

```csharp
using JorgeCostaMacia.Http.Exception.Serilog.Infrastructure;

builder.Services.AddExceptionHandler<ExceptionHandler>();
```

The handler logs known exceptions (`DomainException`, `BadHttpRequestException`, `FluentValidation.ValidationException`) at **Warning** and everything else at **Error**, pushing `ExceptionAggregateId` / `ExceptionAggregateCode` / `ExceptionAggregateType` / `UserName` to the Serilog `LogContext`. It always returns `false`, so the response is still produced by your status-code mapping (e.g. [JorgeCostaMacia.Http.Exception](https://www.nuget.org/packages/JorgeCostaMacia.Http.Exception/)).

## Requirements

The **.NET 10** SDK.

Depends on [JorgeCostaMacia.DDD.Exception](https://www.nuget.org/packages/JorgeCostaMacia.DDD.Exception/), [Serilog](https://www.nuget.org/packages/Serilog/) and the ASP.NET Core shared framework.

## About

`JorgeCostaMacia.Http.Exception.Serilog` is part of **[http-dotnet](https://github.com/JorgeCostaMacia/http-dotnet)** — extensions for ASP.NET Core, each scoped to a single concern and reusable across your services. An independent project, not affiliated with Microsoft or the ASP.NET Core project.

- **Repository:** [github.com/JorgeCostaMacia/http-dotnet](https://github.com/JorgeCostaMacia/http-dotnet)
- **Issues & requests:** [open an issue](https://github.com/JorgeCostaMacia/http-dotnet/issues)
- **Contributing:** [CONTRIBUTING.md](https://github.com/JorgeCostaMacia/http-dotnet/blob/main/CONTRIBUTING.md)
- **Security:** [report a vulnerability](https://github.com/JorgeCostaMacia/http-dotnet/security/advisories/new)

**Author:** Jorge Costa Maciá

- [LinkedIn](https://www.linkedin.com/in/jorge-costa-macia-842817164/)
- [GitHub](https://github.com/JorgeCostaMacia/)
- [Bitbucket](https://bitbucket.org/jorgecostamacia/)
