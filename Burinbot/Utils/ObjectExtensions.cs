using System;
using System.Collections.Generic;
using System.Reflection;

namespace Burinbot.Utils
{
    public static class ObjectExtensions
    {
        public static Dictionary<string, T> ReturnDictionaryOfAllObjectProperties<T>(this object objectToIterateThrough)
        {
            Type t = objectToIterateThrough.GetType();
            PropertyInfo[] objectProperties = t.GetProperties();
            Dictionary<string, T> DictionaryOfObjectProperties = new Dictionary<string, T>();

            if (objectToIterateThrough == null)
                throw new NullReferenceException();

            foreach (PropertyInfo property in objectProperties)
            {
                var value = property.GetValue(objectToIterateThrough);
                DictionaryOfObjectProperties.Add(property.Name, (T)value);
            }

            return DictionaryOfObjectProperties;
        }
    }
}
