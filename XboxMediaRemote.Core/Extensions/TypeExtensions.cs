using System;
using System.Reflection;

namespace XboxMediaRemote.Core.Extensions
{
    public static class TypeExtensions
    {
        public static object GetDefaultValue(this Type type)
        {
            return type.GetTypeInfo().IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
