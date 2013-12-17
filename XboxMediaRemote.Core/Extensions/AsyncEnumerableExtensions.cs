using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XboxMediaRemote.Core.Extensions
{
    public static class AsyncEnumerableExtensions
    {
        public static async Task<IEnumerable<TResult>> SelectAsync<TSource, TResult>(this IEnumerable<TSource> values, Func<TSource, Task<TResult>> asyncSelector)
        {
            return await Task.WhenAll(values.Select(asyncSelector));
        }

        public static Task WhenAllAsync<T>(this IEnumerable<T> values, Func<T, Task> asyncMethod)
        {
            return Task.WhenAll(values.Select(asyncMethod));
        }
    }
}
