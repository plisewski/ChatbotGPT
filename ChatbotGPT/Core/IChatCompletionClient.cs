using OpenAI.Chat;
using System.ClientModel;

namespace ChatbotGPT.Core
{
    internal interface IChatCompletionClient
    {
        Task<ClientResult<ChatCompletion>> CompleteChatAsync(
            IReadOnlyList<ChatMessage> messages);
    }
}