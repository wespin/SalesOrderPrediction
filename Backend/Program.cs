using NLog;
using NLog.Web;
using WebApi;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    var startup = new Startup(builder.Configuration);

    startup.ConfigureServices(builder.Services);

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    startup.Configure(app, app.Environment);

    app.Run();
}
catch(Exception e) 
{
    logger.Error(e, "Program has stopped because there was an exception");
}
finally
{
    NLog.LogManager.Shutdown();
}