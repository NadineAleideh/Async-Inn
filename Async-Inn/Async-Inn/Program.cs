using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
// the Tools package provides us with essential commandlines it's allow us like to write a queries 
namespace Async_Inn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            string? connString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services
                .AddDbContext<AsyncInnDbContext>
                (opions => opions.UseSqlServer(connString));

            // to register, setup, configures the identity service to our app
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                //here we can set various options to costumize the identity service in our app
                options.User.RequireUniqueEmail = true; // this force that each user must have a uniqe address
            }).AddEntityFrameworkStores<AsyncInnDbContext>();

            builder.Services.AddScoped<IUser, IdentityUserServices>();
            builder.Services.AddScoped<IAmenity, AmenityServices>();
            builder.Services.AddScoped<IRoom, RoomServices>();
            builder.Services.AddScoped<IHotel, HotelServices>();
            builder.Services.AddScoped<IHotelRoom, HotelRoomServices>();
            builder.Services.AddScoped<JwtTokenService>();


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // Tell the authenticaion scheme "how/where" to validate the token + secret
                options.TokenValidationParameters = JwtTokenService.GetValidationPerameters(builder.Configuration);
            });


            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Create", policy => policy.RequireClaim("permissions", "Create"));
                options.AddPolicy("Read", policy => policy.RequireClaim("permissions", "Read"));
                options.AddPolicy("Update", policy => policy.RequireClaim("permissions", "Update"));
                options.AddPolicy("Delete", policy => policy.RequireClaim("permissions", "Delete"));

            });
            builder.Services.AddAuthorization();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "HotelSystem",
                    Version = "v1",
                });
            });


            var app = builder.Build();


            app.UseSwagger(aptions =>
            {
                aptions.RouteTemplate = "/api/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(aptions =>
            {
                aptions.SwaggerEndpoint("/api/v1/swagger.json", "HotelSystem");
                aptions.RoutePrefix = string.Empty;
            });
            //aptions.RoutePrefix = "docs";

            app.MapControllers();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}