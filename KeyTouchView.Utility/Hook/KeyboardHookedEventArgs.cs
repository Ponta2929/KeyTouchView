using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KeyTouchView.Utility.Hook
{
    ///<summary>キーボードが操作されたときに実行されるメソッドを表すイベントハンドラ。</summary>
    public delegate void KeyboardHookedEventHandler(object sender, KeyboardHookedEventArgs e);

    ///<summary>KeyboardHookedイベントのデータを提供します。</summary>
    public class KeyboardHookedEventArgs : CancelEventArgs
    {
        ///<summary>新しいインスタンスを作成します。</summary>
        internal KeyboardHookedEventArgs(KeyboardMessage message, ref KeyboardState state)
        {
            this.message = message;
            this.state = state;
        }

        private KeyboardMessage message;
        private KeyboardState state;

        ///<summary>キーボードが押されたか放されたかを表す値を取得します。</summary>
        public KeyboardUpDown UpDown => (message == KeyboardMessage.KeyDown || message == KeyboardMessage.SystemKeyDown) ?
            KeyboardUpDown.Down : KeyboardUpDown.Up;

        ///<summary>操作されたキーの仮想キーコードを表す値を取得します。</summary>
        public Keys KeyCode => state.KeyCode;
        ///<summary>操作されたキーのスキャンコードを表す値を取得します。</summary>
        public int ScanCode => state.ScanCode;
        ///<summary>操作されたキーがテンキーなどの拡張キーかどうかを表す値を取得します。</summary>
        public bool IsExtendedKey => state.Flag.IsExtended;
        ///<summary>ALTキーが押されているかどうかを表す値を取得します。</summary>
        public bool AltDown => state.Flag.AltDown;
    }

    ///<summary>キーボードが押されているか放されているかを表します。</summary>
    public enum KeyboardUpDown
    {
        ///<summary>キーは放されている。</summary>
        Up,
        ///<summary>キーは押されている。</summary>
        Down,
    }

    ///<summary>メッセージコードを表します。</summary>
    public enum KeyboardMessage
    {
        ///<summary>キーが押された。</summary>
        KeyDown = 0x100,
        ///<summary>キーが放された。</summary>
        KeyUp = 0x101,
        ///<summary>システムキーが押された。</summary>
        SystemKeyDown = 0x104,
        ///<summary>システムキーが放された。</summary>
        SystemKeyUp = 0x105,
    }

    ///<summary>キーボードの状態を表します。</summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct KeyboardState
    {
        ///<summary>仮想キーコード。</summary>
        public Keys KeyCode;
        ///<summary>スキャンコード。</summary>
        public int ScanCode;
        ///<summary>各種特殊フラグ。</summary>
        public KeyboardStateFlag Flag;
        ///<summary>このメッセージが送られたときの時間。</summary>
        public int Time;
        ///<summary>メッセージに関連づけられた拡張情報。</summary>
        public IntPtr ExtraInfo;
    }

    ///<summary>キーボードの状態を補足します。</summary>
    public struct KeyboardStateFlag
    {
        private int flag;

        private bool IsFlagging(int value) => (flag & value) != 0;

        private void Flag(bool value, int digit) => flag = value ? (flag | digit) : (flag & ~digit);

        ///<summary>キーがテンキー上のキーのような拡張キーかどうかを表す。</summary>
        public bool IsExtended { get => IsFlagging(0x01); set => Flag(value, 0x01); }

        ///<summary>イベントがインジェクトされたかどうかを表す。</summary>
        public bool IsInjected { get => IsFlagging(0x10); set => Flag(value, 0x10); }

        ///<summary>ALTキーが押されているかどうかを表す。</summary>
        public bool AltDown { get => IsFlagging(0x20); set => Flag(value, 0x20); }

        ///<summary>キーが放されたどうかを表す。</summary>
        public bool IsUp { get => IsFlagging(0x80); set => Flag(value, 0x80); }
    }
}
