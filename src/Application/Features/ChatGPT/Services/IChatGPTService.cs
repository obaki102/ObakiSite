
namespace ObakiSite.Application.Features.ChatGPT.Services
{
    public interface IChatGPTService
    {
        Task<string> AskChatGpt(string question);
    }
}
