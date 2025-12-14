using ChatbotGPT.Core;
using OpenAI.Chat;
using System.ClientModel;

namespace ChatbotGPT.Infrastructure
{
    internal class OpenAiChatCompletionClient : IChatCompletionClient
    {
        private readonly ChatClient _client;

        public OpenAiChatCompletionClient(string apiKey)
        {
            _client = new ChatClient(
                model: "gpt-5-mini",
                apiKey: apiKey
            );
        }

        public Task<ClientResult<ChatCompletion>> CompleteChatAsync(
            IReadOnlyList<ChatMessage> messages)
        {
            return _client.CompleteChatAsync(messages);
        }
    }
}