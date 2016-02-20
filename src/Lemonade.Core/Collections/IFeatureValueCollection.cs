using System;

namespace Lemonade.Core.Collections
{
    public interface IFeatureValueCollection : IValueCollection<bool>
    {
        void Execute(string key, Action action);
        void Execute(Func<dynamic, dynamic> keyFunction, Action action);
    }
}