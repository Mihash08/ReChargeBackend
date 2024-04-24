
using Microsoft.EntityFrameworkCore;
using ReCharge.Data;
using Data.Interfaces;
using Data.Repositories;
using ReCharge.Data.Interfaces;
using ReChargeBackend.Data;
using Microsoft.AspNetCore.Identity;
using Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddControllers().AddJsonOptions(x =>
//   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
//builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ReCharge"));
});


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVerificationCodeRepository, VerificationCodeRepository>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ISlotRepository, SlotRepository>();
builder.Services.AddScoped<IAdminUserRepository, AdminUserRepository>();


var host = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
if (host.Environment.IsDevelopment())
{
    host.UseSwagger();
    host.UseSwaggerUI();
} else
{
    host.UseSwagger();
    host.UseSwaggerUI();
}

host.UseHttpsRedirection();

host.UseAuthorization();
host.UseAuthentication();

host.MapControllers();

TestSeedData.EnsurePopulated(host);

await host.RunAsync();
