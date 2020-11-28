using System.Windows.Forms;

namespace KeyTouchView.Plugin
{
    /// <summary>
    /// 描画領域でのマウスイベントを通知します。
    /// </summary>
    public interface IPluginMouseEvent : IPluginLayout
    {
        /// <summary>
        /// マウス ポインターが描画領域上を移動すると発生します。
        /// </summary>
        /// <param name="e"></param>
        void MouseMove(MouseEventArgs e);

        /// <summary>
        /// マウス ポインターが描画領域上にあり、マウス ボタンが離されると発生します。
        /// </summary>
        /// <param name="e"></param>

        void MouseUp(MouseEventArgs e);

        /// <summary>
        /// マウス ポインターが描画領域上にあり、マウス ボタンがクリックされると発生します。
        /// </summary>
        /// <param name="e"></param>

        void MouseDown(MouseEventArgs e);
    }
}
