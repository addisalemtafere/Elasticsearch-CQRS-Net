using Application;
using Infrastructure;
using Infrastructure.Configuration.ElasticsearchConfiguration;
using Infrastructure.Persistance.ElasticsearchContext;
using Infrastructure.Seed;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
Log.Information("Server Booting Up...");
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    IConfiguration configuration = builder.Configuration;

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddApplicationService();
    builder.Services.AddInfrastructureServices(configuration);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    IElasticContextProvider service = app.Services.GetRequiredService<IElasticContextProvider>();
    if (CommonConfigurationsHelper.GetSettings().IsSeedEnable)
    {
        await SeedData.SeedAsync(service);
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex) when (!ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}