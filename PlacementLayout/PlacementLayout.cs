using KeyTouchView.Plugin;
using KeyTouchView.Utility;
using KeyTouchView.Utility.Hook;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PlacementLayout
{
    public class PlacementLayout : IPluginLayout, IPluginSetting, IPluginMouseEvent
    {
        #region グローバル変数

        // 設定ファイル
        private string file = "";

        // 設定
        private Setting setting = Setting.GetInstance();
        private SettingForm settingForm;

        // キーリスト
        private List<CaptureKey> captureKeys = new List<CaptureKey>();
        private List<ReplaceKey> replaceKeys = new List<ReplaceKey>();

        // 描画
        private Pen pen = new Pen(Color.Black, 1);
        private SolidBrush solidBrush = new SolidBrush(Color.Black);

        // ドラッグ
        private int mouse_x, mouse_y;
        private CaptureKey capture;

        #endregion

        public event SizeChangedEventHandler SizeChanged;

        public string LayoutName => "PlacementLayout";

        public bool HasSettingForm => true;

        public int Width => setting.Width;

        public int Height => setting.Height;

        public void Initialize()
        {
            settingForm = new SettingForm();

            // キーリスト
            setting.CaptureKeys.ListChanged += CaptureKeysChanged;
            setting.ReplaceKeys.ListChanged += ReplaceKeysChanged;

            // 更新  
            setting.CaptureKeys.ResetBindings();
            setting.ReplaceKeys.ResetBindings();
        }
        public void LoadFile(string path)
        {
            file = $"{path}\\{LayoutName}.xml";

            try
            {
                // 設定読み込み
                setting.FileDeserialize(file);
            }
            catch
            {

            }
        }

        public void SaveFile()
        {
            try
            {
                // 設定保存
                setting.FileSerialize(file);
            }
            catch
            {

            }
        }

        private void CaptureKeysChanged(object sender, ListChangedEventArgs e)
        {
            captureKeys.Clear();
            captureKeys.AddRange(setting.CaptureKeys);

            // リスト変更
            ListChanged();
        }

        private void ReplaceKeysChanged(object sender, ListChangedEventArgs e)
        {
            replaceKeys.Clear();
            replaceKeys.AddRange(setting.ReplaceKeys);

            // リスト変更
            ListChanged();
        }

        public void ListChanged()
        {
            captureKeys.ForEach(item =>
            {
                item.String = Utility.GetReplacedName(item.Key, replaceKeys.ToArray());
                item.Size = Utility.MeasureString(item.String, setting.Font);
            });

            // サイズ変更通知
            OnSizeChanged(Width, Height);
        }

        public void Calc()
        {

        }

        public void Draw(Graphics g)
        {
            // 背景描画
            g.Clear(setting.BackgroundColor);

            // アンチエイリアス
            if (!setting.Antialiasing)
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
            }

            pen.Color = setting.BorderColor;
            pen.Width = setting.BorderSize;

            // 枠描画
            if (setting.BorderSize > 0)
            {
                g.DrawRectangle(pen, new Rectangle(0, 0, Width - 1, Height - 1));
            }

            // キー描画
            captureKeys.ForEach(item =>
            {
                if (item.UpDown == KeyboardUpDown.Up)
                {
                    solidBrush.Color = setting.NormalColor;
                }
                else
                {
                    solidBrush.Color = setting.KeyDownColor;
                }

                g.DrawString(item.String, setting.Font, solidBrush, item.Point);


                if (settingForm.Visible &&
                    mouse_x > item.Point.X && item.Point.X + item.Size.Width > mouse_x &&
                    mouse_y > item.Point.Y && item.Point.Y + item.Size.Height > mouse_y)
                {
                    g.DrawRectangle(Pens.Red, item.Point.X, item.Point.Y, item.Size.Width, item.Size.Height);
                }
            });

        }

        public void KeyboardHooked(KeyboardHookedEventArgs e)
        {
            var find = captureKeys.Find(item => item.Key == e.KeyCode);

            if (find != null)
            {
                find.UpDown = e.UpDown;
            }

            // 設定フォームにキーボードフックを流す
            settingForm.KeyboardHooked(this, e);
        }

        public void MouseHooked(MouseHookedEventArgs e)
        {
            // マウスフックは実装しません。
        }

        public void WindowResize(Size size)
        {
            setting.Width = size.Width;
            setting.Height = size.Height;
        }

        public void ShowSettingForm(Form owner) =>
            settingForm.Show();

        public void MouseMove(MouseEventArgs e)
        {
            mouse_x = e.X;
            mouse_y = e.Y;

            // 項目の移動
            if (settingForm.Visible && e.Button == MouseButtons.Left && capture != null)
            {
                capture.Point = new Point(e.X - (int)(capture.Size.Width / 2), e.Y - (int)(capture.Size.Height / 2));
            }
        }

        public void MouseUp(MouseEventArgs e)
        {
            capture = null;
        }

        public void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (var i = 0; i < captureKeys.Count; i++)
                {
                    var item = captureKeys[i];

                    // カーソル位置の項目選択
                    if (settingForm.Visible &&
                        mouse_x > item.Point.X && item.Point.X + item.Size.Width > mouse_x &&
                        mouse_y > item.Point.Y && item.Point.Y + item.Size.Height > mouse_y)
                    {
                        capture = item;
                    }
                }
            }
        }

        public void OnSizeChanged(int width, int height) =>
            SizeChanged?.Invoke(this, new SizeChangedEventArgs(width, height));

        public void Dispose()
        {

        }
    }
}
