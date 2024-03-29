﻿//using AutoMapper;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Options;
//using Moq;
//using ObakiSite.Application.Features.Email.Constants;
//using ObakiSite.Application.Features.Email.Services;
//using ObakiSite.Application.Shared.DTO;
//using ObakiSite.Application.Shared.DTO.Response;

//namespace ObakiSite.Tests.Features.Email
//{
//    public class EmailServiceTests
//    {
//        private IConfigurationRoot _configuration;
//        private IMapper _mapper;

//        public EmailServiceTests()
//        {
//            var mockMapper = new Mock<IMapper>();   
//            _configuration = new ConfigurationBuilder()
//               .AddUserSecrets<EmailServiceTests>()
//               .Build();
//            _mapper = mockMapper.Object;
//        }

//        [Fact]
//        [Trait("EmailServiceTests", "SendMessage")]
//        public void  SendMessage_ValidEmailMessage_ShouldReturnTrue()
//        {
//            //Arrange
//            var message = new EmailMessageDTO
//            {
//                RecipientEmail = "joshuajpiluden@gmail.com",
//                RecipientName = "May",
//                Message = "test"
//            };

//            var mockEmailClient = new Mock<IEmailService>();
//            mockEmailClient.Setup(x => x.SendEmail(It.IsAny<EmailMessageDTO>()).Result).Returns(ApplicationResponse.Success());

//            //Act
//            var result = mockEmailClient.Object.SendEmail(message);

//            //Assert
//            Assert.True(result.Result.IsSuccess);
//        }

//        [Fact]
//        [Trait("EmailServiceTests", "SendMessage")]
//        public void SendMessage_InValidEmailMessage_ShouldReturnFalse()
//        {
//            //Arrange
//            var message = new EmailMessageDTO
//            {
//                RecipientEmail = "joshuajpiluden@gmail.com",
//                RecipientName = "May",
//                Message = "Invalid"
//            };

//            var mockEmailClient = new Mock<IEmailService>();
//            mockEmailClient.Setup(x => x.SendEmail(It.IsAny<EmailMessageDTO>()).Result).Returns(ApplicationResponse.Fail());

//            //Act
//            var result = mockEmailClient.Object.SendEmail(message);

//            //Assert
//            Assert.False(result.Result.IsSuccess);
//        }

//        [Fact]
//        [Trait("EmailServiceTests", "SendMessage")]
//        public void SendMessage_NoRecipient_ShouldReturnFalse()
//        {
//            //Arrange
//            var mockHttp = new MockHttpMessageHandler();
//            var mockHttpClient = mockHttp.ToHttpClient();
//            var httpFactory = new Mock<IHttpClientFactory>();
//            httpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(mockHttpClient);

//            var message = new EmailMessageDTO();

//            var ioptions  = Options.Create(new EmailServiceOptions
//            {
//                AppPassword = _configuration[EmailConstants.AppPassword]
//            });
            
           
//            var emailClient = new EmailService(ioptions, httpFactory.Object) ;

//            //Act
//            var result = emailClient.SendEmail(message);

//            //Assert
//            Assert.False(result.Result.IsSuccess);
//        }


//        [Fact]
//        [Trait("EmailServiceTests", "SendMessage")]
//        public void SendMessage_InValidCredentials_ShouldReturnFalse()
//        {
//            //Arrange
//            var mockHttp = new MockHttpMessageHandler();
//            var mockHttpClient = mockHttp.ToHttpClient();

//            var httpFactory = new Mock<IHttpClientFactory>();
//            httpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(mockHttpClient);

//            var message = new EmailMessageDTO();
//            var ioptions = Options.Create(new EmailServiceOptions
//            {
//                AppPassword = "InvalidCredemtial"
//            });
//            var emailClient = new EmailService(ioptions, httpFactory.Object);

//            //Act
//            var result = emailClient.SendEmail(message);

//            //Assert
//            Assert.False(result.Result.IsSuccess);
//        }

//        [Fact]
//        [Trait("EmailServiceTests", "SendMessage")]
//        public void SendMessage_ValidCredentials_ShouldReturnTrue()
//        {
//            //Arrange
//            var mockHttp = new MockHttpMessageHandler();
//            var mockHttpClient = mockHttp.ToHttpClient();
//            var httpFactory = new Mock<IHttpClientFactory>();
//            httpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(mockHttpClient);

//            var message = new EmailMessageDTO
//            {
//                RecipientEmail = "joshuajpiluden@gmail.com",
//                RecipientName = "May",
//                Message = "Test"
//            };
//            var ioptions = Options.Create(new EmailServiceOptions
//            {
//                AppPassword = _configuration[EmailConstants.AppPassword]
//            });

//            var emailClient = new EmailService(ioptions, httpFactory.Object);

//            //Act
//            var result = emailClient.SendEmail(message);

//            //Assert
//            Assert.True(result.Result.IsSuccess);
//        }
//    }
//}
