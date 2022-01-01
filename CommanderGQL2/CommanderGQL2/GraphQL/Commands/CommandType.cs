using CommanderGQL2.Data;
using CommanderGQL2.Models;
using HotChocolate;
using HotChocolate.Types;
using System.Linq;

namespace CommanderGQL2.GraphQL.Commands
{
    public class CommandType : ObjectType<Command>
    {
        protected override void Configure(IObjectTypeDescriptor<Command> descriptor) 
        {
            descriptor.Description("Represents any executable Command.");

            descriptor.Field(c => c.Id).
                Description("Represents the unique Id for the command.");

            descriptor.Field(c => c.HowTo).
                 Description("Represents the how-to for the command.");

            descriptor.Field(c => c.CommandLine).
                Description("Represents the command Line");

            descriptor.Field(c => c.PlatformId).
                Description("Represents the uniqueId of the platform which the command belongs.");

            descriptor.Field(c => c.Platform).
                ResolveWith<Resolvers>(c => c.GetPlatform(default!, default!)).
                UseDbContext<AppDbContext>().
                Description("This is the platform to which code belongs");
        
        
        }

        public class Resolvers 
        {
            public Platform GetPlatform(Command command, [ScopedService] AppDbContext context) 
            {
                return context.Platforms.FirstOrDefault(p => p.Id == command.PlatformId);
            }
        }
    }
}
