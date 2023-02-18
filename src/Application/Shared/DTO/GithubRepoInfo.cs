using System.Text.Json.Serialization;

namespace ObakiSite.Application.Shared.DTO
{
    public class Owner
    {
        [JsonPropertyName("login")]
        public string Login { get; set; } = string.Empty;

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("node_id")]
        public string NodeId { get; set; } = string.Empty;

        [JsonPropertyName("avatar_url")]
        public string AvatarUrl { get; set; } = string.Empty;

        [JsonPropertyName("gravatar_id")]
        public string GravatarId { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("html_url")]
        public string HtmlUrl { get; set; } = string.Empty;

        [JsonPropertyName("followers_url")]
        public string FollowersUrl { get; set; } = string.Empty;

        [JsonPropertyName("following_url")]
        public string FollowingUrl { get; set; } = string.Empty;

        [JsonPropertyName("gists_url")]
        public string GistsUrl { get; set; } = string.Empty;

        [JsonPropertyName("starred_url")]
        public string StarredUrl { get; set; } = string.Empty;

        [JsonPropertyName("subscriptions_url")]
        public string SubscriptionsUrl { get; set; } = string.Empty;

        [JsonPropertyName("organizations_url")]
        public string OrganizationsUrl { get; set; } = string.Empty;

        [JsonPropertyName("repos_url")]
        public string ReposUrl { get; set; } = string.Empty;

        [JsonPropertyName("events_url")]
        public string EventsUrl { get; set; } = string.Empty;

        [JsonPropertyName("received_events_url")]
        public string ReceivedEventsUrl { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("site_admin")]
        public bool SiteAdmin { get; set; }
    }

    public class GithubRepoInfo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("node_id")]
        public string NodeId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("full_name")]
        public string FullName { get; set; } = string.Empty;

        [JsonPropertyName("private")]
        public bool Private { get; set; }

        [JsonPropertyName("owner")]
        public Owner Owner { get; set; } = new();

        [JsonPropertyName("html_url")]
        public string HtmlUrl { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("fork")]
        public bool Fork { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("forks_url")]
        public string ForksUrl { get; set; } = string.Empty;

        [JsonPropertyName("keys_url")]
        public string KeysUrl { get; set; } = string.Empty;

        [JsonPropertyName("collaborators_url")]
        public string CollaboratorsUrl { get; set; } = string.Empty;

        [JsonPropertyName("teams_url")]
        public string TeamsUrl { get; set; } = string.Empty;

        [JsonPropertyName("hooks_url")]
        public string HooksUrl { get; set; } = string.Empty;

        [JsonPropertyName("issue_events_url")]
        public string IssueEventsUrl { get; set; } = string.Empty;

        [JsonPropertyName("events_url")]
        public string EventsUrl { get; set; } = string.Empty;

        [JsonPropertyName("assignees_url")]
        public string AssigneesUrl { get; set; } = string.Empty;

        [JsonPropertyName("branches_url")]
        public string BranchesUrl { get; set; } = string.Empty;

        [JsonPropertyName("tags_url")]
        public string TagsUrl { get; set; } = string.Empty;

        [JsonPropertyName("blobs_url")]
        public string BlobsUrl { get; set; } = string.Empty;

        [JsonPropertyName("git_tags_url")]
        public string GitTagsUrl { get; set; } = string.Empty;

        [JsonPropertyName("git_refs_url")]
        public string GitRefsUrl { get; set; } = string.Empty;

        [JsonPropertyName("trees_url")]
        public string TreesUrl { get; set; } = string.Empty;

        [JsonPropertyName("statuses_url")]
        public string StatusesUrl { get; set; } = string.Empty;

        [JsonPropertyName("languages_url")]
        public string LanguagesUrl { get; set; } = string.Empty;

        [JsonPropertyName("stargazers_url")]
        public string StargazersUrl { get; set; } = string.Empty;

        [JsonPropertyName("contributors_url")]
        public string ContributorsUrl { get; set; } = string.Empty;

        [JsonPropertyName("subscribers_url")]
        public string SubscribersUrl { get; set; } = string.Empty;

        [JsonPropertyName("subscription_url")]
        public string SubscriptionUrl { get; set; } = string.Empty;

        [JsonPropertyName("commits_url")]
        public string CommitsUrl { get; set; } = string.Empty;

        [JsonPropertyName("git_commits_url")]
        public string GitCommitsUrl { get; set; } = string.Empty;

        [JsonPropertyName("comments_url")]
        public string CommentsUrl { get; set; } = string.Empty;

        [JsonPropertyName("issue_comment_url")]
        public string IssueCommentUrl { get; set; } = string.Empty;

        [JsonPropertyName("contents_url")]
        public string ContentsUrl { get; set; } = string.Empty;

        [JsonPropertyName("compare_url")]
        public string CompareUrl { get; set; } = string.Empty;

        [JsonPropertyName("merges_url")]
        public string MergesUrl { get; set; } = string.Empty;

        [JsonPropertyName("archive_url")]
        public string ArchiveUrl { get; set; } = string.Empty;

        [JsonPropertyName("downloads_url")]
        public string DownloadsUrl { get; set; } = string.Empty;

        [JsonPropertyName("issues_url")]
        public string IssuesUrl { get; set; } = string.Empty;

        [JsonPropertyName("pulls_url")]
        public string PullsUrl { get; set; } = string.Empty;

        [JsonPropertyName("milestones_url")]
        public string MilestonesUrl { get; set; } = string.Empty;

        [JsonPropertyName("notifications_url")]
        public string NotificationsUrl { get; set; } = string.Empty;

        [JsonPropertyName("labels_url")]
        public string LabelsUrl { get; set; } = string.Empty;

        [JsonPropertyName("releases_url")]
        public string ReleasesUrl { get; set; } = string.Empty;

        [JsonPropertyName("deployments_url")]
        public string DeploymentsUrl { get; set; } = string.Empty;

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("pushed_at")]
        public DateTime PushedAt { get; set; }

        [JsonPropertyName("git_url")]
        public string GitUrl { get; set; } = string.Empty;

        [JsonPropertyName("ssh_url")]
        public string SshUrl { get; set; } = string.Empty;

        [JsonPropertyName("clone_url")]
        public string CloneUrl { get; set; } = string.Empty;

        [JsonPropertyName("svn_url")]
        public string SvnUrl { get; set; } = string.Empty;

        [JsonPropertyName("homepage")]
        public string Homepage { get; set; } = string.Empty;

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("stargazers_count")]
        public int StargazersCount { get; set; }

        [JsonPropertyName("watchers_count")]
        public int WatchersCount { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; } = string.Empty;

        [JsonPropertyName("has_issues")]
        public bool HasIssues { get; set; }

        [JsonPropertyName("has_projects")]
        public bool HasProjects { get; set; }

        [JsonPropertyName("has_downloads")]
        public bool HasDownloads { get; set; }

        [JsonPropertyName("has_wiki")]
        public bool HasWiki { get; set; }

        [JsonPropertyName("has_pages")]
        public bool HasPages { get; set; }

        [JsonPropertyName("has_discussions")]
        public bool HasDiscussions { get; set; }

        [JsonPropertyName("forks_count")]
        public int ForksCount { get; set; }

        [JsonPropertyName("mirror_url")]
        public object MirrorUrl { get; set; } = string.Empty;

        [JsonPropertyName("archived")]
        public bool Archived { get; set; }

        [JsonPropertyName("disabled")]
        public bool Disabled { get; set; }

        [JsonPropertyName("open_issues_count")]
        public int OpenIssuesCount { get; set; }

        [JsonPropertyName("license")]
        public object License { get; set; } = string.Empty;

        [JsonPropertyName("allow_forking")]
        public bool AllowForking { get; set; }

        [JsonPropertyName("is_template")]
        public bool IsTemplate { get; set; }

        [JsonPropertyName("web_commit_signoff_required")]
        public bool WebCommitSignoffRequired { get; set; }

        [JsonPropertyName("topics")]
        public List<string> Topics { get; } = new List<string>();

        [JsonPropertyName("visibility")]
        public string Visibility { get; set; } = string.Empty;

        [JsonPropertyName("forks")]
        public int Forks { get; set; }

        [JsonPropertyName("open_issues")]
        public int OpenIssues { get; set; }

        [JsonPropertyName("watchers")]
        public int Watchers { get; set; }

        [JsonPropertyName("default_branch")]
        public string DefaultBranch { get; set; } = string.Empty;

        [JsonPropertyName("temp_clone_token")]
        public object TempCloneToken { get; set; } = string.Empty;

        [JsonPropertyName("network_count")]
        public int NetworkCount { get; set; }

        [JsonPropertyName("subscribers_count")]
        public int SubscribersCount { get; set; }
    }


}
