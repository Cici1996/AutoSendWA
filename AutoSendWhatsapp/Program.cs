using AutoSendWhatsapp.Constants;
using AutoSendWhatsapp.Services;
using AutoSendWhatsapp.Services.BackgroundJobs;
using AutoSendWhatsapp.Utils;
using Hangfire;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHangfire(c => c.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(builder.Configuration.GetConnectionString(GlobalConstants.Default)));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfireServer();

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "Logs/log-.txt"),rollingInterval: RollingInterval.Day)
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddHttpClient(GlobalConstants.WAHAClient, c => 
    c.BaseAddress = new Uri(builder.Configuration.GetSection(GlobalConstants.WAHABaseURL).Value ?? string.Empty));

builder.Services.AddScoped<IMessageService,MessageService>();
builder.Services.AddScoped<IBackgroundJobService, BackgroundJobService>();
builder.Services.AddScoped<IExcelHelper, ExcelHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
