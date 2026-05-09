using WorkerService1.Services;

namespace WorkerService1
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;

		public Worker(ILogger<Worker> logger)
		{
			_logger = logger;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var matchService = new MatchService();
			var telegramService = new TelegramService();

			try
			{
				Console.WriteLine("=================================");
				Console.WriteLine($"Checking matches: {DateTime.Now}");

				var matches = await matchService.GetMatchesAsync();

				Console.WriteLine($"Matches Count: {matches.Count}");

				foreach (var match in matches)
				{
					Console.WriteLine($"{match.Team1} vs {match.Team2}");

					bool isZamalek =
						match.Team1.Contains("Zamalek", StringComparison.OrdinalIgnoreCase)
						|| match.Team2.Contains("Zamalek", StringComparison.OrdinalIgnoreCase);

					if (isZamalek)
					{
						Console.WriteLine("?? Zamalek Match Found!");

						var msg =
$"""
?? Zamalek Match Found

? {match.Team1Ar} vs {match.Team2Ar}

?? Stadium: {match.Stadium}

?? Kickoff: {match.KickOffTime}

?? https://www.tazkarti.com/#/matches
""";

						await telegramService.SendMessage(msg);

						Console.WriteLine("? Telegram Message Sent");
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"? ERROR: {ex.Message}");
			}

			Console.WriteLine("Finished checking.");
		}
	}
}