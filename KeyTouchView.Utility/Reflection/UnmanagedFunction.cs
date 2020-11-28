using KeyTouchView.Utility.Reflection.Win32API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace KeyTouchView.Utility.Reflection
{
    /// <summary>
    /// アンマネージド 関数の読み込みをサポートするクラスです。
    /// </summary>
    public class UnmanagedFunction : IDisposable
    {
        /// <summary>
        /// 格納された関数一覧
        /// </summary>
        public Dictionary<string, Delegate> Functions { get; private set; }

        /// <summary>
        /// リソースが破棄されているか
        /// </summary>
        public bool Disposed { get; private set; }

        /// <summary>
        /// DLLのハンドル
        /// </summary>
        public IntPtr Handle { get; private set; }

        /// <summary>
        /// 64bitモード
        /// </summary>
        private bool x64;

        private UnmanagedFunction()
        {
            if (IntPtr.Size == 8)
                this.x64 = true;
        }

        /// <summary>
        /// アンマネージド DLLを読み込みます。
        /// </summary>
        /// <param name="fileName">読み込むDLL</param>
        public UnmanagedFunction(string fileName)
        {
            if (File.Exists(fileName))
                throw new FileNotFoundException("ファイルが存在しませんでした。", fileName);

            // DLLを読み込みます。
            this.Handle = API.LoadLibrary(fileName);

            if (this.Handle == IntPtr.Zero)
                throw new NullReferenceException("指定されたモジュールを読み込めませんでした。");

            this.Functions = new Dictionary<string, Delegate>();
        }

        /// <summary>
        /// 指定された関数名を関数一覧から選択します。
        /// </summary>
        /// <typeparam name="T">実行させる関数情報</typeparam>
        public T Select<T>() where T : class =>
            Select<T>(typeof(T).Name);

        /// <summary>
        /// 指定された関数名を関数一覧から選択します。
        /// </summary>
        /// <typeparam name="T">実行させる関数情報</typeparam>
        /// <param name="funcName">関数名</param>
        public T Select<T>(string funcName) where T : class =>
            this.Functions[funcName] as T;

        /// <summary>
        /// アンマネージド関数を生成し、関数一覧に追加します。
        /// </summary>
        /// <typeparam name="T">キャストするデリゲート</typeparam>
        public void AddFunction(Type type) =>
            this.AddFunction(type, type.Name);

        /// <summary>
        /// アンマネージド関数を生成し、関数一覧に追加します。
        /// </summary>
        /// <typeparam name="T">キャストするデリゲート</typeparam>
        public void AddFunction<T>() where T : class =>
            this.AddFunction<T>(typeof(T).Name);

        /// <summary>
        /// アンマネージド関数を生成し、関数一覧に追加します。
        /// </summary>
        /// <typeparam name="T">キャストするデリゲート</typeparam>
        /// <param name="funcName">関数名</param>
        public void AddFunction<T>(string funcName) =>
            this.AddFunction(typeof(T), funcName);

        /// <summary>
        /// アンマネージド関数を生成し、関数一覧に追加します。
        /// </summary>
        /// <typeparam name="T">キャストするデリゲート</typeparam>
        /// <param name="funcName">関数名</param>
        public void AddFunction(Type type, string funcName) 
        {
            if (this.Disposed)
                throw new ObjectDisposedException(this.ToString(), "このクラスは既に破棄されています。");

            var func = API.GetProcAddress(this.Handle, funcName);

            if (func == IntPtr.Zero)
                throw new InvalidCastException("関数名は有効ではありませんでした。");

            var info = type.GetMethod("Invoke");
            var param = info.GetParameters().Select(v => v.ParameterType).ToArray();

            // 動的メソッド生成
            var method = new DynamicMethod($"{funcName}_Emit", info.ReturnType, param, typeof(UnmanagedFunction));

            // ILコード生成
            var il = method.GetILGenerator();

            // 関数に送られるすべての引数を積む 
            for (var i = 0; i < param.Length; i++)
                il.Emit(OpCodes.Ldarg, i);

            // 関数ポインタ書き込み
            if (this.x64)
                il.Emit(OpCodes.Ldc_I8, func.ToInt64());
            else
                il.Emit(OpCodes.Ldc_I4, func.ToInt32());
            il.EmitCalli(OpCodes.Calli, CallingConvention.StdCall, info.ReturnType, param);
            il.Emit(OpCodes.Ret);

            // 関数を追加します。
            this.Functions.Add(funcName, method.CreateDelegate(type));
        }

        /// <summary>
        /// アンマネージド関数を生成します。
        /// </summary>
        /// <typeparam name="T">キャストするデリゲート</typeparam>
        public T CreateFunction<T>() where T : class =>
            this.CreateFunction<T>(typeof(T).Name);

        /// <summary>
        /// アンマネージド関数を生成します。
        /// </summary>
        /// <typeparam name="T">キャストするデリゲート</typeparam>
        public Delegate CreateFunction(Type type) =>
            this.CreateFunction(type, type.Name);

        /// <summary>
        /// アンマネージド関数を生成します。
        /// </summary>
        /// <typeparam name="T">キャストするデリゲート</typeparam>
        /// <param name="funcName">関数名</param>
        public T CreateFunction<T>(string funcName) where T : class =>
            this.CreateFunction(typeof(T), funcName) as T;

        /// <summary>
        /// アンマネージド関数を生成します。
        /// </summary>
        /// <typeparam name="T">キャストするデリゲート</typeparam>
        /// <param name="funcName">関数名</param>
        public Delegate CreateFunction(Type type, string funcName)
        {
            if (this.Disposed)
                throw new ObjectDisposedException(this.ToString(), "このクラスは既に破棄されています。");

            var func = API.GetProcAddress(this.Handle, funcName);

            if (func == IntPtr.Zero)
                throw new InvalidCastException("関数名は有効ではありませんでした。");

            var info = type.GetMethod("Invoke");
            var param = info.GetParameters().Select(v => v.ParameterType).ToArray();

            // 動的メソッド生成
            var method = new DynamicMethod($"{funcName}_Emit", info.ReturnType, param, typeof(UnmanagedFunction));

            // ILコード生成
            var il = method.GetILGenerator();

            // 関数に送られるすべての引数を積む 
            for (var i = 0; i < param.Length; i++)
                il.Emit(OpCodes.Ldarg, i);

            // 関数ポインタ書き込み
            if (this.x64)
                il.Emit(OpCodes.Ldc_I8, func.ToInt64());
            else
                il.Emit(OpCodes.Ldc_I4, func.ToInt32());
            il.EmitCalli(OpCodes.Calli, CallingConvention.StdCall, info.ReturnType, param);
            il.Emit(OpCodes.Ret);

            // 関数を追加します。
            return method.CreateDelegate(type);
        }

        /// <summary>
        /// protected : dispose
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                if (disposing)
                    if (this.Handle != IntPtr.Zero)
                        API.FreeLibrary(this.Handle); // 使用したDLLを破棄します。
              
                this.Disposed = true;
            }
        }

        /// <summary>
        /// 使用されたリソースを破棄します。
        /// </summary>
        public void Dispose()
        {
            // 使用したDLLを破棄します。
            this.Dispose(true);

            // GCを無効にします。
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// デストラクタ
        /// </summary>
        ~UnmanagedFunction() => this.Dispose(false);
    }
}