using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DevBoost.DroneDelivery.CrossCutting.IOC;
using DevBoost.DroneDelivery.Infrastructure.Swagger;
using System.Diagnostics.CodeAnalysis;
using DevBoost.DroneDelivery.Infrastructure.Security;
using DevBoost.DroneDelivery.Infrastructure.Data.Contexts;
using Rebus.ServiceProvider;
using Rebus.Kafka;
using Rebus.Routing.TypeBased;
using Confluent.Kafka;
using DevBoost.DroneDelivery.Core.Domain.Messages;
using DevBoost.DroneDelivery.Core.Domain.Messages.IntegrationEvents;
using DevBoost.DroneDelivery.Application.Events;
using Rebus.Persistence.InMem;
using DevBoost.DroneDelivery.Application.Sagas;

namespace DevBoost.DroneDelivery.API
{
    [ExcludeFromCodeCoverage]

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var nomeFila = "fila_rebus";
            services.AddRebus(configure => configure
               //.Transport(t => t.UseInMemoryTransport(new InMemNetwork(), nomeFila))
               .Transport(t => t.UseKafka("localhost:9092", nomeFila))
               //.Subscriptions(s => s.StoreInMemory())
               .Routing(r =>
               {
                   r.TypeBased()
                       .MapAssemblyOf<Message>(nomeFila)
                       .MapAssemblyOf<PedidoAdicionadoEvent>(nomeFila)
                       .MapAssemblyOf<PedidoDespachadoEvent>(nomeFila);
               })
               .Sagas(s => s.StoreInMemory())
               .Options(o =>
               {
                   o.SetNumberOfWorkers(1);
                   o.SetMaxParallelism(1);
                   o.SetBusName("Demo Rebus");
               })
           );
            services.AutoRegisterHandlersFromAssemblyOf<PedidoSaga>();

            services.AddDbContext<DCDroneDelivery>();
            services.Register(Configuration);
            services.SwaggerAdd();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    //ValidIssuer = TokenGenerator.TokenConfig.Emissor,
                    ClockSkew = TimeSpan.FromMinutes(30),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(TokenGenerator.TokenConfig.ObterChave()),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireAudience = true
                    
                };
            });


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ApplicationServices.UseRebus(async e => await e.Subscribe<PedidoSolicitadoEvent>());
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            

            app.SwaggerAdd();
          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
