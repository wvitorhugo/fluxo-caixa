using FluxoCaixa.Relatorio.API.Entities;
using FluxoCaixa.Relatorio.API.Services;
using FluxoCaixa.Shared.MessageBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace FluxoCaixa.Relatorio.API.BackgroundServices
{ 
    public class FluxoCaixaBackgroundService : BackgroundService
    {
        private readonly RabbitMQConfiguration _config;
        private readonly ILogger _logger;
        private IConnection _connection;
        private IModel _channel;
        private readonly LancamentoServices _lancamentoServices;

        public FluxoCaixaBackgroundService(ILoggerFactory loggerFactory, IOptions<RabbitMQConfiguration> options, LancamentoServices LancamentoServices)
        {
            _config = options.Value;
            _lancamentoServices = LancamentoServices;
            this._logger = loggerFactory.CreateLogger<FluxoCaixaBackgroundService>();
            InitRabbitMQ();
        }

        private void InitRabbitMQ()
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = _config.Host
            };

            _connection = factory.CreateConnection();

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _config.Queue,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());

                HandleMessage(content);
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(_config.Queue, false, consumer);
            return Task.CompletedTask;
        }

        private void HandleMessage(string content)
        {            
            var message = JsonSerializer.Deserialize<Lancamento>(content);
            _lancamentoServices.CreateAsync(message);

            _logger.LogInformation($"consumer received {content}");
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, EventArgs e) { } 
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}