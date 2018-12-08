using Microsoft.Extensions.Logging;
using System;

namespace Drive.Client.Services.Signal {
    public class InfallibleLogger : ILogger, IDisposable {
        public IDisposable BeginScope<TState>(TState state) => this;

        public void Dispose() { }

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) {
            Console.WriteLine("===> {0}", formatter.Invoke(state, exception));
        }
    }
}
