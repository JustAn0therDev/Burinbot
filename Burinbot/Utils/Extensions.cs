using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Burinbot.Utils
{
    public static class Extensions
    {
        public static Dictionary<string, T> GetDictionaryOfAllObjectProperties<T>(this object objectToIterateThrough)
        {
            object propertyValue;
            PropertyInfo[] objectProperties;
            Dictionary<string, T> dictionaryOfObjectProperties = new Dictionary<string, T>();

            if (objectToIterateThrough == null)
                throw new NullReferenceException("Cannot iterate through properties of a null object.");

            objectProperties = objectToIterateThrough.GetType().GetProperties();

            objectProperties.ToList().ForEach(prop => {
                propertyValue = prop.GetValue(objectToIterateThrough);
                dictionaryOfObjectProperties.Add(prop.Name, (T)propertyValue);
            });

            return dictionaryOfObjectProperties;
        }
    }
}
