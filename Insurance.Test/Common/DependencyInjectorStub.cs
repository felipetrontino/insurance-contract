using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Insurance.Test.Common
{
    public static class DependencyInjectorStub
    {
        public static IServiceProvider Get(Action<IServiceCollection, IConfiguration> configure = null, Dictionary<string, string> configData = null)
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(configData)
                .Build();

            var services = new ServiceCollection();

            configure?.Invoke(services, config);

            return services.BuildServiceProvider();
        }
    }
}