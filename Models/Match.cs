using System.Text.Json.Serialization;
namespace WorkerService1.Models
{

	public class Match
	{
		[JsonPropertyName("matchId")]
		public int MatchId { get; set; }

		[JsonPropertyName("teamName1")]
		public string Team1 { get; set; }

		[JsonPropertyName("teamName2")]
		public string Team2 { get; set; }

		[JsonPropertyName("teamNameAr1")]
		public string Team1Ar { get; set; }

		[JsonPropertyName("teamNameAr2")]
		public string Team2Ar { get; set; }

		[JsonPropertyName("kickOffTime")]
		public DateTime KickOffTime { get; set; }

		[JsonPropertyName("stadiumNameAr")]
		public string Stadium { get; set; }
	}
}
