using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordJamHelper.Commands
{
    public class TimerCommands : BaseCommandModule
    {
        private Timer _timer;

        [Command("StartJamReminder")]
        public async Task StartJamReminderCommand(CommandContext ctx)
        {
          //  await ctx.Channel.SendMessageAsync("До начала джема остался 21 день ");

            var currentTime = DateTime.Now;
            var nextDueDate = new DateTime(currentTime.Year, 8, 27);
           
            var timeUntilDueDate = nextDueDate - currentTime;
            _timer = new Timer(TimerCallback, ctx, timeUntilDueDate, TimeSpan.FromDays(3));
        }

        private void TimerCallback(object state)
        {
            var ctx = (CommandContext)state;
            var dueDate = new DateTime(DateTime.Now.Year, 8, 27);
            var currentTime = DateTime.Now;

            if(currentTime >= dueDate)
            {
                _timer.Dispose();
                ctx.Channel.SendMessageAsync("Jam начался!");
            }
            else
            {
                var timeUntilDueDate = dueDate - currentTime;

                ctx.Channel.SendMessageAsync($"До джема осталось {timeUntilDueDate.Days} дней.");
            }            
        }
    }
}
