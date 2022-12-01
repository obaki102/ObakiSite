using Moq;
using ObakiSite.Application.Features.Email.Commands;
using ObakiSite.Shared.Models;

using System.Net;


namespace ObakiSite.Tests.Features.Email
{
    public class SendEmailTests
    {
        [Fact]
        [Trait("SendEmailTests", "SendEmail")]
        public async Task SendEmail_Http200_ShouldReturnTrue()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("http://localhost:7080/api/sendEmail")
                   .Respond(HttpStatusCode.OK);
            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:7080");

            var httpFactory = new Mock<IHttpClientFactory>();
            httpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var handler = new SendEmailHandler(httpFactory.Object);

            var dummyEmailMessage = new SendEmail
            (
               new EmailMessage
               {
                   RecipientEmail = "joshuajpiluden@gmail.com",
                   RecipientName = "May",
                   Message = "test"

               }
            );

            //Act
            var result = await handler.Handle(dummyEmailMessage, default);

            //Assert
            Assert.True(result.IsSuccess);

        }
    }
}
