using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace KeyTouchView.Utility.Hook
{
    ///<summary>マウスが操作されたときに実行されるメソッドを表すイベントハンドラ。</summary>
    public delegate void MouseHookedEventHandler(object sender, MouseHookedEventArgs e);

    public class MouseHookedEventArgs : CancelEventArgs
    {
        ///<summary>新しいインスタンスを作成する。</summary>
        internal MouseHookedEventArgs(MouseMessage message, ref MouseState state)
        {
            this.message = message;
            this.state = state;
        }

        private MouseMessage message;
        private MouseState state;

        ///<summary>マウス座標を取得します。</summary>
        public Point Point => state.Point;

        ///<summary>マウスの状態を取得します。</summary>
        public MouseMessage State => message;

        /// <summary>
        /// ホイール移動量を取得します。
        /// </summary>
        public int WheelDelta =>
            message == MouseMessage.MouseWheel | message == MouseMessage.MouseHorizontalWheel ? state.MouseData >> 16 : 0;

        /// <summary>
        /// Xボタン番号を取得します。
        /// </summary>
        public int XButton =>
            message == MouseMessage.XButtonDoubleClick | message == MouseMessage.XButtonDown | message == MouseMessage.XButtonUp |
            message == MouseMessage.NoClientXButtonDoubleClick | message == MouseMessage.NoClientXButtonDown | message == MouseMessage.NoClientXButtonUp ?
            state.MouseData >> 16 : 0;

    }

    ///<summary>
    ///メッセージコードを表す。
    ///</summary>
    public enum MouseMessage
    {
        MouseActive = 0x0021,
        NoClientHitTest = 0x0084,
        NoClientMouseMove = 0x00A0,
        NoClientLeftButtonDown = 0x00A1,
        NoClientLeftButtonUp = 0x00A2,
        NoClientLeftButtonDoubleClick = 0x00A3,
        NoClientRightButtonDown = 0x00A4,
        NoClientRightButtonUp = 0x00A5,
        NoClientRightButtonDoubleClick = 0x00A6,
        NoClientMiddleButtonDown = 0x00A7,
        NoClientMiddleButtonUp = 0x00A8,
        NoClientMiddleButtonDoubleClick = 0x00A9,
        NoClientXButtonDown = 0x00AB,
        NoClientXButtonUp = 0x00AC,
        NoClientXButtonDoubleClick = 0x00AD,
        MouseMove = 0x0200,
        LeftButtonDown = 0x0201,
        LeftButtonUp = 0x0202,
        LeftButtonDoubleClick = 0x0203,
        RightButtonDown = 0x0204,
        RightButtonUp = 0x0205,
        RightButtonDoubleClick = 0x0206,
        MiddleButtonDown = 0x0207,
        MiddleButtonUp = 0x0208,
        MiddleButtonDoubleClick = 0x0209,
        MouseWheel = 0x020A,
        XButtonDown = 0x020B,
        XButtonUp = 0x020C,
        XButtonDoubleClick = 0x020D,
        MouseHorizontalWheel = 0x020E,
        CaptureChanged = 0x0215,
        NoClientMouseHover = 0x02A0,
        MouseHover = 0x02A1,
        NoClientMouseLeave = 0x02A2,
        MouseLeave = 0x02A3,
    }

    /// <summary>
    /// マウスの状態を表す。
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MouseState
    {
        /// <summary>
        /// マウス座標
        /// </summary>
        public Point Point;
        /// <summary>
        /// マウスデータ
        /// </summary>
        public int MouseData;
        /// <summary>
        /// マウスフラグ
        /// </summary>
        public int Flags;
        /// <summary>
        /// イベント発生時間
        /// </summary>        
        public int Time;
        /// <summary>
        /// 予約
        /// </summary>
        public IntPtr ExtraInfo;
    }
}
