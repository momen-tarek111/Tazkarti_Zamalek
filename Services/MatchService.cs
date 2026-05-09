using System.Text.Json;
using WorkerService1.Models;
namespace WorkerService1.Services
{

	public class MatchService
	{
		private readonly HttpClient _httpClient;

		private const string URL ="https://www.tazkarti.com/data/matches-list-json.json";

		public MatchService()
		{
			_httpClient = new HttpClient();
		}

		public async Task<List<Match>> GetMatchesAsync()
		{
			var response = await _httpClient.GetStringAsync(URL);

			var matches = JsonSerializer.Deserialize<List<Match>>(response);

			return matches ?? new List<Match>();
		}
	}
}
