using System.Text.Json.Serialization;

namespace ObakiSite.Shared.DTO
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
        [property: JsonPropertyName("end_date")] string EndDate
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
        [property: JsonPropertyName("season")] string SeasonName
    );

    public record StartSeason(
        [property: JsonPropertyName("year")] int Year,
        [property: JsonPropertyName("season")] string SeasonName
    );

    public record Studio(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("name")] string Name
    );



}
