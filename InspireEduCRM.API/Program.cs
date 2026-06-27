using InspireEduCRM.Application.Services;
using InspireEduCRM.Application.Services.Books;
using InspireEduCRM.Application.Services.Contacts;
using InspireEduCRM.Application.Services.FollowUps;
using InspireEduCRM.Application.Services.Leads;
using InspireEduCRM.Application.Services.Visits;
using InspireEduCRM.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(); // <-- replaces AddOpenApi()
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Enter your JWT token like this: Bearer {your token}",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddHttpContextAccessor();

// Register our Auth service for dependency injection
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<InspireEduCRM.Application.Services.ISchoolService, InspireEduCRM.Application.Services.SchoolService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<InspireEduCRM.Application.Services.Common.ICurrentUserService, InspireEduCRM.Application.Services.Common.CurrentUserService>();
builder.Services.AddScoped<IVisitService, VisitService>();
builder.Services.AddScoped<ILeadService, LeadService>();
builder.Services.AddScoped<IFollowUpService, FollowUpService>();


// JWT Authentication setup
var jwtKey = builder.Configuration["Jwt:Key"]!;
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors("AllowAngularApp");

if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(); // gives you the visual page at /swagger
}

app.UseHttpsRedirection();

app.UseAuthentication(); // must come BEFORE UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();