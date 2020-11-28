using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageLayout
{
    /// <summary>
    /// スキンのサイズを保存
    /// </summary>
    public class SkinSetting
    {
        /// <summary>
        /// 対象のスキンファイル
        /// </summary>
        public string SkinFile { get; set; }

        /// <summary>
        /// スキンの横幅
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// スキンの高さ
        /// </summary>
        public int Height { get; set; }
    }
}
