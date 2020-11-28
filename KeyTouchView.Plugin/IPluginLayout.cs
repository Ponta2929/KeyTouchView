using KeyTouchView.Utility.Hook;
using System.Drawing;

namespace KeyTouchView.Plugin
{
    /// <summary>
    /// レイアウトを実装します。
    /// </summary>
    public interface IPluginLayout : IPluginBase
    {
        /// <summary>
        /// サイズの変更を通知するイベントハンドラ
        /// </summary>
        event SizeChangedEventHandler SizeChanged;

        /// <summary>
        /// レイアウト名。
        /// </summary>
        string LayoutName { get; }

        /// <summary>
        /// レイアウトの横幅。
        /// </summary>
        int Width { get; }

        /// <summary>
        /// レイアウトの高さ。
        /// </summary>
        int Height { get; }

        /// <summary>
        /// 初期化処理
        /// </summary>
        void Initialize();

        /// <summary>
        /// 計算処理を実行します。
        /// UI スレッドとは別に実行されます。
        /// </summary>
        void Calc();

        /// <summary>
        /// 描画処理を実行します。
        /// UI スレッドで実行されます。
        /// </summary>
        /// <param name="g">描画用 System.Drawing.Graphics</param>
        void Draw(Graphics g);

        /// <summary>
        /// キーボードが操作されると発生します。
        /// UIスレッドとは別に実行されます。
        /// </summary>
        /// <param name="e">KeyboardHookedイベントデータ</param>
        void KeyboardHooked(KeyboardHookedEventArgs e);

        /// <summary>
        /// マウスフックが操作されると発生します。
        /// UIスレッドとは別に実行されます。
        /// </summary>
        /// <param name="e">MouseHookedイベントデータ</param>
        void MouseHooked(MouseHookedEventArgs e);

        /// <summary>
        /// ウィンドウがリサイズされると呼び出されます。
        /// </summary>
        /// <param name="size">リサイズ後の System.Drawing.Size</param>
        void WindowResize(Size size);
    }
}
