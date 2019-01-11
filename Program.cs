using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;


namespace Quizbot
{
    class Program
    {   
        static DiscordClient discord;
        static CommandsNextModule commands;
        static InteractivityModule interactivity;
        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            discord = new DiscordClient(new DiscordConfiguration 
            {Token = "{Place your bot token here}", TokenType = TokenType.Bot
            });
            // Command prefix is '!'
            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = "!"
            });

            commands.RegisterCommands<MyCommand>();
            interactivity = discord.UseInteractivity(new InteractivityConfiguration
            {
                
            });
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }   
}
