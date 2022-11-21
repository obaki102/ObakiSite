using System.Text.Json.Serialization;

namespace ObakiSite.Application.Features.Animelist.DTO
{
    //Todo: check for nulls and set a default value
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


}


//public class AlternativeTitles
//{
//    [JsonPropertyName("synonyms")]
//    public List<string> Synonyms = new();

//    [JsonPropertyName("en")]
//    public string En = string.Empty;

//    [JsonPropertyName("ja")]
//    public string Ja = string.Empty;
//}

//public class Broadcast
//{
//    [JsonPropertyName("day_of_the_week")]
//    public string DayOfTheWeek = string.Empty;

//    [JsonPropertyName("start_time")]
//    public string StartTime = string.Empty;
//}

//public class Datum
//{
//    [JsonPropertyName("node")]
//    public Node Node = new();
//}

//public class Genre
//{
//    [JsonPropertyName("id")]
//    public int Id = 0;

//    [JsonPropertyName("name")]
//    public string Name = string.Empty;
//}

//public class MainPicture
//{
//    [JsonPropertyName("medium")]
//    public string Medium = string.Empty;

//    [JsonPropertyName("large")]
//    public string Large = string.Empty;
//}

//public class Node
//{
//    [JsonPropertyName("id")]
//    public int Id = 0;

//    [JsonPropertyName("title")]
//    public string Title = string.Empty;

//    [JsonPropertyName("main_picture")]
//    public MainPicture MainPicture = new();

//    [JsonPropertyName("alternative_titles")]
//    public AlternativeTitles AlternativeTitles = new();

//    [JsonPropertyName("start_date")]
//    public string StartDate = string.Empty;

//    [JsonPropertyName("synopsis")]
//    public string Synopsis = string.Empty;

//    [JsonPropertyName("popularity")]
//    public int Popularity = 0;

//    [JsonPropertyName("num_list_users")]
//    public int NumListUsers = 0;

//    [JsonPropertyName("num_scoring_users")]
//    public int NumScoringUsers = 0;

//    [JsonPropertyName("nsfw")]
//    public string Nsfw = string.Empty;

//    [JsonPropertyName("created_at")]
//    public DateTime CreatedAt = DateTime.MinValue;

//    [JsonPropertyName("updated_at")]
//    public DateTime UpdatedAt = DateTime.MinValue;

//    [JsonPropertyName("media_type")]
//    public string MediaType = string.Empty;

//    [JsonPropertyName("status")]
//    public string Status = string.Empty;

//    [JsonPropertyName("genres")]
//    public List<Genre> Genres = new();

//    [JsonPropertyName("num_episodes")]
//    public int NumEpisodes = 0;

//    [JsonPropertyName("start_season")]
//    public StartSeason StartSeason = new();

//    [JsonPropertyName("broadcast")]
//    public Broadcast Broadcast = new();

//    [JsonPropertyName("source")]
//    public string Source = string.Empty;

//    [JsonPropertyName("average_episode_duration")]
//    public int AverageEpisodeDuration = 0;

//    [JsonPropertyName("rating")]
//    public string Rating = string.Empty;

//    [JsonPropertyName("studios")]
//    public List<Studio> Studios = new();

//    [JsonPropertyName("end_date")]
//    public string EndDate = string.Empty;

//    //[JsonPropertyName("rank")]
//    //public int Rank { get { return Rank?? 0}; set; }
//}

//public class Paging
//{
//}

//public class AnimeListRoot
//{
//    [JsonPropertyName("data")]
//    public List<Datum> Data = new();

//    [JsonPropertyName("paging")]
//    public Paging Paging = new();

//    [JsonPropertyName("season")]
//    public Season Season = new();
//}

//public class Season
//{
//    [JsonPropertyName("year")]
//    public int Year = 1990;

//    [JsonPropertyName("seasonOfTheYear")]
//    public string SeasonOfTheYear = string.Empty;
//}

//public class StartSeason
//{
//    [JsonPropertyName("year")]
//    public int Year = 1990;

//    [JsonPropertyName("season")]
//    public string Season = string.Empty;
//}

//public class Studio
//{
//    [JsonPropertyName("id")]
//    public int Id = 0;

//    [JsonPropertyName("name")]
//    public string Name = string.Empty;
//}