using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageLayout.Utility
{
    [Serializable]
    /// <summary>
    /// キャプチャする項目
    /// </summary>
    public class KeyItem
    {
        /// <summary>
        /// キー
        /// </summary>
        public Keys Key { get; set; }

        /// <summary>
        /// X座標
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y座標
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// 幅
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 高さ
        /// </summary>
        public int Height { get; set; }
    }
}
