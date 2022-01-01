using CommanderGQL2.Data;
using CommanderGQL2.Models;
using HotChocolate;
using HotChocolate.Data;
using System.Linq;

namespace CommanderGQL2.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext context) 
        {
            return context.Platforms;
        
        }

        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
       public IQueryable<Command> GetCommands([ScopedService] AppDbContext context)
        {
            return context.Commands;
        }
    }
} 
