using System;

namespace Sommite.Extensions
{
    public static class VbImplementations
    {
        public static T With<T>(this T item, Action<T> action)
        {
            action(item);
            return item;
        }
    }
}