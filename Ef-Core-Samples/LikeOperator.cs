using EFCore6.Core.Entities;
using EFCore6.Infra;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef_Core_Samples
{
    public class Operators
    {
        public async Task RunSamples(DbContextOptionsBuilder<PubContext> contextOptions)
        {
            using (var context = new PubContext(contextOptions.Options))
            {
                await Like(context);
            }
        }

        private async Task Like(PubContext context)
        {
            // Contains will always generate a like function = %a%, so use EF.Functions.Like if we need more control
            var authorsNameEndWith = await context.Authors.Where(a => EF.Functions.Like(a.FirstName, "%a")).ToListAsync();
        }

        private async Task Find(PubContext context)
        {
            // it is not a LINQ query, it is a method on DB set, that will generate a Select Top(1) 
            var authorsNameEndWith = await context.Authors.FindAsync(1);
        }

        private async Task GetAuthors(PubContext context)
        {
            foreach (var item in context.Authors)
            {
                //Do some work
                //Avoid logic when looping as the connection remains
                // open till the loop exits
            }
        }


    }
}
