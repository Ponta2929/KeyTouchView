using System;

namespace ImageLayout.Utility
{
    [Serializable]
    public class SkinInfomation
    {
        /// <summary>
        /// スキン名
        /// </summary>
        public string SkinName { get; set; }

        /// <summary>
        /// 押す前の画像
        /// </summary>
        public string NormalImage { get; set; }

        /// <summary>
        /// 押した後の画像
        /// </summary>
        public string PressImage { get; set; }

        /// <summary>
        /// キー情報
        /// </summary>
        public KeyItem[] Keys { get; set; }
    }
}