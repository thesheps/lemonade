using System;

namespace Lemonade.Core.Services
{
    public interface IRetryPolicy
    {
        void Execute(Action action);
    }
}