using Microsoft.Extensions.Logging;

public interface IMyWorkerService
{
    void DoWork();
}

public class MyWorkerService : IMyWorkerService
{
    private readonly ILogger<MyWorkerService> _logger;

    public MyWorkerService(ILogger<MyWorkerService> logger) => _logger = logger;

    public void DoWork()
    {
        _logger.LogInformation("MyService is doing work at {Time}", DateTime.Now);
    }
}
