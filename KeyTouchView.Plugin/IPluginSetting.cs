using System.Windows.Forms;

namespace KeyTouchView.Plugin
{
    /// <summary>
    /// 設定ダイアログがある、設定データを保存する場合実装してください。
    /// </summary>
    public interface IPluginSetting : IPluginLayout
    {
        /// <summary>
        /// 初期化前に呼び出されます。
        /// </summary>
        /// <param name="path">アプリケーション プラグインフォルダへのパスが渡されます。</param>
        void LoadFile(string path);

        /// <summary>
        /// プラグイン破棄前に呼び出されます。
        /// </summary>
        void SaveFile();

        /// <summary>
        /// 設定フォームが存在する。
        /// </summary>
        bool HasSettingForm { get; }

        /// <summary>
        /// 設定フォームを呼び出します。
        /// </summary>
        /// <param name="owner">呼び出し元のフォームです。</param>
        void ShowSettingForm(Form owner);
    }
}
