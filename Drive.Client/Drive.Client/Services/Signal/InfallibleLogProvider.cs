using Microsoft.Extensions.Logging;

namespace Drive.Client.Services.Signal {
    public class InfallibleLogProvider : ILoggerProvider {

        public ILogger CreateLogger(string categoryName) => new InfallibleLogger();

        public void Dispose() { }
    }
}
