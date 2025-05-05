using ColorBlindnessSimulator;
using GraphicsHelper;
using LevelGenerator;
using Microsoft.Extensions.DependencyInjection;
using PaletteGenerator;
using PaletteGenerator.ColorConverter;

namespace ConsolePlayground;

public static class DIContainer
{
    public static ServiceProvider RegisterServices()
    {
        ServiceCollection services = new ();
        services.AddSingleton<ILevelGenerator, LevelGenerator.LevelGenerator>();
        services.AddSingleton<IImageConverter, ImageConverter>();
        services.AddSingleton<IColorConverter, ColorMineColorConverter>();
        services.AddSingleton<IColorBlindnessSimulator, ColorBlindnessSimulator.ColorBlindnessSimulator>();
        services.AddSingleton<IPaletteGenerator, PaletteGenerator.PaletteGenerator>();
        return services.BuildServiceProvider();
    }
}