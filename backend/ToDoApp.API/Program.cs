using Microsoft.AspNetCore.Authentication.JwtBearer;
using ToDoApp.API.Config;
using ToDoApp.Infra.Data.Services;
using ToDoApp.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenForToDoApp();

builder.Services.AddAuthorization();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();
builder.Services.ConfigureOptions<JwtBearerConfigureExtensions>();
builder.Services.AddHttpContextAccessor();

builder.Services.SetupToDoApp(builder.Configuration);
builder.Services.SetupCors(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("Development");
}
else
{
    app.UseCors("Production");
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

MigrationService.ApplyAll(app.Services.CreateScope());

app.Run();
