using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace SamagnaSagamBVProjTests.Mocks
{
    public sealed class FakeLogger<T>: ILogger<T>, IDisposable
    {
        public FakeLogger()
        {
        }
        private readonly List<LoggedMessage> _messages = new List<LoggedMessage>();
        public IReadOnlyList<LoggedMessage> Messages => _messages;

        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public void Log<TState>(LogLevel logLevel,EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);
            _messages.Add(new LoggedMessage(logLevel, exception, message));
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

       

        public void Dispose()
        {
            
        }

        public sealed class LoggedMessage
        {
            public LogLevel logLevel { get; }
            public Exception exception { get; }
            public string Message { get; }

            public LoggedMessage(LogLevel log, Exception ex, string message)
            {
                logLevel = log;
                exception = ex;
                Message = message;
            }
        }
    }
}
