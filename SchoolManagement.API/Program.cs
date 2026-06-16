using SchoolManagement.API.Extensions;
using SchoolManagement.Application.DependencyInjection;
using SchoolManagement.Persistence.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


// Swagger
builder.Services.AddSwaggerDocumentation();

// Application Layer
builder.Services.AddApplicationServices();

// Persistence Layer
builder.Services.AddPersistenceServices(builder.Configuration);

// JWT
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseApplicationMiddleware();

app.Run();