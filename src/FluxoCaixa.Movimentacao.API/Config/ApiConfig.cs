using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; 
using FluxoCaixa.Movimentacao.Application.Services; 
using FluxoCaixa.Shared.Autenticacao;
using FluxoCaixa.Movimentacao.API.Middlewares;
using FluxoCaixa.Movimentacao.Application.Interfaces; 
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Reflection;
using System.Linq;
using FluxoCaixa.Movimentacao.Infra.Database;
using FluxoCaixa.Movimentacao.Infra.Extensions;
using FluxoCaixa.Shared.MessageBus;

namespace FluxoCaixa.Movimentacao.API.Config
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblies(Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load));

            services.AddDbContext<FluxoCaixaContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("FluxoCaixaContext")));

            services.SetupUnitOfWork();

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<ILancamentoService, LancamentoService>();
            services.Configure<RabbitMQConfiguration>(configuration.GetSection("RabbitMq"));
            services.AddSingleton<IMessageProducer, MessageProducer> ();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUsuarioLogado, UsuarioLogado>();

            services.AddControllers(); 

            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FluxoCaixa.Movimentacao.API v1"));
            }

            app.UseMiddleware<ExceptionHandler>();

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
