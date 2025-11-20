using EasyNetQ;
using EasyPC.Model.Messages;
using EasyPC.Subscriber.Services;
using Microsoft.Extensions.Configuration;

Console.WriteLine("===========================================");
Console.WriteLine("    EasyPC Email Subscriber Started");
Console.WriteLine("===========================================");
Console.WriteLine($"Started at: {DateTime.Now}");
Console.WriteLine();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

var rabbitMqHost = configuration["RabbitMQ__Host"] ?? configuration["RabbitMQ:Host"] ?? "localhost";
var rabbitMqUsername = configuration["RabbitMQ__Username"] ?? configuration["RabbitMQ:Username"] ?? "admin";
var rabbitMqPassword = configuration["RabbitMQ__Password"] ?? configuration["RabbitMQ:Password"] ?? "admin";
var rabbitMqVirtualHost = configuration["RabbitMQ__VirtualHost"] ?? configuration["RabbitMQ:VirtualHost"] ?? "/";
var connectionString = $"host={rabbitMqHost};virtualHost={rabbitMqVirtualHost};username={rabbitMqUsername};password={rabbitMqPassword}";

Console.WriteLine($"Connecting to RabbitMQ: {rabbitMqHost}");

using var bus = RabbitHutch.CreateBus(connectionString);
var emailService = new EmailService(configuration);

Console.WriteLine("Subscribing to OrderEmailMessage queue...");
Console.WriteLine("Waiting for messages. Press CTRL+C to exit.");
Console.WriteLine();

bus.PubSub.Subscribe<OrderEmailMessage>("order_email_subscription", async message =>
{
    try
    {
        Console.WriteLine($"[{DateTime.Now}] Received order email request:");
        Console.WriteLine($"  - Order ID: {message.OrderId}");
        Console.WriteLine($"  - Customer: {message.UserName} ({message.UserEmail})");
        Console.WriteLine($"  - Total: ${message.TotalPrice:F2}");
        Console.WriteLine($"  - Items: {message.OrderItems.Count}");

        await emailService.SendOrderConfirmationEmail(message);

        Console.WriteLine($"[{DateTime.Now}]  Email sent successfully!");
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[{DateTime.Now}]  Error processing message: {ex.Message}");
        Console.WriteLine($"  Stack Trace: {ex.StackTrace}");
        Console.WriteLine();
    }
});

Console.WriteLine("Subscriber is running. Press CTRL+C to stop...");

var cancellationTokenSource = new CancellationTokenSource();
Console.CancelKeyPress += (sender, e) =>
{
    e.Cancel = true;
    cancellationTokenSource.Cancel();
    Console.WriteLine();
    Console.WriteLine("Shutting down subscriber...");
};

try
{
    await Task.Delay(Timeout.Infinite, cancellationTokenSource.Token);
}
catch (TaskCanceledException)
{
    Console.WriteLine("Subscriber stopped.");
}
