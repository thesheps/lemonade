namespace Lemonade.Web.Core.Commands
{
    public class DeleteApplicationCommand : ICommand
    {
        public int ApplicationId { get; }

        public DeleteApplicationCommand(int applicationId)
        {
            ApplicationId = applicationId;
        }
    }
}