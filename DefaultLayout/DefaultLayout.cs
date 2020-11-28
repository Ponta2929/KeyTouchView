using KeyTouchView.Plugin;
using KeyTouchView.Utility;
using KeyTouchView.Utility.Hook;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DefaultLayout
{
    public class DefaultLayout : IPluginLayout, IPluginSetting
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

        #endregion

        public event SizeChangedEventHandler SizeChanged;

        public string LayoutName => "DefaultLayout";

        public bool HasSettingForm => true;

        public int Width { get; private set; }

        public int Height => (int)Utility.MeasureString("012ABCabcあいう", setting.Font).Height;

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

        private void CaptureKeysChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            captureKeys.Clear();
            captureKeys.AddRange(setting.CaptureKeys);

            // リスト変更
            ListChanged();
        }
        private void ReplaceKeysChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            replaceKeys.Clear();
            replaceKeys.AddRange(setting.ReplaceKeys);

            // リスト変更
            ListChanged();
        }

        /// <summary>
        /// リスト変更時アイテム整列
        /// </summary>
        public void ListChanged()
        {
            var width = 0.0f;

            CaptureKey prev = null;

            captureKeys.ForEach(item =>
            {
                item.String = Utility.GetReplacedName(item.Key, replaceKeys.ToArray());
                item.Size = Utility.MeasureString(item.String, setting.Font);

                if (prev == null)
                    item.Point = Point.Empty;
                else
                    item.Point = new PointF(prev.Point.X + prev.Size.Width, 0);

                width += (prev = item).Size.Width;
            });

            // サイズ変更通知
            OnSizeChanged(Width = (int)width, Height);
        }

        public void Calc()
        {

        }

        public void Draw(Graphics g)
        {
            // 描画処理
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

                // 描画
                g.DrawString(item.String, setting.Font, solidBrush, item.Point);
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

        public void WindowResize(Size size) =>
            OnSizeChanged(Width, Height);

        public void OnSizeChanged(int width, int height) =>
            SizeChanged?.Invoke(this, new SizeChangedEventArgs(width, height));

        public void ShowSettingForm(Form owner) =>
            settingForm.Show();

        public void Dispose()
        {

        }
    }
}
