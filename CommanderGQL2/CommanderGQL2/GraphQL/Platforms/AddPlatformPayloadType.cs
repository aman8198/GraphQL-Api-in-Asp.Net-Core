using HotChocolate.Types;

namespace CommanderGQL2.GraphQL.Platforms
{
    public class AddPlatformPayloadType :ObjectType<AddPlatformPayload>
    {
        protected override void Configure(IObjectTypeDescriptor<AddPlatformPayload> descriptor) 
        {
            descriptor.Description("Represents the payload for an added platform.");
            descriptor.Field(p => p.platform).Description("Represents the added platform.");

            base.Configure(descriptor);
        
        }
    }
}
