using WorkerService1.Services;

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

		while (!stoppingToken.IsCancellationRequested)
		{
			try
			{
				Console.WriteLine("=================================");
				Console.WriteLine($"Checking matches: {DateTime.Now}");

				var matches = await matchService.GetMatchesAsync();
				foreach (var match in matches)
				{
					bool isZamalek =
						match.Team1.Contains("Zamalek", StringComparison.OrdinalIgnoreCase)
						|| match.Team2.Contains("Zamalek", StringComparison.OrdinalIgnoreCase);

					if (isZamalek)
					{

						if (!sentMatches.Contains(match.MatchId))
						{
							var msg =
									$"""
							🚨 New Match Alert

							⚽ {match.Team1Ar} vs {match.Team2Ar}

							🏟 {match.Stadium}

							🕒 {match.KickOffTime}

							🔗 https://www.tazkarti.com/#/matches
							""";

							await telegramService.SendMessage(msg);

							sentMatches.Add(match.MatchId);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"❌ ERROR: {ex.Message}");
			}

			await Task.Delay(30000, stoppingToken);
		}
	}
}