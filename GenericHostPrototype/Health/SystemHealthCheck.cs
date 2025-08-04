using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;

/// <summary>
/// Health check that reports on system memory usage and uptime, configurable via options.
/// </summary>
public class SystemHealthCheck : IHealthCheck
{
    private readonly SystemHealthOptions _options;
    private readonly ILogger<SystemHealthCheck> _logger;

    public SystemHealthCheck(IOptions<SystemHealthOptions> options, ILogger<SystemHealthCheck> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            // Check system metrics
            var memoryUsage = GC.GetTotalMemory(false);
            var processorCount = Environment.ProcessorCount;
            var uptimeMinutes = (DateTime.Now - Process.GetCurrentProcess().StartTime).TotalMinutes;

            var isHealthy = memoryUsage < _options.MaxMemoryThreshold &&
                          uptimeMinutes < _options.MaxUptimeMinutes;

            var data = new Dictionary<string, object>
            {
                { "MemoryUsage", memoryUsage },
                { "ProcessorCount", processorCount },
                { "UptimeMinutes", uptimeMinutes }
            };

            if (isHealthy)
            {
                _logger.LogInformation("System is healthy. Memory: {Memory} bytes", memoryUsage);
                return Task.FromResult(HealthCheckResult.Healthy("System is healthy", data));
            }

            _logger.LogWarning("System health check failed. Memory: {Memory} bytes", memoryUsage);
            return Task.FromResult(HealthCheckResult.Unhealthy("System exceeds health thresholds", null, data));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error performing health check");
            return Task.FromResult(HealthCheckResult.Unhealthy("Health check failed", ex));
        }
    }
}