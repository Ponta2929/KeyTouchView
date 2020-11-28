using KeyTouchView.Utility.Hook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageLayout
{
    /// <summary>
    /// キーの状態
    /// </summary>
    public class KeyInfo
    {
        /// <summary>
        /// 対象のキー
        /// </summary>
        public Keys Key { get; set; }

        /// <summary>
        /// キーの状態
        /// </summary>
        public KeyboardUpDown UpDown { get; set; } = KeyboardUpDown.Up;
     }
}
