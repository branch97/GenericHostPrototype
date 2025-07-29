# GenericHostPrototype

A demo .NET 10 console application that showcases the modern .NET Generic Host capabilities, emphasizing best practices for building robust, maintainable, and extensible apps.

## Key Features

* **Dependency Injection** — clean service registration with singleton and hosted services.
* **Logging** — configurable logging providers with console, debug output, and filtering.
* **Configuration Providers** — dynamic configuration loading from JSON files and environment variables.
* **Application Lifecycle Management** — use of `IHostedService` and lifecycle hooks to handle background tasks gracefully.
* **Metrics Integration** — extensible metrics support for application monitoring.

## Overview

This sample app uses `Host.CreateApplicationBuilder` for modern host setup, making it easy to:

* Configure environment variables and application name.
* Load settings from multiple configuration sources (`appsettings.json`, environment variables).
* Register and run custom singleton services and hosted background services.
* Implement detailed lifecycle event logging via `IHostedLifecycleService`.

## Project Structure

* **`Program.cs`**
  Sets up the generic host: configures environment, logging, configuration sources, and service registrations.

* **`MyWorkerService.cs`**
  A singleton service example that performs background work and logs activity.

* **`HostedLifecycleService.cs`**
  Demonstrates the implementation of `IHostedService` and lifecycle event handling for clean startup/shutdown sequences.

* **`appsettings.json`**
  Sample configuration file with reload-on-change enabled for dynamic configuration updates.

* **`GenericHostPrototype.Tests`**
  xUnit test project leveraging Moq to validate service behaviors and lifecycle event triggers.

## Getting Started

### Prerequisites

* [.NET 10 SDK (Preview)](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) installed on your machine.

### Build and Run

1. Build the solution:

   ```bash
   dotnet build
   ```

2. Run the application:

   ```bash
   dotnet run --project GenericHostPrototype/GenericHostPrototype.csproj
   ```

3. Run unit tests:

   ```bash
   dotnet test
   ```

## Sample Output

When running, the application logs detailed lifecycle events and service activity, such as:

```
info: HostedLifecycleService[0]
      1. StartingAsync has been called.
info: HostedLifecycleService[0]
      2. StartAsync has been called.
info: HostedLifecycleService[0]
      3. StartedAsync has been called.
info: MyWorkerService[0]
      MyService is doing work at 2024-07-28T12:34:56.789Z
...
```

## Extending the Application

* Customize `appsettings.json` or override settings using environment variables for different deployment environments.
* Add additional services or hosted services by registering them in `Program.cs`.
* Integrate more complex metrics, health checks, or telemetry providers as needed.


