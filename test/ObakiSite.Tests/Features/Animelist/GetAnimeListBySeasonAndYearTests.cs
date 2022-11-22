﻿using Moq;
using ObakiSite.Shared.DTO;
using ObakiSite.Application.Features.Animelist.Queries;
using ObakiSite.Application.Features.LocalStorageCache.Services;
using ObakiSite.Shared.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ObakiSite.Tests.Features.Animelist
{
    public  class GetAnimeListBySeasonAndYearTests
    {

        [Fact]
        [Trait("GetAnimeListBySeasonAndYearTests", "GetAnimeListBySeasonAndYear")]
        public async Task GetAnimeListBySeasonAndYear_CacheIsEmpty_ShouldReturnTrueAndNotNull()
        {
            //Arrange
            string testData = System.IO.File.ReadAllText(@$"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}\TestData\animelistData.json");
            var mockTestData = ApplicationResponse<AnimeListRoot>.Success(JsonSerializer.Deserialize<AnimeListRoot>(testData)); 

            var mockHttp = new MockHttpMessageHandler();
            var mockHttpClient = mockHttp.ToHttpClient();
            var httpFactory = new Mock<IHttpClientFactory>();
            httpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(mockHttpClient);

            var mockLocalStorageCache = new Mock<ILocalStorageCache<AnimeListRoot>>();
            mockLocalStorageCache.Setup(x => x.IsDataNeedsRefresh().Result).Returns(false);
            mockLocalStorageCache.Setup(x => x.GetCacheData().Result).Returns(mockTestData);

            var handler = new GetAnimeListBySeasonAndYearHandler(httpFactory.Object, mockLocalStorageCache.Object);
            var dummySeason = new GetAnimeListBySeasonAndYear(new Season(2022, "fall"));


            //Act
            var result = await handler.Handle(dummySeason, default);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);

        }

        [Fact]
        [Trait("GetAnimeListBySeasonAndYearTests", "GetAnimeListBySeasonAndYear")]
        public async Task GetAnimeListBySeasonAndYear_CacheIsEmptyNeedsDataRefresh_ShouldReturnTrueAndNotNull()
        {
            //Arrange
            string testData = System.IO.File.ReadAllText(@$"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}\TestData\animelistData.json");
            var mockTestData = ApplicationResponse<AnimeListRoot>.Success(JsonSerializer.Deserialize<AnimeListRoot>(testData));

            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("http://localhost:7080/api/animelists/fall/2022")
                   .Respond("application/json", testData);
            var httpClient = mockHttp.ToHttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:7080");

            var httpFactory = new Mock<IHttpClientFactory>();
            httpFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var mockLocalStorageCache = new Mock<ILocalStorageCache<AnimeListRoot>>();
            mockLocalStorageCache.Setup(x => x.IsDataNeedsRefresh().Result).Returns(true);
            mockLocalStorageCache.Setup(x => x.GetCacheData().Result).Returns(mockTestData);

            var handler = new GetAnimeListBySeasonAndYearHandler(httpFactory.Object, mockLocalStorageCache.Object);
            var dummySeason = new GetAnimeListBySeasonAndYear(new Season(2022, "fall"));


            //Act
            var result = await handler.Handle(dummySeason, default);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);

        }
    }

}
