using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.Services;
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


            builder.Services.AddScoped<IAmenity, AmenityServices>();
            builder.Services.AddScoped<IRoom, RoomServices>();
            builder.Services.AddScoped<IHotel, HotelServices>();
            builder.Services.AddScoped<IHotelRoom, HotelRoomServices>();


            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "HotelSystemm2",
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
                aptions.SwaggerEndpoint("/api/v1/swagger.json", "HotelSystemm2");
                aptions.RoutePrefix = string.Empty;
            });
            //aptions.RoutePrefix = "docs";

            app.MapControllers();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}