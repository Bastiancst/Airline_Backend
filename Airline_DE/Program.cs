using Airline_DE.Settings;
using Airline_DE.Extensions;
using Microsoft.EntityFrameworkCore;
using Airline_DE.DbContext;
using Airline_DE.Seeds;
using Microsoft.AspNetCore.Identity;
using Airline_DE.Interfaces.IRepository;

var builder = WebApplication.CreateBuilder(args);

#region Variables
var SpecificOrigins = "_myAllowSpecificOrigins";

string connectionString = "";
string? identityToken = "";
string sqlUsername = "";
string sqlCatalog = "";
string? sqlPassword = "";
string sqlDatabase = "";

string? port = "";
string? server = "";
string? password = "";
string? user = "";

string confirmEmailDomain = "";
string redirectEmailDomain = "";
string redirectRecoveryDomain = "";

#endregion

#region Envionments
if (builder.Environment.IsProduction())
{
    //sqlDatabase = "";
    //sqlUsername = "";
    //sqlCatalog = "";
    //sqlPassword = "";
    //identityToken = "";

}
else if (builder.Environment.IsStaging())
{
    //sqlDatabase = "";
    //sqlUsername = "";
    //sqlCatalog = "";
    //sqlPassword = "";
    //identityToken = "";
}
else
{
    //sqlDatabase = "";
    //sqlUsername = "";
    //sqlCatalog = "";
    //sqlPassword = "";
    //identityToken = "";
}
#endregion

#region Configurations
JWTSettings.Key = builder.Configuration["JWTSettings:Key"];
JWTSettings.Issuer = builder.Configuration["JWTSettings:Issuer"];
JWTSettings.Audience = builder.Configuration["JWTSettings:Audience"];
JWTSettings.DurationInMinutes = Int32.Parse(builder.Configuration["JWTSettings:DurationInMinutes"]);

EmailSettings.ApiKey = "";

//connectionString = $"Server = tcp:{sqlDatabase},1433; Initial Catalog = {sqlCatalog}; Persist Security Info = False; User ID = {sqlUsername}; Password = {sqlPassword}; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";
connectionString = "Server=.;Database=AirlineDE;TrustServerCertificate=True;Trusted_Connection=True;MultipleActiveResultSets=True";
ConnectionSettings.ConnectionString = connectionString;

DomainSettings.ConfirmEmailRedirectDomain = confirmEmailDomain;
DomainSettings.AfterConfirmEmailDomain = redirectEmailDomain;
DomainSettings.RedirectRecoveryDomain = redirectRecoveryDomain;

#endregion

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServiceExtension(builder.Configuration);
builder.Services.AddAccountServiceExtension(builder.Configuration);
builder.Services.AddDbContext<Context>(option =>
{
    option.UseSqlServer(ConnectionSettings.ConnectionString);
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: SpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(SpecificOrigins);

SeedDatabase();

app.MapControllers();

app.Run();


#region methods
async void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await SeedRoles.SeedAsync(roleManager);
            var flightPlanning = services.GetRequiredService<IFlightPlanningRepository>();
            var airport = services.GetRequiredService<IAirportRepository>();
            await SeedAirlane.SeedAirlaneAsync(airport, flightPlanning, false);

        }
        catch (Exception)
        {
            throw;
        }
    }
}

#endregion