namespace Lemonade.Web.Core.Commands
{
    public class DeleteResourceCommand : ICommand
    {
        public int ResourceId { get; }

        public DeleteResourceCommand(int resourceId)
        {
            ResourceId = resourceId;
        }
    }
}