using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TF.BasketballStats
{
    public static class Extensions
    {
        public static void CopyPropertiesFrom(this object targetObject, object sourceObject, params string[] excludePropertyNames)
        {
            CopyProperties(sourceObject, targetObject, excludePropertyNames);
        }
        public static void CopyPropertiesTo(this object sourceObject, object targetObject, params string[] excludePropertyNames)
        {
            CopyProperties(sourceObject, targetObject, excludePropertyNames);
        }
        public static void CopyProperties(object src, object target, params string[] excludePropertyNames)
        {
            var srcProps = src.GetType().GetProperties().ToList();
            var targetProps = target.GetType().GetProperties().Where(e => e.CanWrite).ToList();
            var ex = excludePropertyNames.Select(e => e.ToLower()).ToArray();
            foreach (var targetProp in targetProps)
            {
                var srcProp = srcProps.Find(e => e.Name == targetProp.Name);
                if (srcProp == null || ex.Contains(srcProp.Name.ToLower()) || !srcProp.PropertyType.EqualsInheritsImplements(targetProp.PropertyType)) continue;
                try
                {
                    targetProp.SetValue(target, srcProp.GetValue(src));
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        /// <example>string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;</example>
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        public static Type GetFirstGenericTypeOf<T>(this Type src)
        {
            if (src == null) return null;
            if (src.IsGenericType)
                foreach (var t in src.GenericTypeArguments)
                    if (t.EqualsInheritsImplements(typeof(T)))
                        return t;
            return GetFirstGenericTypeOf<T>(src.BaseType);
        }


        /// <summary>
        /// Checks if current type is equal to, or inherits specified type.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="otherType"></param>
        /// <returns></returns>
        public static bool EqualsInheritsImplements(this Type src, Type otherType)
        {
            if (src == otherType)
            {
                return true;
            }

            return src.GetParentTypes().Any(t => t == otherType);
        }


        public static IEnumerable<Type> GetParentTypes(this Type type)
        {
            var info = type?.GetTypeInfo();
            // is there any base type?
            if (info?.BaseType == null)
            {
                yield break;
            }

            // return all implemented or inherited interfaces
            foreach (var i in info?.ImplementedInterfaces)
            {
                yield return i;
            }

            // return all inherited types
            var currentBaseType = info?.BaseType;
            while (currentBaseType != null)
            {
                yield return currentBaseType;

                currentBaseType = currentBaseType.GetTypeInfo().BaseType;
            }
        }
    }
}
