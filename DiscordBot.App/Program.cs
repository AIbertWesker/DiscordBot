using Discord;
using Discord.WebSocket;

namespace DiscordBot.App
{
    public class Program
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private static DiscordSocketClient _client;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public static async Task Main()
        {
            _client = new DiscordSocketClient();

            var config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
            };

            _client = new DiscordSocketClient(config);

            //TODO Token z JSON
            var token = "";

            await _client.LoginAsync(TokenType.Bot, token);
            _client.MessageReceived += MessageReceivedAsync;
            _client.Ready += ReadyAsync;
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private static async Task MessageReceivedAsync(SocketMessage message)
        {
            if (message.Author.Id == _client.CurrentUser.Id)
                return;


            if (message.Content == "!ping")
            {
                var cb = new ComponentBuilder()
                    .WithButton("Click me!", "unique-id", ButtonStyle.Primary);
                await message.Channel.SendMessageAsync("pong!", components: cb.Build());
            }
        }

        private static Task ReadyAsync()
        {
            Console.WriteLine($"{_client.CurrentUser} is connected!");

            return Task.CompletedTask;
        }
    }
}
