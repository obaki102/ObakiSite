using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;

namespace ObakiSite.Application.Features.ChatGPT.Services
{
    public class ChatGPTService : IChatGPTService
    {
        private readonly IOpenAIService _openAIService;

        public ChatGPTService(IOpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        public async Task<string> AskChatGpt(string question)
        {

            var completionResult = await _openAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                 {
                    ChatMessage.FromUser(question)
                 },
                Model = Models.ChatGpt3_5Turbo,
                MaxTokens = 1000
            });


            if (completionResult.Successful)
            {
                return completionResult.Choices.First().Message.Content;
            }

            return $"Error: {completionResult.Error.Message}";
        }
    }
}
