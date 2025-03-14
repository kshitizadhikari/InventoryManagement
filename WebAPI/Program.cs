using WebAPI;

var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;

builder.Services.AddAuthorizationBuilder()
              .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAppDI(builder.Configuration);
var app = builder.Build();

// Run role seeding after app is built
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    await Infrastructure.DependencyInjection.InitializeDatabaseAsync(serviceProvider);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
