namespace ChatbotGPT.Core
{
    internal interface IChatBotService
    {
        /// <summary>
        /// Sends a user input to the chat service and returns the assistant's response.
        /// </summary>
        /// <param name="userInput">User input text.</param>
        /// <returns>Assistant response text.</returns>
        Task<string> AskAsync(string userInput);
    }
}
