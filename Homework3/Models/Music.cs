using CsvHelper.Configuration.Attributes;

namespace homework3_livecharts.Models;

public class Music
{
    [Name("User_ID")]
    public string User_ID { get; set; }
    
    [Name("Age")]
    public int Age { get; set; }
    
    [Name("Country")]
    public string Country { get; set; }
    
    [Name("Streaming Platform")]
    public string StreamingPlatform { get; set; }
    
    [Name("Top Genre")]
    public string TopGenre { get; set; }
    
    [Name("Minutes Streamed Per Day")]
    public int MinutesStreamedPerDay { get; set; }
    
    [Name("Number of Songs Liked")]
    public int NumberOfSongsLiked { get; set; }
    
    [Name("Most Played Artist")]
    public string MostPlayedArtist { get; set; }
    
    [Name("Subscription Type")]
    public string SubscriptionType { get; set; }
    
    [Name("Listening Time (Morning/Afternoon/Night)")]
    public string ListeningTime { get; set; }
    
    [Name("Discover Weekly Engagement (%)")]
    public decimal DiscoverWeeklyEngagement { get; set; }
    
    [Name("Repeat Song Rate (%)")]
    public decimal RepeatSongRate { get; set; }
    
}