using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// Create a generic host builder with default configuration, dependency injection, and logging support.
// This initializes the application's hosting environment and services.
var builder = Host.CreateApplicationBuilder(args);

// -------------------------
// Configure Environment
// -------------------------
// Set the application name and environment explicitly.
// This can influence configuration loading, logging, and other environment-specific behaviors.
builder.Environment.ApplicationName = "GenericHostPrototype";
builder.Environment.EnvironmentName = "Development";

// -------------------------
// Add Configuration Sources
// -------------------------
// Load configuration settings from JSON file and environment variables.
// - appsettings.json is optional and reloads on change (useful for dynamic configuration).
// - Environment variables allow overriding or providing configuration externally.
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// -------------------------
// Configure Logging
// -------------------------
// Clear default logging providers and add specific ones:
// - Console logger for standard output.
// - Debug logger for debug output during development.
// Also apply a filter to only log warnings and above from "Microsoft.Hosting.Lifetime" category to reduce noise.
builder.Logging
    .ClearProviders()
    .AddConsole()
    .AddDebug()
    .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Warning);

// -------------------------
// Register Dependencies and Services
// -------------------------
// Register application services with dependency injection container:
// - IMyWorkerService implemented by MyWorkerService as a singleton (single instance for app lifetime).
// - HostedLifecycleService as a hosted background service that runs with the host lifecycle.
builder.Services
    .AddSingleton<IMyWorkerService, MyWorkerService>()
    .AddHostedService<HostedLifecycleService>();

// -------------------------
// Add Metrics Services
// -------------------------
// Register metrics-related services using the builder's Metrics extension.
// This enables metrics collection and reporting capabilities for monitoring the application.
builder.Metrics.Services.AddMetrics();

// -------------------------
// Build and Run Host
// -------------------------
// Build the configured host instance and start it asynchronously.
// This runs the application and keeps it alive until shutdown is requested.
var host = builder.Build();
await host.RunAsync();
