using SimplifiedBankingApi.Contracts;
using SimplifiedBankingApi.Data;
using System.Text;
using RabbitMQ.Client;
using SimplifiedBankingApi.Models.Dto;
using System.Text.Json;

namespace SimplifiedBankingApi.Repository
{
    public class RabbitMqRepository : IRabbitMqRepository
    {
        private readonly BankContext _context;

        public RabbitMqRepository(BankContext context)
        {
            _context = context;
        }

        public void SendingEmail(TransactionNotifyEmailDto notifyEmail)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "transaction-notify-email",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
            

            var transactionNotifyEmail = JsonSerializer.Serialize(notifyEmail);
            
            string message = transactionNotifyEmail;
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                     routingKey: "transaction-notify-email",
                     basicProperties: null,
                     body: body);

            Console.WriteLine($" [x] Sent {message}");

        }
    }
}
