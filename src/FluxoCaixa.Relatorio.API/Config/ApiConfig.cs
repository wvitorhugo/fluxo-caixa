using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; 
using FluxoCaixa.Shared.Autenticacao;  
using FluxoCaixa.Shared.MessageBus;
using FluxoCaixa.Relatorio.API.Services;
using FluxoCaixa.Relatorio.API.Entities;
using FluxoCaixa.Relatorio.API.BackgroundServices;

namespace FluxoCaixa.Relatorio.API.Config 
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUsuarioLogado, UsuarioLogado>();

            services.AddControllers();

            services.Configure<RabbitMQConfiguration>(configuration.GetSection("RabbitMq"));

            services.Configure<FluxoCaixaDatabaseSettings>
                (configuration.GetSection("DatabaseSettings"));

            services.AddSingleton<LancamentoServices>();

            services.AddHostedService<FluxoCaixaBackgroundService>();


            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FluxoCaixa.Relatorio.API v1"));
            }
             
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });             

            return app;
        }
    }
}
