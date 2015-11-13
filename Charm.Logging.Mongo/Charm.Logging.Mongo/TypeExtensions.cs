using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Charm.Logging.Mongo
{
    public static class TypeExtensions
    {
        public static Boolean IsAnonymousType(this Type type)
        {
            Boolean hasCompilerGeneratedAttribute =
                type.GetCustomAttributes(typeof (CompilerGeneratedAttribute), false).Any();
            Boolean nameContainsAnonymousType = type.FullName.Contains("AnonymousType");
            Boolean isAnonymousType = hasCompilerGeneratedAttribute && nameContainsAnonymousType;

            return isAnonymousType;
        }
    }
}
