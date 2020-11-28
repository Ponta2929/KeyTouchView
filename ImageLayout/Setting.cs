using ImageLayout.Utility;
using KeyTouchView.Utility.IO;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace ImageLayout
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

        public event EventHandler SkinChanged;

        [XmlIgnore]
        /// <summary>
        /// キャプチャされるキー一覧
        /// </summary>
        public BindingList<KeyItem> CaptureKeys = new BindingList<KeyItem>();

        /// <summary>
        /// 読み込まれたスキン一覧
        /// </summary>
        [XmlIgnore]
        public BindingList<Skin> SkinFiles = new BindingList<Skin>();

        /// <summary>
        /// スキンの状態を保存
        /// </summary>
        public BindingList<SkinSetting> SkinSetting = new BindingList<SkinSetting>();

        public Skin Skin { get; set; }

        /// <summary>
        /// 横幅
        /// </summary>
        public int Width { get; set; } = 300;

        /// <summary>
        /// 高さ
        /// </summary>
        public int Height { get; set; } = 100;

        public void OnSkinChanged() =>
            SkinChanged?.Invoke(this, EventArgs.Empty);
    }
}
