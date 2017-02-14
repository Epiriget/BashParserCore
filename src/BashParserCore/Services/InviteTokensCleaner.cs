using BashParserCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BashParserCore.Services
{
    interface IInviteTokensCleaner
    {
        void removeOutdatedTokens();
    }

    public class InviteTokensCleaner : IInviteTokensCleaner
    {
        private ApplicationDbContext context;
        public InviteTokensCleaner(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void removeOutdatedTokens()
        {
            var time = DateTime.Now;
            var toDelete = context.Invitees.Where(p => (time - p.SendingTime).TotalHours >= 24).ToList();
            context.Invitees.RemoveRange(toDelete);
            context.SaveChanges();
        }
    }


}
