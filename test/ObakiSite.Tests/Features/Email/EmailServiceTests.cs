using Microsoft.Extensions.Configuration;
using Moq;
using ObakiSite.Application.Features.Email.Services;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.Models;
using ObakiSite.Shared.Models.Response;

namespace ObakiSite.Tests.Features.Email
{
    public class EmailServiceTests
    {
        private IConfigurationRoot _configuration;

        public EmailServiceTests()
        {
            _configuration = new ConfigurationBuilder()
               .AddUserSecrets<EmailServiceTests>()
               .Build();
        }

        [Fact]
        [Trait("EmailServiceTests", "SendMessage")]
        public void  SendMessage_ValidEmailMessage_ShouldReturnTrue()
        {
            //Arrange
            var message = new EmailMessage
            {
                RecipientEmail = "joshuajpiluden@gmail.com",
                RecipientName = "May",
                Message = "test"
            };

            var mockEmailClient = new Mock<IEmailService>();
            mockEmailClient.Setup(x => x.SendEmail(It.IsAny<EmailMessage>()).Result).Returns(ApplicationResponse.Success());

            //Act
            var result = mockEmailClient.Object.SendEmail(message);

            //Assert
            Assert.True(result.Result.IsSuccess);
        }

        [Fact]
        [Trait("EmailServiceTests", "SendMessage")]
        public void SendMessage_InValidEmailMessage_ShouldReturnFalse()
        {
            //Arrange
            var message = new EmailMessage
            {
                RecipientEmail = "joshuajpiluden@gmail.com",
                RecipientName = "May",
                Message = "Invalid"
            };

            var mockEmailClient = new Mock<IEmailService>();
            mockEmailClient.Setup(x => x.SendEmail(It.IsAny<EmailMessage>()).Result).Returns(ApplicationResponse.Fail());

            //Act
            var result = mockEmailClient.Object.SendEmail(message);

            //Assert
            Assert.False(result.Result.IsSuccess);
        }

        [Fact]
        [Trait("EmailServiceTests", "SendMessage")]
        public void SendMessage_NoRecipient_ShouldReturnFalse()
        {
            //Arrange
            var message = new EmailMessage();
            var options = new EmailServiceOptions
            {
                AppPassword = _configuration[EmailConstants.AppPassword]
            };
            var emailClient = new EmailService(options);

            //Act
            var result = emailClient.SendEmail(message);

            //Assert
            Assert.False(result.Result.IsSuccess);
        }


        [Fact]
        [Trait("EmailServiceTests", "SendMessage")]
        public void SendMessage_InValidCredentials_ShouldReturnFalse()
        {
            //Arrange
            var message = new EmailMessage();
            var options = new EmailServiceOptions
            {
                AppPassword = "InvalidCredemtial"
            };
            var emailClient = new EmailService(options);

            //Act
            var result = emailClient.SendEmail(message);

            //Assert
            Assert.False(result.Result.IsSuccess);
        }

        [Fact]
        [Trait("EmailServiceTests", "SendMessage")]
        public void SendMessage_ValidCredentials_ShouldReturnTrue()
        {
            //Arrange
            var message = new EmailMessage
            {
                RecipientEmail = "joshuajpiluden@gmail.com",
                RecipientName = "May",
                Message = "Test"
            };
            var options = new EmailServiceOptions
            {
                AppPassword = _configuration[EmailConstants.AppPassword]
            };
            var emailClient = new EmailService(options);

            //Act
            var result = emailClient.SendEmail(message);

            //Assert
            Assert.True(result.Result.IsSuccess);
        }
    }
}
