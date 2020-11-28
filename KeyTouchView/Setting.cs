using KeyTouchView.Utility;
using KeyTouchView.Utility.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace KeyTouchView
{
    public class Setting : Serializer<Setting>
    {
        #region Singleton

        /// <summary>
        /// インスタンス
        /// </summary>
        private static Setting instance;

        /// <summary>
        /// インスタンスを取得します。
        /// </summary>
        /// <returns></returns>
        public static Setting GetInstance() => instance ?? (instance = new Setting());

        #endregion

        /// <summary>
        /// 表示モード
        /// </summary>
        public string LayoutName { get; set; }

        /// <summary>
        /// フォーム透明度
        /// </summary>
        public int Opacity { get; set; } = 100;

        /// <summary>
        /// フォーム位置
        /// </summary>
        public Point Position { get; set; } = new Point(200, 250);

        /// <summary>
        /// フォーム位置
        /// </summary>
        public Size ClientSize { get; set; } = new Size(200, 50);
    }
}
