using Microsoft.Extensions.Configuration;

namespace ChatbotGPT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            var apiKey = configuration["OpenAI:ApiKey"];

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                Console.WriteLine("Brak klucza API OpenAI");
                return;
            }

            Console.WriteLine("Klucz API OpenAI został poprawnie wczytany");
        }
    }
}
