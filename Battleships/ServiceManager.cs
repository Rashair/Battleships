using System;
using Battleships.GameLogic;
using Battleships.Grid;
using Battleships.Init;
using Battleships.IO;
using Battleships.Settings;
using Battleships.Ships;
using Microsoft.Extensions.DependencyInjection;

namespace Battleships
{
    public class ServiceManager
    {
        private readonly ServiceProvider serviceProvider;

        public ServiceManager()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IIOManager, IOManager>();
            services.AddTransient<Random>();

            services.AddSingleton<SettingsManager>();

            services.AddSingleton<ShipsGenerator>();
            services.AddSingleton<BoardFactory>();
            services.AddSingleton<GameInitializer>();

            services.AddSingleton<JudgeFactory>();
            services.AddSingleton<PlayerFactory>();
            services.AddSingleton<GameFactory>();

            serviceProvider = services.BuildServiceProvider();
        }

        public IServiceProvider GetServiceProvider()
        {
            return serviceProvider;
        }
    }
}
