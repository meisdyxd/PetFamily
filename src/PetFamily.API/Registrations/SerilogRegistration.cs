using Serilog;

namespace PetFamily.API.Registrations;

public static class SerilogRegistration
{
    public static void Execute(IConfiguration configurations)
    {
        var serilogConfiguration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(serilogConfiguration)
            .WriteTo.Seq(configurations.GetConnectionString("Seq")
                ?? throw new Exception("Seq connection string is empty"))
            .CreateLogger();
    }
}
