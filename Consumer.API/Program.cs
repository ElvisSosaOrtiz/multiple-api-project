using Consumer.API.LocalServices;
using ServiceContracts;
using Services;
using Services.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddSingleton<IConfigurationSettingsProvider, ConfigurationSettingsProvider>()
    .AddScoped<IOffersService, OffersService>()
    .AddScoped<IJsonConvertWrapper, JsonConvertWrapper>();

IConfigurationSettingsProvider configuration = new ConfigurationSettingsProvider(builder.Configuration);

builder.Services.AddHttpClient(HttpClientNames.CompanyOne, client =>
{
    client.BaseAddress = new Uri(configuration.CompanyOneBaseUrl);
});
builder.Services.AddHttpClient(HttpClientNames.CompanyTwo, client =>
{
    client.BaseAddress = new Uri(configuration.CompanyTwoBaseUrl);
});
builder.Services.AddHttpClient(HttpClientNames.CompanyThree, client =>
{
    client.BaseAddress = new Uri(configuration.CompanyThreeBaseUrl);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
