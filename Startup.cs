﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WebApi.Helpers;
using WebApi.Services;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;

namespace WebApi
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // use sql server db in production and sqlite db in development
            if (_env.IsProduction())
                //services.AddDbContext<DataContext>();
                services.AddDbContext<DataContext, SqliteDataContext>();
            else
                services.AddDbContext<DataContext, SqliteDataContext>();

            services.AddCors();
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // configure strongly typed settings objects
            var appSettingsSection = _configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userService.GetById(userId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<IEquipmentFieldService, EquipmentFieldService>();
            services.AddScoped<IEquipmentFileService, EquipmentFileService>();
            services.AddScoped<IEquipmentLinkService, EquipmentLinkService>();
            services.AddScoped<IEquipmentModelFieldService, EquipmentModelFieldService>();
            services.AddScoped<IEquipmentModelFileService, EquipmentModelFileService>();
            services.AddScoped<IEquipmentModelLinkService, EquipmentModelLinkService>();
            services.AddScoped<IEquipmentPartService, EquipmentPartService>();            
            services.AddScoped<IPartService, PartService>();
            services.AddScoped<IPartFieldService, PartFieldService>();
            services.AddScoped<IEquipmentPartService, EquipmentPartService>();
            services.AddScoped<IQrLinkService, QrLinkService>();
            services.AddScoped<IWorkOrderService, WorkOrderService>();
            services.AddScoped<IWOTaskSubService, WOTaskSubService>();
            services.AddScoped<ITaskSubService, TaskSubService>();
            services.AddScoped<IWOPriorityService, WOPriorityService>();
            services.AddScoped<IWOFileService, WOFileService>();
            services.AddScoped<IMediaService, MediaService>();
            services.AddScoped<IEquipmentModelService, EquipmentModelService>();
            services.AddScoped<IEquipmentStatusService, EquipmentStatusService>();
            services.AddScoped<IWOTaskSubFileService, WOTaskSubFileService>();
            services.AddScoped<IPartModelService, PartModelService>();
            services.AddScoped<IPartModelFieldService, PartModelFieldService>();
            services.AddScoped<IPartTypeService, PartTypeService>();
            services.AddScoped<IEquipmentTypeService, EquipmentTypeService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IWOStatusService, WOStatusService>();
            services.AddScoped<IWOExecutionService, WOExecutionService>();
            services.AddScoped<IEquipmentModelPartService, EquipmentModelPartService>();
            services.AddScoped<INotificationService, NotificationService>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            // migrate any database changes on startup (includes initial db creation)
            dataContext.Database.Migrate();

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("Content-Disposition"));  

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
