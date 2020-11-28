using KeyTouchView.Utility.Hook;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace KeyTouchView.Utility
{
    /// <summary>
    /// キャプチャされたキー情報
    /// </summary>
    [Serializable]
    public class CaptureKey
    {
        /// <summary>
        /// 対象のキー
        /// </summary>
        public Keys Key { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public PointF Point { get; set; }

        /// <summary>
        /// サイズ
        /// </summary>
        public SizeF Size { get; set; }
        
        /// <summary>
        /// キーの状態
        /// </summary>
        [XmlIgnore]
        public KeyboardUpDown UpDown { get; set; }

        /// <summary>
        /// 描画用色
        /// </summary>
        [XmlIgnore]
        public ColorEx Color { get; set; }

        /// <summary>
        /// 描画用テキスト
        /// </summary>
        [XmlIgnore]
        public string String { get; set; }
    }
}
