using OpenAI.Chat;

namespace ChatbotGPT
{
    internal class ChatBotService
    {
        private const int MaxHistoryMessages = 10;

        private readonly ChatClient _client;
        private readonly List<ChatMessage> _messages = new();

        public ChatBotService(string apiKey)
        {
            _client = new ChatClient(
                model: "gpt-5-mini",
                apiKey: apiKey
            );

            _messages.Add(new SystemChatMessage(
                "Jesteś pomocnym, rzeczowym asystentem. Odpowiadaj krótko i konkretnie."
            ));
        }

        public async Task<string> AskAsync(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
                return "Puste pytanie.";

            _messages.Add(new UserChatMessage(userInput));
            TrimHistory();

            try
            {
                var response = await _client.CompleteChatAsync(_messages);
                var answer = response.Value.Content[0].Text;

                _messages.Add(new AssistantChatMessage(answer));
                TrimHistory();

                return answer;
            }
            catch (Exception ex)
            {
                return $"Błąd podczas komunikacji z API: {ex.Message}";
            }
        }

        private void TrimHistory()
        {
            if (_messages.Count <= MaxHistoryMessages)
                return;

            var systemMessage = _messages.First();
            var recentMessages = _messages
                .Skip(_messages.Count - MaxHistoryMessages)
                .ToList();

            _messages.Clear();
            _messages.Add(systemMessage);
            _messages.AddRange(recentMessages);
        }
    }
}
