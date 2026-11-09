# Changelog

What a consumer of these packages has to act on, newest first. Versions that only touched the docs, the build or the internals are not listed.

## 5.1.0

The log messages are PascalCase event names: `Request End` is `RequestEnd`, `Request Fail` is `RequestFail` and `Request Crash` is `RequestCrash`. A query, a dashboard or an alert on the old text needs the new one.

## 5.0.0

The `JorgeCostaMacia.Http.*` packages continue here from http-net with the same ids, in a major because the exceptions the handlers map now come from `JorgeCostaMacia.DDD.Exception` 11.0.0 (ddd-dotnet): bump them together with it and rename `JorgeCostaMacia.Exception` to `JorgeCostaMacia.DDD.Exception` in your `using`s. Nothing else changes.
