using RabbitMQ.Client;
using System;
using System.Text;

namespace Mensageiro
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare( queue: "hello",
                                          durable: false,
                                          exclusive: false,
                                          autoDelete: false,
                                          arguments: null );

                    string message = "";

                    do
                    {
                        Console.WriteLine("Digite 0 para sair:");
                        Console.WriteLine("Digite uma mensagem:");
                       
                        message = Console.ReadLine(); 

                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "",
                                              routingKey: "hello",
                                              basicProperties: null,
                                              body: body);

                        Console.WriteLine();
                        Console.WriteLine("[x] Sent {0}", message);

                        Console.WriteLine("-----------------------------------------------------");

                    } while (message != "0");
                }

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
