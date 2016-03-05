namespace Lemonade.Web.Core.Commands
{
    public class UpdateApplicationCommand : ICommand
    {
        public int ApplicationId { get; }
        public string Name { get; }

        public UpdateApplicationCommand(int applicationId, string name)
        {
            ApplicationId = applicationId;
            Name = name;
        }
    }
}