using System;
using System.Drawing;
using System.Windows.Forms;

namespace KeyTouchView.Utility
{
    public static class Utility
    {
        private static object syncObject = new object();
        private static Bitmap surface = new Bitmap(1, 1);
        private static Graphics graphics = Graphics.FromImage(surface);

        public static string GetReplacedName(CaptureKey captureKey, ReplaceKey[] replaceKeys) =>
            GetReplacedName(captureKey.Key, replaceKeys);

        public static string GetReplacedName(Keys key, ReplaceKey[] replaceKeys) =>
            Array.Find<ReplaceKey>(replaceKeys, item => item.Key == key)?.Replace ?? Enum.GetName(typeof(Keys), key);

        /// <summary>
        /// 描画する文字の大きさを取得します。
        /// </summary>
        /// <param name="text">描画するテキスト</param>
        /// <param name="font">描画に使用するフォント</param>
        /// <returns>System.Drawing.SizeF</returns>
        public static SizeF MeasureString(string text, Font font)
        {
            lock (syncObject)
            {
                return graphics.MeasureString(text, font, int.MaxValue);
            }
        }

    }
}
