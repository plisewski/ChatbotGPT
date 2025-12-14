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
            Console.WriteLine("AI ChatBot");
            Console.WriteLine("Type your question and press Enter.");
            Console.WriteLine("Type 'exit' to quit.");
            Console.WriteLine();

            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();

                if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
                    break;

                var response = await _chatBotService.AskAsync(input);

                Console.WriteLine();
                Console.WriteLine("AI response:");
                Console.WriteLine(response);
                Console.WriteLine();
            }
        }
    }
}
