using System;
using System.IO;
using System.Windows.Forms;

namespace ImageLayout
{
    public partial class SettingForm : Form
    {
        #region グローバル変数

        private Setting setting = Setting.GetInstance();
        private ToolTip toolTip = new ToolTip();

        #endregion

        public SettingForm()
        {
            InitializeComponent();

            // 初期化
            Initialize();
        }

        /// <summary>
        /// 初期化を行います。
        /// </summary>
        private void Initialize()
        {
            // スキンリストをデータソースに設定
            ComboBox_SkinList.DataSource = setting.SkinFiles;
            ComboBox_SkinList.FormattingEnabled = true;

            if (setting.Skin != null)
            {
                ComboBox_SkinList.SelectedItem = setting.Skin;
            }
        }

        private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }

            Visible = false;
        }


        private void Apply_Click(object sender, EventArgs e)
        {
            // スキン適用
            if (ComboBox_SkinList.SelectedIndex != -1)
            {
                setting.Skin = (Skin)ComboBox_SkinList.SelectedItem;
            }

            // 更新   
            setting.OnSkinChanged();
        }

        private void ComboBox_SkinList_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.DesiredType == typeof(string))
            {
                var key = (Skin)e.Value;

                // フォーマット
                e.Value = Path.GetFileName(key.SkinInfo.SkinName);
            }
        }
    }
}
