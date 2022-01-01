using CommanderGQL2.Data;
using CommanderGQL2.Models;
using HotChocolate;
using HotChocolate.Types;
using System.Linq;

namespace CommanderGQL2.GraphQL.Platforms
{
    public class PlatformType : ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor) 
        {
            descriptor.Description("represnts any software or service that has a command line interface");
            descriptor.
                Field(p => p.Id).
                Description("Represents the unique ID for the platform ");

            descriptor.
                Field(p => p.Name).
                Description("Represents the name for the platform");

            descriptor.
                Field(p => p.LicenseKey).Ignore();

            descriptor.
                Field(p => p.Commands).
                ResolveWith<Resolvers>(p => p.GetCommands(default!, default!)).
                UseDbContext<AppDbContext>().
                Description("this is the list of commands available for this platform ");
        
        }

        public class Resolvers
        {
            public IQueryable<Command> GetCommands(Platform platform,[ScopedService] AppDbContext context) 
            {
                return context.Commands.Where(p => p.PlatformId == platform.Id);         
            }
            
        }
    }
}
