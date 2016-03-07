using System;
using Lemonade.Core.Services;
using Polly;

namespace Lemonade.Services
{
    public class DefaultRetryPolicy : IRetryPolicy
    {
        public DefaultRetryPolicy(int maximumAttempts)
        {
            _maximumAttempts = maximumAttempts;
        }

        public void Execute(Action action)
        {
            var exception = Policy.Handle<Exception>()
                .Retry(_maximumAttempts)
                .ExecuteAndCapture(action)
                .FinalException;

            if (exception != null) throw exception;
        }

        private readonly int _maximumAttempts;
    }
}