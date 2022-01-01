using CommanderGQL2.Models;
using HotChocolate;
using HotChocolate.Types;

namespace CommanderGQL2.GraphQL
{
    [GraphQLDescription("represents the queries available.")]
    public class Subscription
    {
        [Subscribe]
        [Topic]
        [GraphQLDescription("The subscription for an added Platform.")]
        public Platform OnPlatformAdded([EventMessage] Platform platform)
        {
            return platform;
        }
    }
}
