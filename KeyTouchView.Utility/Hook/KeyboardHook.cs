using System;
using System.Runtime.InteropServices;

namespace KeyTouchView.Utility.Hook
{
    public class KeyboardHook : GlobalHook
    {
        #region Singleton

        /// <summary>
        /// インスタンス
        /// </summary>
        private static KeyboardHook instance;

        /// <summary>
        /// インスタンスを取得します。
        /// </summary>
        /// <returns></returns>
        public static KeyboardHook GetInstance() => instance ?? (instance = new KeyboardHook());

        #endregion

        private delegate int CallNextHookEx(IntPtr hook, int code, KeyboardMessage message, ref KeyboardState state);
        private delegate int KeyboardHookDelegate(int code, KeyboardMessage message, ref KeyboardState state);

        /// <summary>
        /// キーボードフック
        /// </summary>
        private const int KeyboardHookType = 13;

        /// <summary>
        /// キーボードフックイベント
        /// </summary>
        public event KeyboardHookedEventHandler KeyboardHooked;

        public KeyboardHook() : base() =>
            this.Initialize(KeyboardHookType, new KeyboardHookDelegate(this.CallNextHook), typeof(CallNextHookEx));
        
        /// <summary>
        /// フックチェーンを次のフックプロシージャに渡します。
        /// </summary>
        private int CallNextHook(int code, KeyboardMessage message, ref KeyboardState state)
        {
            if (code >= 0)
            {
                var e = new KeyboardHookedEventArgs(message, ref state);

                this.OnKeyboardHooked(e);

                if (e.Cancel)
                    return -1;
            }

            return ((CallNextHookEx)base.callNextHook)(IntPtr.Zero, code, message, ref state);
        }

        ///<summary>
        ///KeyboardHookedイベントを発生させる。
        ///</summary>
        ///<param name="e">イベントのデータ。</param>
        protected virtual void OnKeyboardHooked(KeyboardHookedEventArgs e) =>
            this.KeyboardHooked?.Invoke(this, e);
    }
}
