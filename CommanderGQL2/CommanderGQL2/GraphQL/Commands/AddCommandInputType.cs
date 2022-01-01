using HotChocolate.Types;

namespace CommanderGQL2.GraphQL.Commands
{
    public class AddCommandInputType : InputObjectType<AddCommandInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AddCommandInput> descriptor)
        {
            descriptor.Description("Represent the input to add for a command.");
            descriptor.Field(c => c.HowTo).
                Description("Represents the HowTo for the command.");
            descriptor.Field(c => c.CommandLine).
                Description("Represents the Command Line");
            descriptor.Field(c=>c.PlatformId).
                Description("Represents the unique Id of the platform which the command belongs.");

            base.Configure(descriptor);
        }
    }
}
