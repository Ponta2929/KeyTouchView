using KeyTouchView.Utility;
using KeyTouchView.Utility.IO;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace StackLayout
{
    public class Setting : Serializer<Setting>
    {
        #region Singleton

        /// <summary>
        /// インスタンス
        /// </summary>
        private static Setting instance;

        /// <summary>
        /// インスタンスを取得します。
        /// </summary>
        /// <returns></returns>
        public static Setting GetInstance() => instance ?? (instance = new Setting());

        #endregion

        private static FontConverter fontConverter = new FontConverter();

        /// <summary>
        /// キャプチャされるキー一覧
        /// </summary>
        public BindingList<ReplaceKey> ReplaceKeys = new BindingList<ReplaceKey>();

        /// <summary>
        /// 描画フォント
        /// </summary>
        [XmlIgnore]
        public Font Font { get; set; } = new Font("MS UI Gothic", 9);


        /// <summary>
        /// 描画フォントの文字情報
        /// </summary>
        public string FontString { get => fontConverter.ConvertToString(Font); set => Font = (Font)fontConverter.ConvertFromString(value); }

        /// <summary>
        /// 標準色
        /// </summary>
        public ColorEx NormalColor { get; set; } = Color.Silver;

        /// <summary>
        /// キー押下色
        /// </summary>
        public ColorEx KeyDownColor { get; set; } = Color.Black;

        /// <summary>
        /// 背景色
        /// </summary>
        public ColorEx BackgroundColor { get; set; } = SystemColors.Control;

        /// <summary>
        /// 枠色
        /// </summary>
        public ColorEx BorderColor { get; set; } = Color.Black;

        /// <summary>
        /// 枠幅
        /// </summary>
        public int BorderSize { get; set; } = 1;

        /// <summary>
        /// アンチエイリアス
        /// </summary>
        public bool Antialiasing { get; set; } = true;

        /// <summary>
        /// 流れる文字方向
        /// </summary>
        public Direction Direction { get; set; } = Direction.Left;

        /// <summary>
        /// 横幅
        /// </summary>
        public int Width { get; set; }
    }
}
