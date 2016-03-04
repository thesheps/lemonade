namespace Lemonade.Web.Core.Commands
{
    public class CreateApplicationCommand : ICommand
    {
        public string Name { get; }

        public CreateApplicationCommand(string name)
        {
            Name = name;
        }
    }
}