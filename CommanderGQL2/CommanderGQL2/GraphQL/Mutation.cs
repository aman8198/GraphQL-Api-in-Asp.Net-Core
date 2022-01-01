using CommanderGQL2.Data;
using CommanderGQL2.GraphQL.Commands;
using CommanderGQL2.GraphQL.Platforms;
using CommanderGQL2.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using System.Threading;
using System.Threading.Tasks;

namespace CommanderGQL2.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        [GraphQLDescription("Represents the mutations available.")]
        public async Task<AddPlatformPayload> AddPlatformAsync(
            AddPlatformInput input,
            [ScopedService] AppDbContext context,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken 
            ) 
        {
            var platform = new Platform {
            Name = input.Name
            };
            context.Platforms.Add(platform);
            await context.SaveChangesAsync(cancellationToken);
            await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded), platform, cancellationToken);
            return new AddPlatformPayload(platform);



        }

        [UseDbContext(typeof(AppDbContext))]
        [GraphQLDescription("Adds a command.")]
        public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, [ScopedService] AppDbContext context) 
        {
            var command = new Command { 
             HowTo = input.HowTo,
             CommandLine = input.CommandLine,
             PlatformId = input.PlatformId
            };

            context.Commands.Add(command);
            await context.SaveChangesAsync();
            return new AddCommandPayload(command);
        
        }

         
    }
}
