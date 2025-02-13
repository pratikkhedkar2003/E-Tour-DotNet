using System.Text;
using etour_api.Exceptions.Handler;
using etour_api.Repositories;
using etour_api.Services;
using etour_api.Services.Impl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => {
    options.AddPolicy("EtourAllowPoliciy", builder => {
        builder.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

// Repositories
builder.Services.AddDbContextPool<AppDbContext>((options) => 
    options.UseMySql(
        builder.Configuration.GetConnectionString("DbConnection"), 
        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.19-mysql")
    )
);

builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<IJwtService, JwtServiceImpl>();
builder.Services.AddScoped<ITourCategoryService, TourCategoryServiceImpl>();

builder.Services.AddExceptionHandler<ApiExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(exceptionHandlerApp 
    => exceptionHandlerApp.Run(async context 
        => await context.RequestServices
            .GetRequiredService<IExceptionHandler>()
            .TryHandleAsync(context, context.Features.Get<IExceptionHandlerFeature>()?.Error!, default)));


app.UseCors("EtourAllowPoliciy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
