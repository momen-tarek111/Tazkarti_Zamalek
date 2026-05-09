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

			HashSet<int> sentMatches = new();
			await telegramService.SendMessage("?? Bot Started Successfully");
			while (!stoppingToken.IsCancellationRequested)
			{
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

							if (!sentMatches.Contains(match.MatchId))
							{
								var msg =
											$"""
								?? New Match Alert

								? {match.Team1Ar} vs {match.Team2Ar}

								?? {match.Stadium}

								?? {match.KickOffTime}

								?? https://www.tazkarti.com/#/matches
								""";

								await telegramService.SendMessage(msg);

								Console.WriteLine("? Telegram Message Sent");

								sentMatches.Add(match.MatchId);
							}
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"? ERROR: {ex.Message}");
				}

				Console.WriteLine("? Waiting 30 seconds...");
				await Task.Delay(30000, stoppingToken);
			}
		}
	}
}
