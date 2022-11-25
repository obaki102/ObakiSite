using Moq;
using ObakiSite.Application.Features.Animelist.Services;
using System.Net;

namespace ObakiSite.Tests.Features.Animelist
{
    public class AnimeListServiceTests
    {

        [Fact]
        [Trait("AnimeListHttpServiceTests", "GetAnimeListBySeasonAndYear")]
        public async Task GetAnimeListBySeasonAndYear_ValidResult_ShouldReturnTrue()
        {
            //Arrange
            string testData = System.IO.File.ReadAllText(@$"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}\TestData\animelistData.json");
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://api.myanimelist.net/*")
                   .Respond("application/json", testData);
            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = new Uri( "https://api.myanimelist.net/");
           
            var httpFactory = new Mock<IHttpClientFactory>();
            httpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
            var _animeListHttpService = new AnimeListService(httpFactory.Object);

            //Act
            var response = await _animeListHttpService.GetAnimeListBySeasonAndYear(2022, "fall");

            //Assert
            Assert.True(response.IsSuccess);
        }

        [Fact]
        [Trait("AnimeListHttpServiceTests", "GetAnimeListBySeasonAndYear")]
        public async Task GetAnimeListBySeasonAndYear_HttpStatusCode502_ShouldReturnFalse()
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://api.myanimelist.net/*")
                   .Respond(HttpStatusCode.BadGateway);
            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = new Uri("https://api.myanimelist.net/");

            var httpFactory = new Mock<IHttpClientFactory>();
            httpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
            var _animeListHttpService = new AnimeListService(httpFactory.Object);

            //Act
            var response = await _animeListHttpService.GetAnimeListBySeasonAndYear(2022, "fall");

            //Assert
            Assert.False(response.IsSuccess);
        }

        [Fact]
        [Trait("AnimeListHttpServiceTests", "GetAnimeListBySeasonAndYear")]
        public async Task GetAnimeListBySeasonAndYear_HttpStatusCode504_ShouldReturnFalse()
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://api.myanimelist.net/*")
                   .Respond(HttpStatusCode.GatewayTimeout);
            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = new Uri("https://api.myanimelist.net/");

            var httpFactory = new Mock<IHttpClientFactory>();
            httpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
            var _animeListHttpService = new AnimeListService(httpFactory.Object);

            //Act
            var response = await _animeListHttpService.GetAnimeListBySeasonAndYear(2022, "fall");

            //Assert
            Assert.False(response.IsSuccess);
        }

        //[Fact]
        //[Trait("AnimeListHttpServiceTests", "GetAnimeListBySeasonAndYear")]
        //public async Task GetAnimeListBySeasonAndYear_ThrowException_ShouldReturnFalse()
        //{
        //    //Arrange
        //    var mockHttp = new MockHttpMessageHandler();
        //    mockHttp.When("https://api.myanimelist.net/*")
        //           .Throw(new Exception("Simulate Exception"));
        //    var httpClient = mockHttp.ToHttpClient();
        //    httpClient.BaseAddress = new Uri("https://api.myanimelist.net/");

        //    var httpFactory = new Mock<IHttpClientFactory>();
        //    httpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
        //    var _animeListHttpService = new AnimeListService(httpFactory.Object);

        //    //Act
        //    var response = await _animeListHttpService.GetAnimeListBySeasonAndYear(2022, "fall");

        //    //Assert
        //    Assert.Null(response);

        //}


    }
}
