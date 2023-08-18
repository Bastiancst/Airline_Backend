using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

#region Variables
var SpecificOrigins = "_myAllowSpecificOrigins";

string connectionString = "";
string? identityToken = "";
string sqlUsername = "";
string sqlCatalog = "";
string? sqlPassword = "";
string sqlDatabase = "";
string? emailToken = "";

#endregion

#region Envionments
if (builder.Environment.IsProduction())
{

}
else if (builder.Environment.IsStaging())
{

}
else
{

}
#endregion

// Add services to the container.

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
