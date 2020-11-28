using KeyTouchView.Plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KeyTouchView
{
    public class PluginLoader<T> where T : IPluginBase
    {

        /// <summary>
        /// 読み込まれたプラグイン
        /// </summary>
        public T[] Plugin { get; private set; }

        /// <summary>
        /// 読み込み済み
        /// </summary>
        public bool Loaded { get; private set; }

        /// <summary>
        /// 指定したパスから*.dllを読み込みます。
        /// </summary>
        /// <param name="path">読み込み先のパス</param>
        public void Load(string path)
        {
            if (this.Loaded)
                throw new Exception();

            if (!Directory.Exists(path))
                throw new Exception();

            var items = new List<T>();

            var interfaceName = typeof(T).FullName;

            var plugins = Directory.GetFiles(path, "*.dll");

            foreach (var plugin in plugins)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(plugin);

                    foreach (var type in assembly.GetTypes())
                    {
                        if (type.IsClass && type.IsPublic && !type.IsAbstract && type.GetInterface(interfaceName) != null)
                        {
                            items.Add((T)assembly.CreateInstance(type.FullName));
                        }

                    }
                }
                catch
                { }
            }

            this.Plugin = items.ToArray();

            this.Loaded = true;
        }

        /// <summary>
        /// 読み込まれたプラグインを破棄します。
        /// </summary>
        public void Clear()
        {
            foreach (var plugins in this.Plugin)
            {
                plugins.Dispose();
            }

            this.Plugin = null;

            this.Loaded = false;
        }
    }
}
