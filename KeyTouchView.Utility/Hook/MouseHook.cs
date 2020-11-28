using System;
using System.Runtime.InteropServices;

namespace KeyTouchView.Utility.Hook
{
    public class MouseHook : GlobalHook
    {
        #region Singleton

        /// <summary>
        /// インスタンス
        /// </summary>
        private static MouseHook instance;

        /// <summary>
        /// インスタンスを取得します。
        /// </summary>
        /// <returns></returns>
        public static MouseHook GetInstance() => instance ?? (instance = new MouseHook());

        #endregion

        private delegate int CallNextHookEx(IntPtr hook, int code, MouseMessage message, ref MouseState state);
        private delegate int MouseHookDelegate(int code, MouseMessage message, ref MouseState state);

        /// <summary>
        /// マウスフック
        /// </summary>
        private const int MouseHookType = 14;

        /// <summary>
        /// マウスフックイベント
        /// </summary>
        public event MouseHookedEventHandler MouseHooked;

        public MouseHook() : base() =>
            this.Initialize(MouseHookType, new MouseHookDelegate(this.CallNextHook), typeof(CallNextHookEx));

        /// <summary>
        /// フックチェーンを次のフックプロシージャに渡します。
        /// </summary>
        private int CallNextHook(int code, MouseMessage message, ref MouseState state)
        {
            if (code >= 0)
            {
                var e = new MouseHookedEventArgs(message, ref state);

                this.OnMouseHooked(e);

                if (e.Cancel)
                    return -1;
            }

            return ((CallNextHookEx)base.callNextHook)(IntPtr.Zero, code, message, ref state);
        }

        ///<summary>
        ///KeyboardHookedイベントを発生させる。
        ///</summary>
        ///<param name="e">イベントのデータ。</param>
        protected virtual void OnMouseHooked(MouseHookedEventArgs e) =>
            this.MouseHooked?.Invoke(this, e);
    }
}
