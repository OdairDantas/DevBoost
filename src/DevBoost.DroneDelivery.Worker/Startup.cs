using AutoMapper;
using DevBoost.DroneDelivery.Core.Domain.Interfaces.Handlers;
using DevBoost.DroneDelivery.Core.Domain.Mediatrs;
using DevBoost.DroneDelivery.Worker.AutoMapper;
using DevBoost.DroneDelivery.Worker.BackgroundWorker;
using DevBoost.DroneDelivery.Worker.Events;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace DevBoost.DroneDelivery.Worker
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddTransient<IMediatrHandler, MediatrHandler>();
            services.AddMediatR(typeof(Startup));
            //var assembly = AppDomain.CurrentDomain.Load("DevBoost.DroneDelivery.Worker");
            //services.AddMediatR(assembly);
            services.AddSingleton<INotificationHandler<PagamentoSolicitadoEvent>, PagamentoEventHandler>();
            services.AddSingleton<INotificationHandler<PedidoSolicitadoEvent>, PedidoEventHandler>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddControllers();
            services.AddHostedService<PedidoBackground>();
            services.AddHostedService<PagamentoBackground>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
