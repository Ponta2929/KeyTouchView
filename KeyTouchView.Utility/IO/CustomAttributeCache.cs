using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KeyTouchView.Utility.IO
{
    /// <summary>
    /// カスタム属性のキャッシュを構築するクラスです。
    /// </summary>
    internal class CustomAttributeCache
    {
        private IDictionary<Tuple<string, string>, PropertyAsAttribute[]> dictionary = new Dictionary<Tuple<string, string>, PropertyAsAttribute[]>();

        /// <summary>
        /// カスタム属性のキャッシュを返します。
        /// </summary>
        public PropertyAsAttribute[] GetCache(Type type, PropertyInfo info)
        {
            var key = new Tuple<string, string>(type.Name, info.Name);

            if (!dictionary.ContainsKey(key))
            {
                var attr = (PropertyAsAttribute[])info.GetCustomAttributes(typeof(PropertyAsAttribute), false);

                if ((attr?.Count() ?? 0) <= 0)
                    dictionary.Add(key, null);
                else
                    dictionary.Add(key, attr);
            }

            return dictionary[key];
        }

        ~CustomAttributeCache() => dictionary.Clear();
    }
}
