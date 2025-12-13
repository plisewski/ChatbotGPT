namespace ChatbotGPT
{
    internal class ConsoleChatUi
    {
        private readonly ChatBotService _chatBotService;

        public ConsoleChatUi(ChatBotService chatBotService)
        {
            _chatBotService = chatBotService;
        }

        public async Task RunAsync()
        {
            Console.WriteLine("ChatBot AI");
            Console.WriteLine("Wpisz pytanie (exit aby zakończyć)");
            Console.WriteLine();

            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();

                if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
                    break;

                var response = await _chatBotService.AskAsync(input);

                Console.WriteLine();
                Console.WriteLine("AI:");
                Console.WriteLine(response);
                Console.WriteLine();
            }
        }
    }
}
