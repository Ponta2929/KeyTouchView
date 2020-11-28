namespace KeyTouchView.Plugin
{
    ///<summary>サイズが変更されたことを通知するイベントハンドラ。</summary>    
    public delegate void SizeChangedEventHandler(object sender, SizeChangedEventArgs e);

    public class SizeChangedEventArgs
    {
        ///<summary>新しいインスタンスを作成します。</summary>
        public SizeChangedEventArgs(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// 変更後の横幅
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// 変更後の高さ
        /// </summary>
        public int Height { get; private set; }
    }
}
