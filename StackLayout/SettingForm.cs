using KeyTouchView.Utility;
using KeyTouchView.Utility.Hook;
using KeyTouchView.Utility.Reflection.Win32API;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace StackLayout
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
            // データソースの設定
            ListBox_ReplaceList.DataSource = setting.ReplaceKeys;
            ListBox_ReplaceList.FormattingEnabled = true;

            // キーリストを作成
            var keys = (Keys[])Enum.GetValues(typeof(Keys));

            // キー
            var key = Keys.None;

            for (var i = 0; i < keys.Length; i++)
            {
                if (key != keys[i])
                {
                    ComboBox_KeyList.Items.Add(keys[i]);
                }

                // 値を設定
                key = keys[i];
            }

            // その他
            SetSettingData();

            // select
            ComboBox_KeyList.SelectedIndex = 0;

            if (setting.Direction == Direction.Left)
            {
                ComboBox_StackDirection.SelectedIndex = 0;
            }
            else
            {
                ComboBox_StackDirection.SelectedIndex = 1;
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

        public void KeyboardHooked(object sender, KeyboardHookedEventArgs e)
        {
            if (Created)
            {
                Invoke((MethodInvoker)(() =>
                {
                    if (e.UpDown == KeyboardUpDown.Down)
                    {
                        var active = API.GetForegroundWindow() == Handle;

                        if (active && TabControl_Main.SelectedTab == TabPage_Replace && !TextBox_Replace.Focused)
                        {
                            e.Cancel = true;

                            // 項目選択
                            ComboBox_KeyList.SelectedItem = e.KeyCode;
                        }
                    }
                }));
            }
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            if (ListBox_ReplaceList.SelectedIndex != -1)
            {
                setting.ReplaceKeys.RemoveAt(ListBox_ReplaceList.SelectedIndex);
            }

            // 更新   
            setting.ReplaceKeys.ResetBindings();
        }

        private void Font_Click(object sender, EventArgs e)
        {
            using (FontDialog fd = new FontDialog())
            {
                fd.Font = setting.Font;

                // ダイアログ表示
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    setting.Font = fd.Font;
                }

                Label_FontView.Font = new Font(setting.Font.FontFamily, 9); ;
                Label_FontView.Text = $"{setting.Font.FontFamily.Name}, {setting.Font.Size}pt";
            }

            // 更新   
            setting.ReplaceKeys.ResetBindings();
        }

        private void Normal_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog())
            {
                cd.Color = setting.NormalColor;

                // ダイアログ表示
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    setting.NormalColor = cd.Color;
                }

                Panel_Normal.BackColor = setting.NormalColor;
                SetToolTipColor(Panel_Normal, setting.NormalColor);
            }

            // 更新   
            setting.ReplaceKeys.ResetBindings();
        }

        private void KeyDown_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog())
            {
                cd.Color = setting.KeyDownColor;

                // ダイアログ表示
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    setting.KeyDownColor = cd.Color;
                }

                Panel_KeyDown.BackColor = setting.KeyDownColor;
                SetToolTipColor(Panel_KeyDown, setting.KeyDownColor);
            }

            // 更新     
            setting.ReplaceKeys.ResetBindings();
        }

        private void Background_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog())
            {
                cd.Color = setting.BackgroundColor;

                // ダイアログ表示
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    setting.BackgroundColor = cd.Color;
                }

                Panel_Background.BackColor = setting.BackgroundColor;
                SetToolTipColor(Panel_Background, setting.BackgroundColor);
            }

            // 更新   
            setting.ReplaceKeys.ResetBindings();
        }

        private void Border_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog())
            {
                cd.Color = setting.BorderColor;

                // ダイアログ表示{
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    setting.BorderColor = cd.Color;
                }

                Panel_Border.BackColor = setting.BorderColor;
                SetToolTipColor(Panel_Border, setting.BorderColor);
            }

            // 更新   
            setting.ReplaceKeys.ResetBindings();
        }

        private void BorderSize_ValueChanged(object sender, EventArgs e)
        {
            setting.BorderSize = (int)NumericUpDown_BorderSize.Value;

            // 更新   
            setting.ReplaceKeys.ResetBindings();
        }

        private void Default_Click(object sender, EventArgs e)
        {
            // デフォルト値
            setting.Font = new Font("MS UI Gothic", 9);
            setting.NormalColor = Color.Silver;
            setting.KeyDownColor = Color.Black;
            setting.BackgroundColor = SystemColors.Control;
            setting.BorderColor = Color.Black;
            setting.BorderSize = 1;
            setting.Antialiasing = true;

            // 呼び出し
            SetSettingData();
        }

        private void Antialiasing_CheckedChanged(object sender, EventArgs e)
        {
            setting.Antialiasing = CheckBox_Antialiasing.Checked;
            setting.ReplaceKeys.ResetBindings();
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            if (ComboBox_KeyList.SelectedIndex != -1)
            {
                var key = (Keys)ComboBox_KeyList.SelectedItem;

                if (!setting.ReplaceKeys.Any(v => v.Key == key))
                    setting.ReplaceKeys.Add(new ReplaceKey() { Key = key, Replace = TextBox_Replace.Text });
            }

            // 更新   
            setting.ReplaceKeys.ResetBindings();
        }

        private void Upward_Click(object sender, EventArgs e)
        {
            if (ListBox_ReplaceList.SelectedIndex != -1)
            {
                // 初期値
                var index = ListBox_ReplaceList.SelectedIndex;
                var key = setting.ReplaceKeys[index];

                // 削除
                setting.ReplaceKeys.RemoveAt(index);
                index = index - 1 == -1 ? 0 : index - 1;
                setting.ReplaceKeys.Insert(index, key);

                // 位置
                ListBox_ReplaceList.SelectedIndex = index;
            }
        }

        private void Downward_Click(object sender, EventArgs e)
        {
            if (ListBox_ReplaceList.SelectedIndex < setting.ReplaceKeys.Count - 1)
            {
                // 初期値
                var index = ListBox_ReplaceList.SelectedIndex;
                var key = setting.ReplaceKeys[index];

                // 削除
                setting.ReplaceKeys.RemoveAt(index);
                setting.ReplaceKeys.Insert(index + 1, key);

                // 位置
                ListBox_ReplaceList.SelectedIndex = index + 1;
            }
        }

        private void ReplaceList_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.DesiredType == typeof(string))
            {
                var key = (ReplaceKey)e.Value;

                // フォーマット
                e.Value = $"{Enum.GetName(typeof(Keys), key.Key)} > {key.Replace}";
            }
        }

        private void SetToolTipColor(Control control, Color color) =>
            toolTip.SetToolTip(control, $"R:{color.R} G:{color.G} B:{color.B}");

        private void ComboBox_StackDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox_StackDirection.SelectedIndex == 0)
            {
                setting.Direction = Direction.Left;
            }
            else
            {
                setting.Direction = Direction.Right;
            }

        }
        
        private void SetSettingData()
        {
            // 設定読み込み
            Label_FontView.Font = new Font(setting.Font.FontFamily, 9);
            Label_FontView.Text = $"{setting.Font.FontFamily.Name}, {setting.Font.Size}pt";
            toolTip.SetToolTip(Label_FontView, $"{setting.Font.FontFamily.Name}, {setting.Font.Size}pt");
            Panel_Normal.BackColor = setting.NormalColor;
            SetToolTipColor(Panel_Normal, setting.NormalColor);
            Panel_KeyDown.BackColor = setting.KeyDownColor;
            SetToolTipColor(Panel_KeyDown, setting.KeyDownColor);
            Panel_Background.BackColor = setting.BackgroundColor;
            SetToolTipColor(Panel_Background, setting.BackgroundColor);
            Panel_Border.BackColor = setting.BorderColor;
            SetToolTipColor(Panel_Border, setting.BorderColor);
            NumericUpDown_BorderSize.Value = setting.BorderSize;
            CheckBox_Antialiasing.Checked = setting.Antialiasing;
        }
    }
}
