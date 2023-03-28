using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;
using System.Text;

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
            var response = new StringBuilder();
            var completionResult = _openAIService.Completions.CreateCompletionAsStream(new CompletionCreateRequest()
            {
                Prompt = question,
                MaxTokens = 100
            }, Models.TextDavinciV3);

            await foreach (var completion in completionResult)
            {
                if (completion.Successful)
                {
                    response.Append(completion.Choices.FirstOrDefault()?.Text ?? "No response");
                }
                else
                {
                    if (completion.Error == null)
                    {
                        throw new Exception("Unknown Error");
                    }

                    response.Append($"{completion.Error.Code}: {completion.Error.Message}");
                }
            }

            return response.ToString();
        }
    }
}
