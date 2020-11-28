using ImageLayout.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageLayout
{
    [Serializable]
    public class Skin
    {
        /// <summary>
        /// スキンのファイル名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// スキンの設定情報
        /// </summary>
        public SkinInfomation SkinInfo { get; set; }
    }
}
