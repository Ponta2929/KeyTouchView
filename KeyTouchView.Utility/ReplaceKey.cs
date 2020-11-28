using System;
using System.Windows.Forms;

namespace KeyTouchView.Utility
{
    /// <summary>
    /// 置換キー
    /// </summary>
    [Serializable]
    public class ReplaceKey
    {
        /// <summary>
        /// 対象のキー
        /// </summary>
        public Keys Key { get; set; }

        /// <summary>
        /// 置換後のキー名
        /// </summary>
        public string Replace { get; set; }
    }
}
