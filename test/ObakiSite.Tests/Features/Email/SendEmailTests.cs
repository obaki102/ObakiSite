using Moq;
using ObakiSite.Application.Features.Email.Commands;
using System.Net;
using MediatR;


namespace ObakiSite.Tests.Features.Email
{
    public class SendEmailTests
    {
        [Fact]
        [Trait("SendEmailTests", "SendEmail")]
        public async Task SendEmail_ValidRequest_ShouldReturnTrue()
        {
            string testData = File.ReadAllText(@$"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}\TestData\sendEmail_Success.json");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("http://localhost:7080/api/sendEmail/*")
                   .Respond("application/json", testData);
            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:7080");

            var httpFactory = new Mock<IHttpClientFactory>();
            httpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var handler = new SendEmailHandler(httpFactory.Object);
            var dummyEmailMessage = new SendEmail
            {
                RecipientEmail = "joshuajpiluden@gmail.com",
                RecipientName = "Joshua",
                Message = "test"
            };

            //Act
            var result = await handler.Handle(dummyEmailMessage, default);

            //Assert
            Assert.True(result.IsSuccess);

        }

        [Fact]
        [Trait("SendEmailTests", "SendEmail")]
        public async Task SendEmail_Http400_ShouldReturnFalse()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("http://localhost:7080/api/sendEmail/*")
                   .Respond(HttpStatusCode.BadRequest);
            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:7080");

            var httpFactory = new Mock<IHttpClientFactory>();
            httpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var handler = new SendEmailHandler(httpFactory.Object);
            var dummyEmailMessage = new SendEmail
            {
                RecipientEmail = "joshuajpiluden@gmail.com",
                RecipientName = "Joshua",
                Message = "test"
            };

            //Act
            var result = await handler.Handle(dummyEmailMessage, default);

            //Assert
            Assert.False(result.IsSuccess);

        }

        [Fact]
        [Trait("SendEmailTests", "SendEmail")]
        public async Task SendEmail_Http502_ShouldReturnFalse()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("http://localhost:7080/api/sendEmail/*")
                   .Respond(HttpStatusCode.BadGateway);
            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:7080");

            var httpFactory = new Mock<IHttpClientFactory>();
            httpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var handler = new SendEmailHandler(httpFactory.Object);
            var dummyEmailMessage = new SendEmail
            {
                RecipientEmail = "joshuajpiluden@gmail.com",
                RecipientName = "Joshua",
                Message = "test"
            };

            //Act
            var result = await handler.Handle(dummyEmailMessage, default);

            //Assert
            Assert.False(result.IsSuccess);

        }


    }
}
