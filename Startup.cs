using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RelicsAPI.Data;
using RelicsAPI.Data.Repositories;
using RelicsAPI.Auth;
using RelicsAPI.Data.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using RelicsAPI.Auth.Model;
using RelicsAPI.Services;
using Amazon.S3;
using Amazon.Extensions.NETCore.Setup;
using Amazon;
using RelicsAPI.Services.Models;
using RelicsAPI.Services.Helpers;
using System.Configuration;

namespace RelicsAPI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<RelicsContext>()
                .AddDefaultTokenProviders();

            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);

            var tokenValidationParams = new TokenValidationParameters
            {
                ValidIssuer = _configuration["JWT:ValidIssuer"],
                ValidAudience = _configuration["JWT:ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            services.AddSingleton(tokenValidationParams);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParams;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyNames.SameUser, policy => policy.Requirements.Add(new SameUserRequirement()));;
            });

            services.AddSingleton<IAuthorizationHandler, SameUserAuthorizationHandler>();
            services.AddDbContext<RelicsContext>();
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<ICategoriesRepository, CategoriesRepository>();
            services.AddTransient<IRelicsRepository, RelicsRepository>();
            services.AddTransient<IOrdersRepository, OrdersRepository>();
            services.AddTransient<ITokenManager, TokenManager>();
            services.AddTransient<DatabaseSeeder, DatabaseSeeder>();

            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            // ASW S3 service for image upload
            var appSettingsSection = _configuration.GetSection("ServiceConfiguration");
            services.AddAWSService<IAmazonS3>();
            services.Configure<ServiceConfiguration>(appSettingsSection);
            services.AddTransient<IAWSS3FileService, AWSS3FileService>();
            services.AddTransient<IAWSS3BucketHelper, AWSS3BucketHelper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MyPolicy");

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
