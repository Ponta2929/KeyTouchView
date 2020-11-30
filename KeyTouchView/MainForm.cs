using KeyTouchView.Plugin;
using KeyTouchView.Utility.Hook;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyTouchView
{
    public partial class MainForm : Form
    {
        #region グローバル変数 

        // グローバルフック
        private KeyboardHook keyboardHook;
        private MouseHook mouseHook;

        // 設定ファイル
        private Setting setting;

        // 描画計算タイマー
        private System.Timers.Timer draw_timer;
        private System.Timers.Timer calc_timer;

        // 選択中のレイアウト
        private IPluginLayout layout;

        // プラグインローダー
        private PluginLoader<IPluginLayout> plugins;

        // 描画サーフェイス
        private Bitmap surface;

        #endregion

        public MainForm()
        {
            InitializeComponent();

            // Initialize()
            Initialize();
        }

        private void Initialize()
        {
            // 変数初期化
            Task.Run(() =>
            {
                // グローバルフックのインスタンスを取得
                keyboardHook = KeyboardHook.GetInstance();
                mouseHook = MouseHook.GetInstance();

                // 別スレッドで実行
                Application.Run(keyboardHook);
                Application.Run(mouseHook);
            });

            setting = Setting.GetInstance();
            plugins = new PluginLoader<IPluginLayout>();
            draw_timer = new System.Timers.Timer(1000.0f / 60.0f);
            calc_timer = new System.Timers.Timer(1000.0f / 60.0f);
            surface = new Bitmap(1, 1);

            try
            {
                // 設定読み込み
                setting.FileDeserialize($"{Application.StartupPath}\\setting.xml");
            }
            catch
            {

            }
            finally
            {
                Location = setting.Position;

                plugins.Load($"{Path.GetDirectoryName(Environment.GetCommandLineArgs()[0])}\\plugins");

                try
                {
                    foreach (var item in plugins.Plugin)
                    {
                        // メニュー項目作成
                        var menuItem = new ToolStripMenuItem();
                        {
                            menuItem.Text = item.LayoutName;
                            menuItem.Name = item.LayoutName;
                            menuItem.Tag = item;
                            menuItem.Click += ToolStripMenuItem_Layout_Click;
                        }

                        item.SizeChanged += LayoutSizeChanged;

                        if (item is IPluginSetting)
                        {
                            var menuSubItem = new ToolStripMenuItem();
                            {
                                menuSubItem.Text = "設定";
                                menuSubItem.Tag = menuItem;
                                menuSubItem.Click += ToolStripMenuItem_Layout_Setting_Click;
                            }

                            // 設定読み込み
                            ((IPluginSetting)item).LoadFile($"{Application.StartupPath}\\plugins");

                            menuItem.DropDownItems.Add(menuSubItem);
                        }

                        item.Initialize();

                        // 追加
                        ToolStripMenuItem_Layout.DropDownItems.Add(menuItem);
                    }

                    foreach (var item in ToolStripMenuItem_Layout.DropDownItems)
                    {
                        var menuItem = (ToolStripMenuItem)item;

                        // レイアウト適応
                        if (menuItem.Name == setting.LayoutName)
                        {
                            menuItem.PerformClick();

                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"プラグイン読み込み時にエラーが発生しました。\n\r{ex.Message}", "読み込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // イベント受信
            keyboardHook.KeyboardHooked += KeyboardHooked;
            mouseHook.MouseHooked += MouseHooked;

            // ダブルバッファリング
            DoubleBuffered = true;

            // 計算タイマー開始
            calc_timer.Elapsed += CalcTimer_Elapsed;
            calc_timer.Start();

            // タイマー開始
            draw_timer.SynchronizingObject = this;
            draw_timer.Elapsed += DrawTimer_Elapsed;
            draw_timer.Start();
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (layout is IPluginMouseEvent)
                ((IPluginMouseEvent)layout)?.MouseMove(e);
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (layout is IPluginMouseEvent)
                ((IPluginMouseEvent)layout)?.MouseUp(e);
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (layout is IPluginMouseEvent)
                ((IPluginMouseEvent)layout)?.MouseDown(e);
        }

        private void LayoutSizeChanged(object sender, SizeChangedEventArgs e) =>
            SetClientSizeCore(e.Width, e.Height);

        private void MouseHooked(object sender, MouseHookedEventArgs e) =>
            layout?.MouseHooked(e);

        private void CalcTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) =>
            layout?.Calc();

        private void DrawTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) =>
            Invalidate();

        protected override void OnPaint(PaintEventArgs e) =>
            layout?.Draw(e.Graphics);

        private void KeyboardHooked(object sender, KeyboardHookedEventArgs e) =>
            layout?.KeyboardHooked(e);

        private void MainForm_Load(object sender, EventArgs e) =>
            layout?.WindowResize(ClientSize);

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            keyboardHook.Dispose();
            mouseHook.Dispose();

            // 設定書き込み
            try
            {
                setting.FileSerialize(Application.StartupPath + "\\setting.xml");

                foreach (var item in plugins.Plugin)
                {
                    if (item is IPluginSetting)
                    {
                        ((IPluginSetting)item).SaveFile();
                    }

                    // 破棄
                    item.Dispose();
                }
            }
            catch
            {

            }
        }

        private void MainForm_LocationChanged(object sender, EventArgs e) =>
            setting.Position = Location;

        private void MainForm_ResizeEnd(object sender, EventArgs e) =>
            layout?.WindowResize(ClientSize);

        private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e) =>
            Close();

        private void ToolStripMenuItem_Layout_Click(object sender, EventArgs e)
        {
            var @object = (ToolStripMenuItem)sender;

            foreach (var item in ToolStripMenuItem_Layout.DropDownItems)
            {
                ((ToolStripMenuItem)item).Checked = false;
            }

            // レイアウト変更
            layout = ((IPluginLayout)@object.Tag);
            setting.LayoutName = layout.LayoutName;
            @object.Checked = true;

            // レイアウト情報適用
            SetClientSizeCore(layout?.Width ?? Width, layout?.Height ?? Height);
        }

        private void ToolStripMenuItem_Layout_Setting_Click(object sender, EventArgs e) =>
            ((IPluginSetting)((ToolStripMenuItem)((ToolStripMenuItem)sender).Tag).Tag).ShowSettingForm(this);

    }
}