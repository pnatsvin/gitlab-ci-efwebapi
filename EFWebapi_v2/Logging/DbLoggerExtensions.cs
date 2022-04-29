using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;

namespace EFWebapi_v2.Logging
{
    public static class DbLoggerExtensions
    {
        public static ILoggingBuilder AddDbLogger(this ILoggingBuilder builder, Action<DbLoggerConfiguration> configure)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(
                   ServiceDescriptor.Singleton<ILoggerProvider, DbLoggerProvaider>());

            LoggerProviderOptions.RegisterProviderOptions<DbLoggerConfiguration, DbLoggerProvaider>(builder.Services);

            builder.Services.Configure(configure);

            return builder;
        }
    }
}
