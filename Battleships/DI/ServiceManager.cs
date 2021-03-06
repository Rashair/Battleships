using System;
using System.Linq;
using Battleships.GameLogic.Factories;
using Battleships.Grid;
using Battleships.Init;
using Battleships.IO;
using Battleships.Settings;
using Battleships.Ships;
using Microsoft.Extensions.DependencyInjection;

namespace Battleships.DI
{
    public class ServiceManager : ServiceCollectionWrapper
    {
        public override void InitDefault()
        {
            base.InitDefault();
            services.AddSingleton<IIOManager, IOManager>();

            services.AddSingleton<GameSettings>();
            services.AddSingleton<SettingsManager>();

            services.AddSingleton<ShipsGenerator>();
            services.AddSingleton<BoardFactory>();
            services.AddSingleton<GameInitializer>();

            services.AddSingleton<JudgeFactory>();
            services.AddSingleton<PlayerFactory>();
            services.AddSingleton<GameFactory>();

            services.AddTransient<Random>();
            RegisterallShips();
        }

        private void RegisterallShips()
        {
            var shipBaseType = typeof(Ship);
            var assembly = shipBaseType.Assembly;
            var shipTypes = assembly.GetTypes()
                .Where(t => t.IsSubclassOf(shipBaseType));

            foreach (var type in shipTypes)
            {
                services.AddSingleton(shipBaseType, type);
                services.AddTransient(type, type);
            }
        }

        public IServiceProvider GetServiceProvider()
        {
            return services.BuildServiceProvider();
        }
    }
}
