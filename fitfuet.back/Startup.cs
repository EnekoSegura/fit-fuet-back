using fit_fuet_back.Context;
using fit_fuet_back.IRepositorios;
using fit_fuet_back.IServicios;
using fit_fuet_back.Repositorios;
using fit_fuet_back.Servicios;
using fitfuet.back.IServices;
using fitfuet.back.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace fitfuet.back
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
            services.AddDbContext<AplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Conexion")));

            //Servicios
            services.AddScoped<IUsuarioService, UsuarioServicio>();
            services.AddScoped<IEjercicioServicio, EjercicicoServicio>();
            services.AddScoped<IAlimentoServicio, AlimentoServicio>();
            services.AddScoped<IChatService, ChatService>();
            services.AddSingleton<IChatStateService, ChatStateService>(); //para solo tener una instancia

            //Repositorios
            services.AddScoped<IUsuarioRepository, UsuarioRepositorio>();
            services.AddScoped<IEjercicioRepositorio, EjercicioRepositorio>();
            services.AddScoped<IAlimentoRepositorio, AlimentoRepositorio>();

            //Cors
            services.AddCors(options => options.AddPolicy("AllowWebapp",
                builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSockets",
                    builder =>
                    {
                        builder.AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowAnyOrigin();
                    });
            });

            //Autenticación
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = Configuration["Jwt:Issuer"],
                     ValidAudience = Configuration["Jwt:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                     ClockSkew = TimeSpan.Zero
                 };
             });

            //Https
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 443;
            });


            services.AddRazorPages();
            //services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowWebapp");

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowSockets");
            app.UseWebSockets();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/ws", async context =>
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        string userId = context.Request.Query["userId"];
                        if (string.IsNullOrEmpty(userId))
                        {
                            context.Response.StatusCode = 400;
                            return;
                        }

                        var chatService = context.RequestServices.GetRequiredService<IChatService>();
                        await chatService.HandleWebSocket(context, Int32.Parse(userId));
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                });
            });
        }
    }
}
