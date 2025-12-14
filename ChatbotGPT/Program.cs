using ChatbotGPT.Core;
using ChatbotGPT.Infrastructure;
using ChatbotGPT.UI;
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

            // Manual DI (composition root)
            IChatCompletionClient completionClient =
                new OpenAiChatCompletionClient(apiKey);

            IChatBotService chatBotService =
                new ChatBotService(completionClient);

            var ui = new ConsoleChatUi(chatBotService);

            await ui.RunAsync();
        }
    }
}
