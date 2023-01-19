using black_list_ms_candidate.Domain;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace black_list_ms_candidate.Infrastructure.Events
{
    public class KafkaProducer
    {
        private readonly ILogger<KafkaProducer> _logger;
        private IProducer<Null, string> _producer;

        public KafkaProducer(ILogger<KafkaProducer> logger)
        {
            _logger = logger;
            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092"
            };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task StartAsync(Candidate candidate)
        {
            for (int i = 0; i < 1; ++i)
            {
                var nameCandidate = candidate.Name;
                var nameMentor = "Cleidoaldo Ribeiro";
                var value = $"Novo candidato adicionado: Nome: {nameCandidate} | Nome do mentor: {nameMentor}.";
                _logger.LogInformation(value);
                await _producer.ProduceAsync("demo", new Message<Null, string>()
                {
                    Value = value
                });

            }

            _producer.Flush(TimeSpan.FromSeconds(10));
        }

        public Task StopAsync()
        {
            _producer?.Dispose();
            return Task.CompletedTask;
        }
    }
}
