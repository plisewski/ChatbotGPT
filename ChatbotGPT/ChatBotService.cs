using OpenAI.Chat;
using System.ClientModel;

namespace ChatbotGPT
{
    internal class ChatBotService
    {
        private const int MaxHistoryMessages = 10;

        // Retry configuration
        private const int MaxRetries = 3;
        private const int InitialDelayMs = 500;

        private readonly ChatClient _client;
        private readonly List<ChatMessage> _messages = new();

        public ChatBotService(string apiKey)
        {
            _client = new ChatClient(
                model: "gpt-5-mini",
                apiKey: apiKey
            );

            // System prompt – always kept
            _messages.Add(new SystemChatMessage(
                "You are a helpful and concise assistant. Respond briefly and to the point."
            ));
        }

        public async Task<string> AskAsync(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
                return "Empty input is not allowed.";

            _messages.Add(new UserChatMessage(userInput));
            TrimHistory();

            try
            {
                var response = await ExecuteWithRetryAsync(() =>
                    _client.CompleteChatAsync(_messages)
                );

                var answer = response.Value.Content[0].Text;

                _messages.Add(new AssistantChatMessage(answer));
                TrimHistory();

                return answer;
            }
            catch (Exception ex)
            {
                return $"Error while communicating with the OpenAI API: {ex.Message}";
            }
        }

        private async Task<ClientResult<ChatCompletion>> ExecuteWithRetryAsync(
            Func<Task<ClientResult<ChatCompletion>>> action)
        {
            int delayMs = InitialDelayMs;

            for (int attempt = 1; attempt <= MaxRetries; attempt++)
            {
                try
                {
                    return await action();
                }
                catch (Exception) when (attempt < MaxRetries)
                {
                    await Task.Delay(delayMs);
                    delayMs *= 2;
                }
            }

            // Last attempt – let the exception propagate
            return await action();
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
