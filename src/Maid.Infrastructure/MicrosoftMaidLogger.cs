using Maid.Core.Boundaries;
using Microsoft.Extensions.Logging;

namespace Maid.Infrastructure;

public sealed class MicrosoftMaidLogger<T> : IMaidLogger<T>
{
    private readonly ILogger<T> _logger;

    public MicrosoftMaidLogger(ILogger<T> logger)
    {
        _logger = logger;
    }

    public void LogError(string message) => _logger.LogError(message);

    public void LogInformation(string message) => _logger.LogInformation(message);

    public void LogWarning(string message) => _logger.LogWarning(message);
}
