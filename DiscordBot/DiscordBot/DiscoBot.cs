using System;
using Discord.Net;
using Discord.Commands;
using Discord;

/**
 * Created by Visual Studio.
 * Developer: Deuse
 * Date: 12.12.2016
 * Creation Time: 14:54
 * Last update: 23.12.2016 at 21:37
 */

namespace DiscordBot
{
    class DiscoBot
    {
        //Variables
        public const string Token = "MjU3ODMwMzIyMDc3NTY0OTI4.CzApSA.E_WYyUDq62k32u06dTzOzbWUiqk";
        public string[] Images;

        DiscordClient discord;
        CommandService commands;
        Message[] MessagesToDelete;
        Random Rand;

        //Bot logic
        public DiscoBot()
        {
            Images = new string[]
            {
                @"C:\Users\Dom\Desktop\current dump\img1.jpg",
                @"C:\Users\Dom\Desktop\current dump\img2.jpg",
                @"C:\Users\Dom\Desktop\current dump\img3.jpg",
                @"C:\Users\Dom\Desktop\current dump\img4.jpg",
                @"C:\Users\Dom\Desktop\current dump\img5.jpg",
            };

            //Make a logging
            discord = new DiscordClient(x=>     
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            //Before our commands we must use '/'
            discord.UsingCommands(x =>      
            {
                x.PrefixChar = '/';
                x.AllowMentionPrefix = true;
            });

            //Getting a ComandService methods
            commands = discord.GetService<CommandService>();

            Greetings();
            ShowImg();
            Clear();

            //Connection of our bot to the chat server, using the token
            discord.ExecuteAndWait(async () =>      
            {
                await discord.Connect(Token, TokenType.Bot);
            });
        }

        //Bot commands
        private void Greetings()
        {
            commands.CreateCommand("Hi")
                .Do(async (cmd) =>
                {
                    await cmd.Channel.SendMessage("Greetings, my friend!");
                });
        }

        private void ShowImg()
        {
            commands.CreateCommand("Show")
                .Do(async (cmd) =>
                {
                    int RandomImgIndex = Rand.Next(Images.Length);
                    string ImageToShow = Images[RandomImgIndex];
                    await cmd.Channel.SendFile(ImageToShow);
                });
        }

        private void Clear() 
        {
            commands.CreateCommand("Dump")
                .Do(async (cmd) =>
                {
                    MessagesToDelete = await cmd.Channel.DownloadMessages(50);
                    await cmd.Channel.DeleteMessages(MessagesToDelete);   
                });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("[" + DateTime.Now.ToString() + "]" + " " + "Bot started!" );
        }
    }
}
