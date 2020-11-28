using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;

namespace KeyTouchView.Utility.IO
{
    /// <summary>
    /// プロパティフィールドに属性を追加します。
    /// </summary>
    public class PropertyAsAttribute : Attribute
    {
        /// <summary>
        /// 配列のサイズです。
        /// </summary>
        public int SizeOf { get; set; }

        /// <summary>
        /// 配列のサイズをプロパティ値で指定します。
        /// </summary>
        public string SizeOfMember { get; set; }
    }
}