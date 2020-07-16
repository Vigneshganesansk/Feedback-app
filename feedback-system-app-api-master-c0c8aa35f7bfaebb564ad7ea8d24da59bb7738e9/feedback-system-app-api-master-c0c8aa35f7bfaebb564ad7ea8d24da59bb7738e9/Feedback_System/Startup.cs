using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Feedback_System.Entities;
using Feedback_System.Repository;
using Feedback_System.Repository.Interface;
using Feedback_System.Services;
using Feedback_System.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Feedback_System
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors( o => o.AddPolicy("AllowAll", builder => {
            //    builder.AllowAnyHeader()
            //            .WithMethods("GET", "POST", "PUT")
            //            .WithOrigins(
            //        "http://localhost:4200/"
            //        ).AllowCredentials();
            //}));
            services.AddCors(o => o.AddPolicy("AllowAll", builder => 
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials()
            ));

            services.AddDbContext<FeedbackDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DBConnection"))
                );
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IHomeRepository, HomeRepository>();
            services.AddScoped<IMasterDataService, MasterDataService>();
            services.AddScoped<IMasterDataRepository, MasterDataRepository>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExcelUploadService, ExcelUploadService>();

            services.Configure<IISOptions>( options => options.AutomaticAuthentication = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAll");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
