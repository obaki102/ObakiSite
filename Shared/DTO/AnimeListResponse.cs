using System.Text.Json.Serialization;

namespace ObakiSite.Shared.DTO
{
    public record AlternativeTitles(
        [property: JsonPropertyName("synonyms")] IReadOnlyList<string> Synonyms,
        [property: JsonPropertyName("en")] string En,
        [property: JsonPropertyName("ja")] string Ja
    );

    public record Broadcast(
        [property: JsonPropertyName("dayOfTheWeek")] string DayOfTheWeek,
        [property: JsonPropertyName("startTime")] string StartTime
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
        [property: JsonPropertyName("mainPicture")] MainPicture MainPicture,
        [property: JsonPropertyName("alternativeTitles")] AlternativeTitles AlternativeTitles,
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
        [property: JsonPropertyName("genres")] IReadOnlyList<Genre> Genres,
        [property: JsonPropertyName("numEpisodes")] int NumEpisodes,
        [property: JsonPropertyName("startSeason")] StartSeason StartSeason,
        [property: JsonPropertyName("broadcast")] Broadcast Broadcast,
        [property: JsonPropertyName("source")] string Source,
        [property: JsonPropertyName("averageEpisodeDuration")] int AverageEpisodeDuration,
        [property: JsonPropertyName("rating")] string Rating,
        [property: JsonPropertyName("studios")] IReadOnlyList<Studio> Studios,
        [property: JsonPropertyName("endDate")] string EndDate,
        [property: JsonPropertyName("rank")] int Rank
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
        [property: JsonPropertyName("seasonOfTheYear")] string SeasonOfTheYear
    );

    public record StartSeason(
        [property: JsonPropertyName("year")] int Year,
        [property: JsonPropertyName("season")] string Season
    );

    public record Studio(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("name")] string Name
    );


}
