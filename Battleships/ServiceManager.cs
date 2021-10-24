using System;
using Battleships.GameLogic.Factories;
using Battleships.Grid;
using Battleships.Init;
using Battleships.IO;
using Battleships.Settings;
using Battleships.Ships;
using Microsoft.Extensions.DependencyInjection;

namespace Battleships
{
    public class ServiceManager : ServiceCollectionWrapper
    {
        public void InitDefault()
        {
            services = new ServiceCollection();
            services.AddSingleton<IIOManager, IOManager>();
            services.AddTransient<Random>();

            services.AddSingleton<SettingsManager>();

            services.AddSingleton<ShipsGenerator>();
            services.AddSingleton<BoardFactory>();
            services.AddSingleton<GameInitializer>();

            services.AddSingleton<JudgeFactory>();
            services.AddSingleton<PlayerFactory>();
            services.AddSingleton<GameFactory>();
        }

        public IServiceProvider GetServiceProvider()
        {
            return services.BuildServiceProvider();
        }
    }
}
