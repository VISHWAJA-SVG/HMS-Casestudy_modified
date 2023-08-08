using HotelManagement_backend.DataModels;
using HotelManagement_backend.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace HotelManagement_backend
{
    public static class StartUp
    {
      public static WebApplication InitializeApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            Configure(app);
            return app;
        }
        private static void ConfigureServices(WebApplicationBuilder builder) {
            var Configuration = new ConfigurationBuilder()
           .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            builder.Services.AddCors((options) =>
            {
                options.AddPolicy("angularApplication", (builder) =>
                {
                    builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .WithMethods("GET", "POST", "PUT", "DELETE")
                    .WithExposedHeaders("*");
                });
            });

            builder.Services.AddControllers();

            builder.Services.AddDbContext<HotelDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HotelBookingDb")));
            builder.Services.AddDbContext<HotelAuthDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HotelBookingIdentityDb")));

             builder.Services.AddScoped<IRoomRepository,RoomRepository>();
             builder.Services.AddScoped<IGuestRepository, GuestRespository>();
             builder.Services.AddScoped<IStaffRepository, StaffRepository>();
             builder.Services.AddScoped<IDepartmentRepository,DepartmentsRepository>();
             //builder.Services.AddScoped<IInventoryRepository,InventoriesRepository>();
            
             builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
             builder.Services.AddScoped<IPaymentRepository,PaymentRepository>();
             builder.Services.AddScoped<IBillRepository, BillRepository>();
             builder.Services.AddScoped<ITokenRepository, TokenRepository>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
           //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Hotel Booking API", Version = "v1" });
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        },
                        Scheme="Oauth2",
                        Name = JwtBearerDefaults.AuthenticationScheme,
                        In = ParameterLocation.Header
                      },
                      new List<string>()
                    }
                });
            });

            builder.Services.AddAutoMapper(typeof(StartUp).Assembly);

            builder.Services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("HotelManagement_backend")
                .AddEntityFrameworkStores<HotelAuthDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                });
         }
        private static void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("angularApplication");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapPost("/api/auth/logout", async context =>
                {
                    await context.SignOutAsync(IdentityConstants.ApplicationScheme);
                    context.Response.Redirect("/");
                });
            });
        }

       
    }
}
