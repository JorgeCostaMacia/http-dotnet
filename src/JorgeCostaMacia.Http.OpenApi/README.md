# JorgeCostaMacia.Http.OpenApi

**RFC 7807 schema transformer for native OpenAPI** — a schema transformer for .NET's built-in OpenAPI generation that augments the ProblemDetails schemas with `errors`, `requestId`, `traceId`, `nodeId` and domain aggregate metadata, so your API docs match the error shape your services actually return.

[![NuGet](https://img.shields.io/nuget/v/JorgeCostaMacia.Http.OpenApi.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Http.OpenApi/)
[![Downloads](https://img.shields.io/nuget/dt/JorgeCostaMacia.Http.OpenApi.svg)](https://www.nuget.org/packages/JorgeCostaMacia.Http.OpenApi/)
[![Build](https://github.com/JorgeCostaMacia/http-dotnet/actions/workflows/main.yml/badge.svg?branch=main)](https://github.com/JorgeCostaMacia/http-dotnet/actions/workflows/main.yml)
[![License](https://img.shields.io/github/license/JorgeCostaMacia/http-dotnet.svg)](https://github.com/JorgeCostaMacia/http-dotnet/blob/main/LICENSE.txt)

---

## Install

```bash
dotnet add package JorgeCostaMacia.Http.OpenApi
```

## Usage

```csharp
using JorgeCostaMacia.Http.OpenApi.Infrastructure;
using Microsoft.AspNetCore.OpenApi;

builder.Services.AddOpenApi(options => options.AddSchemaTransformer<ProblemDetailsSchemaTransformer>());
```

Registers `ProblemDetailsSchemaTransformer`, which adds `errors`, `requestId`, `traceId`, `nodeId`, `aggregateId`, `aggregateCode` and `aggregateType` to the `ProblemDetails` / `ValidationProblemDetails` / `HttpValidationProblemDetails` OpenAPI schemas, using the native Microsoft OpenAPI engine. The `AddOpenApi` call stays visible in your `Program`; the package only contributes the transformer.

## Requirements

**.NET 10** — uses the native `Microsoft.AspNetCore.OpenApi` (10.x) generation pipeline.

Depends on [Microsoft.AspNetCore.OpenApi](https://www.nuget.org/packages/Microsoft.AspNetCore.OpenApi/) (which brings the ASP.NET Core shared framework transitively).

## About

`JorgeCostaMacia.Http.OpenApi` is part of **[http-dotnet](https://github.com/JorgeCostaMacia/http-dotnet)** — extensions for ASP.NET Core, each scoped to a single concern and reusable across your services. An independent project, not affiliated with Microsoft or the ASP.NET Core project.

- **Repository:** [github.com/JorgeCostaMacia/http-dotnet](https://github.com/JorgeCostaMacia/http-dotnet)
- **Issues & requests:** [open an issue](https://github.com/JorgeCostaMacia/http-dotnet/issues)
- **Contributing:** [CONTRIBUTING.md](https://github.com/JorgeCostaMacia/http-dotnet/blob/main/CONTRIBUTING.md)
- **Security:** [report a vulnerability](https://github.com/JorgeCostaMacia/http-dotnet/security/advisories/new)

**Author:** Jorge Costa Maciá

- [LinkedIn](https://www.linkedin.com/in/jorge-costa-macia-842817164/)
- [GitHub](https://github.com/JorgeCostaMacia/)
- [Bitbucket](https://bitbucket.org/jorgecostamacia/)
