using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace WebStore.Logger
{
    public static class Log4NetExtensions
    {
        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory, string configurationFile = "log4net.config")
        {
            if (!Path.IsPathRooted(configurationFile))
            {
                var assembly = Assembly.GetEntryAssembly()
                               ?? throw new InvalidOperationException("Process executable not found in default application domain");

                var dir = Path.GetDirectoryName(assembly.Location)
                          ?? throw new InvalidOperationException("The start assembly location directory is not defined");

                configurationFile = Path.Combine(dir, configurationFile);
            }

            factory.AddProvider(new Log4NetLoggerProvider(configurationFile));

            return factory;
        }
    }
}