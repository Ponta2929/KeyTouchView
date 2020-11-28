using System;
using System.Runtime.InteropServices;

namespace KeyTouchView.Utility.Reflection.Win32API
{
    public static partial class API
    {
        /// <summary>指定された実行可能モジュールを、呼び出し側プロセスのアドレス空間内にマップします。</summary>
        /// <param name="lpLibFileName">モジュールのファイル名</param>
        /// <returns>関数が成功すると、モジュールのハンドルが返ります。</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr LoadLibrary(string lpLibFileName);

        /// <summary>
        /// ロード済みのダイナミックリンクライブラリ（DLL）モジュールの参照カウントを 1 つ減らします。
        /// 参照カウントが 0 になると、モジュールは呼び出し側プロセスのアドレス空間からマップ解除され、そのモジュールのハンドルは無効になります。
        /// </summary>
        /// <param name="hLibModule">DLL モジュールのハンドル</param>
        /// <returns>関数が成功すると、True の値が返ります。</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr hLibModule);

        /// <summary>
        /// ダイナミックリンクライブラリ（DLL）が持つ、指定されたエクスポート済み関数のアドレスを取得します。
        /// </summary>
        /// <param name="hModule">DLL モジュールのハンドル</param>
        /// <param name="lpProcName">関数名</param>
        /// <returns>関数が成功すると、DLL のエクスポート済み関数のアドレスが返ります。</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        /// <summary>
        /// ダイナミックリンクライブラリ（DLL）が持つ、指定されたエクスポート済み関数を指定されたデリゲートに変換して取得します。
        /// </summary>
        /// <param name="methodType">変換に使用するデリゲート。関数名の取得にはデリゲート名が使用されます( typeof(delegate) )</param>
        /// <param name="handle">DLL モジュールのハンドル</param>
        /// <returns>関数が成功すると、DLL のエクスポート済み関数のデリゲートが返ります。</returns>
        public static Delegate GetProcAddress(Type methodType, IntPtr handle)
        {
            return GetProcAddress(methodType, handle, methodType.Name);
        }

        /// <summary>
        /// ダイナミックリンクライブラリ（DLL）が持つ、指定されたエクスポート済み関数を指定されたデリゲートに変換して取得します。
        /// </summary>
        /// <param name="methodType">変換に使用するデリゲート( typeof(delegate) )</param>
        /// <param name="handle">DLL モジュールのハンドル</param>
        /// <param name="methodName">関数名</param>
        /// <returns>関数が成功すると、DLL のエクスポート済み関数のデリゲートが返ります。</returns>
        public static Delegate GetProcAddress(Type methodType, IntPtr handle, string methodName)
        {
            // アンマネージド関数の呼び出し
            var ptr = GetProcAddress(handle, methodName);

            if (ptr != IntPtr.Zero)
                return Marshal.GetDelegateForFunctionPointer(ptr, methodType);

            return null;
        }

        /// <summary>
        /// ダイナミックリンクライブラリ（DLL）が持つ、指定されたエクスポート済み関数を指定されたデリゲートに変換して取得します。
        /// </summary>
        /// <typeparam name="T">変換に使用するデリゲート。関数名の取得にはデリゲート名が使用されます</typeparam>
        /// <param name="handle">DLL モジュールのハンドル</param>
        /// <returns>関数が成功すると、DLL のエクスポート済み関数のデリゲートが返ります。</returns>
        public static T GetProcAddress<T>(IntPtr handle) where T : class
        {
            return GetProcAddress<T>(handle, typeof(T).Name);
        }

        /// <summary>
        /// ダイナミックリンクライブラリ（DLL）が持つ、指定されたエクスポート済み関数を指定されたデリゲートに変換して取得します。
        /// </summary>
        /// <typeparam name="T">変換に使用するデリゲート</typeparam>
        /// <param name="handle">DLL モジュールのハンドル</param>
        /// <param name="methodName">関数名</param>
        /// <returns>関数が成功すると、DLL のエクスポート済み関数のデリゲートが返ります。</returns>
        public static T GetProcAddress<T>(IntPtr handle, string methodName) where T : class
        {
            // アンマネージド関数の呼び出し
            var ptr = GetProcAddress(handle, methodName);

            if (ptr != IntPtr.Zero)
                return Marshal.GetDelegateForFunctionPointer(ptr, typeof(T)) as T;

            return null;
        }
    }
}
