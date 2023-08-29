using Airline_DE.Settings;
using Airline_DE.Extensions;

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
JWTSettings.Key = identityToken;
JWTSettings.Issuer = builder.Configuration["JWTSettings:Issuer"];
JWTSettings.Audience = builder.Configuration["JWTSettings:Audience"];
JWTSettings.DurationInMinutes = Int32.Parse(builder.Configuration["JWTSettings:DurationInMinutes"]);

EmailSettings.Port = Int32.Parse(builder.Configuration["Email:Port"]);
EmailSettings.Password = builder.Configuration["Email:Password"];
EmailSettings.Server = builder.Configuration["Email:Server"];
EmailSettings.User = builder.Configuration["Email:User"];

connectionString = $"Server = tcp:{sqlDatabase},1433; Initial Catalog = {sqlCatalog}; Persist Security Info = False; User ID = {sqlUsername}; Password = {sqlPassword}; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";
ConnectionStringSettings.ConnectionString = connectionString;

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
