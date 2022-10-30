using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ObakiSite.Api.DTO
{
    public record AlternativeTitles(
   [property: JsonPropertyName("synonyms")] IReadOnlyList<string> Synonyms,
   [property: JsonPropertyName("en")] string En,
   [property: JsonPropertyName("ja")] string Ja
);

    public record Broadcast(
        [property: JsonPropertyName("day_of_the_week")] string DayOfTheWeek,
        [property: JsonPropertyName("start_time")] string StartTime
    );

    public record Datum(
        [property: JsonPropertyName("node")] Node Node
    );

    public record Genre(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("name")] string Name
    );

    public record MainPicture(
        [property: JsonPropertyName("medium")] string Medium,
        [property: JsonPropertyName("large")] string Large
    );

    public record Node(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("title")] string Title,
        [property: JsonPropertyName("main_picture")] MainPicture MainPicture,
        [property: JsonPropertyName("alternative_titles")] AlternativeTitles AlternativeTitles,
        [property: JsonPropertyName("start_date")] string StartDate,
        [property: JsonPropertyName("synopsis")] string Synopsis,
        [property: JsonPropertyName("popularity")] int Popularity,
        [property: JsonPropertyName("num_list_users")] int NumListUsers,
        [property: JsonPropertyName("num_scoring_users")] int NumScoringUsers,
        [property: JsonPropertyName("nsfw")] string Nsfw,
        [property: JsonPropertyName("created_at")] DateTime CreatedAt,
        [property: JsonPropertyName("updated_at")] DateTime UpdatedAt,
        [property: JsonPropertyName("media_type")] string MediaType,
        [property: JsonPropertyName("status")] string Status,
        [property: JsonPropertyName("genres")] IReadOnlyList<Genre> Genres,
        [property: JsonPropertyName("num_episodes")] int NumEpisodes,
        [property: JsonPropertyName("start_season")] StartSeason StartSeason,
        [property: JsonPropertyName("broadcast")] Broadcast Broadcast,
        [property: JsonPropertyName("source")] string Source,
        [property: JsonPropertyName("average_episode_duration")] int AverageEpisodeDuration,
        [property: JsonPropertyName("rating")] string Rating,
        [property: JsonPropertyName("studios")] IReadOnlyList<Studio> Studios,
        [property: JsonPropertyName("end_date")] string EndDate,
        [property: JsonPropertyName("rank")] int? Rank
    );

    public record Paging(

    );

    public record AnimeListRoot(
        [property: JsonPropertyName("data")] IReadOnlyList<Datum> Data,
        [property: JsonPropertyName("paging")] Paging Paging,
        [property: JsonPropertyName("season")] Season Season
    );

    public record Season(
        [property: JsonPropertyName("year")] int Year,
        [property: JsonPropertyName("season")] string SeasonOfTheYear
    );

    public record StartSeason(
        [property: JsonPropertyName("year")] int Year,
        [property: JsonPropertyName("season")] string Season
    );

    public record Studio(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("name")] string Name
    );


    public record AlternativeTitles2(
        [property: JsonPropertyName("synonyms")] IReadOnlyList<string> Synonyms,
        [property: JsonPropertyName("en")] string En,
        [property: JsonPropertyName("ja")] string Ja
    );

    public record Broadcast2(
        [property: JsonPropertyName("dayOfTheWeek")] string DayOfTheWeek,
        [property: JsonPropertyName("startTime")] string StartTime
    );

    public record Genre2(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("name")] string Name
    );

    public record MainPicture2(
        [property: JsonPropertyName("medium")] string Medium,
        [property: JsonPropertyName("large")] string Large
    );

    public record Node2(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("title")] string Title,
        [property: JsonPropertyName("mainPicture")] MainPicture2 MainPicture,
        [property: JsonPropertyName("alternativeTitles")] AlternativeTitles2 AlternativeTitles,
        [property: JsonPropertyName("startDate")] string StartDate,
        [property: JsonPropertyName("synopsis")] string Synopsis,
        [property: JsonPropertyName("popularity")] int Popularity,
        [property: JsonPropertyName("numListUsers")] int NumListUsers,
        [property: JsonPropertyName("numScoringUsers")] int NumScoringUsers,
        [property: JsonPropertyName("nsfw")] string Nsfw,
        [property: JsonPropertyName("createdAt")] DateTime CreatedAt,
        [property: JsonPropertyName("updatedAt")] DateTime UpdatedAt,
        [property: JsonPropertyName("mediaType")] string MediaType,
        [property: JsonPropertyName("status")] string Status,
        [property: JsonPropertyName("genres")] IReadOnlyList<Genre2> Genres,
        [property: JsonPropertyName("numEpisodes")] int NumEpisodes,
        [property: JsonPropertyName("startSeason")] StartSeason2 StartSeason,
        [property: JsonPropertyName("broadcast")] Broadcast2 Broadcast,
        [property: JsonPropertyName("source")] string Source,
        [property: JsonPropertyName("averageEpisodeDuration")] int AverageEpisodeDuration,
        [property: JsonPropertyName("rating")] string Rating,
        [property: JsonPropertyName("studios")] IReadOnlyList<Studio2> Studios,
        [property: JsonPropertyName("endDate")] string EndDate,
        [property: JsonPropertyName("rank")] int Rank
    );

    public record AnimeListRoot2(
        [property: JsonPropertyName("node")] Node2 Node
    );

    public record StartSeason2(
        [property: JsonPropertyName("year")] int Year,
        [property: JsonPropertyName("season")] string Season
    );

    public record Studio2(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("name")] string Name
    );

}
