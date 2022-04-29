using EFWebApi_v2.Data;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
namespace EFWebapi_v2.Logging
{
    [ProviderAlias("Dblogging")]
    public class DbLoggerProvaider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, DbLogger> _loggers = new();

        private readonly DbLoggerConfiguration _config;

        private readonly SqlEfContex _sqlEfContex;


        public DbLoggerProvaider(IOptionsMonitor<DbLoggerConfiguration> config)
        {
            _config = config.CurrentValue;

        }

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new DbLogger(name, _config ));
        public void Dispose() => _loggers.Clear();
    }
}
