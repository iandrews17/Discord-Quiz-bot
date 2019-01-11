using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity;

namespace Quizbot
{
    public class MyCommand
    {
        static Question quiz;
        [Command("quiz")]
        public async Task Quiz(CommandContext ctx)
        {
			// Creates the new question.
            quiz = new Question();
            var interactivity = ctx.Client.GetInteractivityModule();
            await ctx.RespondAsync($"{quiz.question}, {ctx.User.Mention}!");
            
            var msg = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id
            && xm.Content.ToLower() == quiz.answer, TimeSpan.FromMinutes(1));
            if (msg != null)
            {
				// Responses
                await ctx.RespondAsync($"Correct!");
            }
			else
			{
				await ctx.RespondAsync($"The correct answer is, {quiz.answer}");
			}
        }

    }
}
