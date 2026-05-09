
using Telegram.Bot;
namespace WorkerService1.Services
{

	public class TelegramService
	{
		private readonly TelegramBotClient _bot;
		private readonly long _chatId;

		public TelegramService()
		{
			var token = Environment.GetEnvironmentVariable("BOT_TOKEN");

			_chatId = long.Parse(Environment.GetEnvironmentVariable("CHAT_ID"));

			_bot = new TelegramBotClient(token);
		}

		public async Task SendMessage(string message)
		{
			await _bot.SendMessage(_chatId, message);
		}
	}
}
