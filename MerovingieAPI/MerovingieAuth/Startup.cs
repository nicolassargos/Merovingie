﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MerovingieAuth.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Merovingie;
using MerovingieAuth.Hubs;
using Microsoft.AspNetCore.Identity.UI.Services;
using Common;

namespace MerovingieAuth
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private MSocketHandler msocketHandler;

        public Startup(IConfiguration configuration)
        {
            msocketHandler = new MSocketHandler();
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IEmailSender, EmailSender>();
            
            services.AddSignalR();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Debug);
            loggerFactory.AddDebug(LogLevel.Debug);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Websockets Options
            var webSocketsOptions = new WebSocketOptions
            {
                KeepAliveInterval = TimeSpan.FromSeconds(300),
                ReceiveBufferSize = 4 * 1024
            };
            app.UseWebSockets(webSocketsOptions);

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        var socket = await context.WebSockets.AcceptWebSocketAsync();
                        await msocketHandler.Listen(context, socket);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }

                }
                else
                {
                    await next();
                }
            });

        }
    }
}