using KeyTouchView.Utility.Reflection;
using KeyTouchView.Utility.Reflection.Win32API;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyTouchView.Utility.Hook
{
    public class GlobalHook : ApplicationContext
    {
        /// <summary>
        /// リソースが破棄されているか
        /// </summary>
        protected bool Disposed { get; private set; }

        /// <summary>
        /// Hookハンドル
        /// </summary>
        public IntPtr Handle { get; private set; }

        /// <summary>
        /// インスタンスハンドル
        /// </summary>
        public IntPtr Module { get; private set; }

        /// <summary>
        /// 関数生成
        /// </summary>
        private UnmanagedFunction emitter;

        /// <summary>
        /// GCによる回収保護
        /// </summary>
        private GCHandle @delegate;

        /// <summary>
        /// Unmanaged : 
        /// フックチェーンを次のフックプロシージャに渡します。
        /// </summary>
        protected Delegate callNextHook;

        public GlobalHook()
        {
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                throw new PlatformNotSupportedException("Windows 98/Meではサポートされていません。");
        }

        /// <summary>
        /// 初期化を行います。
        /// </summary>
        /// <param name="hookType">インストールするフック手順のタイプ。</param>
        protected virtual void Initialize(int hookType, Delegate callback, Type callNextHook)
        {
            // エミッター生成
            this.emitter = new UnmanagedFunction("user32.dll");

            // Hook
            this.callNextHook = this.emitter.CreateFunction(callNextHook);

            // コールバック関数
            this.@delegate = GCHandle.Alloc(callback);

            // モジュールポインタ
            this.Module = IntPtr.Zero;
            this.Handle = API.SetWindowsHookEx(hookType, Marshal.GetFunctionPointerForDelegate(callback), this.Module, 0);
        }

        /// <summary>
        /// protected : dispose
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                if (disposing)
                {
                    // 使用したDLLを破棄します。
                    if (this.Handle != IntPtr.Zero)
                    {
                        API.UnhookWindowsHookEx(this.Handle);
                        this.Handle = IntPtr.Zero;
                    }

                    if (@delegate.IsAllocated)
                        this.@delegate.Free();

                    this.emitter.Dispose();
                }

                this.Disposed = true;
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// 使用されたリソースを破棄します。
        /// </summary>
        public new void Dispose()
        {
            // 使用したDLLを破棄します。
            this.Dispose(true);

            // GCを無効にします。
            GC.SuppressFinalize(this);
        }

        ~GlobalHook() => this.Dispose(false);
    }
}
