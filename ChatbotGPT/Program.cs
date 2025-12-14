using Microsoft.Extensions.Configuration;

namespace ChatbotGPT
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            var apiKey = configuration["OpenAI:ApiKey"];

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                Console.WriteLine("OpenAI API key is missing.");
                Console.WriteLine("Please configure it using User Secrets or environment variables.");
                return;
            }

            var chatBotService = new ChatBotService(apiKey);
            var ui = new ConsoleChatUi(chatBotService);

            await ui.RunAsync();
        }
    }
}
