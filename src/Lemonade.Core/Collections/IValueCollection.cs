using System;
using System.Linq.Expressions;

namespace Lemonade.Core.Collections
{
    public interface IValueCollection<out T>
    {
        T this[Func<dynamic, dynamic> keyFunction] { get; }
        T this[string key] { get; }
        T Get<TExpression>(Expression<Func<TExpression, dynamic>> expression);
    }
}