using KeyTouchView.Plugin;
using KeyTouchView.Utility;
using KeyTouchView.Utility.Hook;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace StackLayout
{
    public class StackLayout : IPluginLayout, IPluginSetting
    {
        #region グローバル変数

        private static object syncObject = new object();

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

        public string LayoutName => "StackLayout";

        public bool HasSettingForm => true;

        public int Width => setting.Width;

        public int Height => (int)Utility.MeasureString("012ABCabcあいう", setting.Font).Height;

        public void Initialize()
        {
            settingForm = new SettingForm();

            // キーリスト
            setting.ReplaceKeys.ListChanged += ReplaceKeysChanged;

            // 更新  
            setting.ReplaceKeys.ResetBindings();
        }

        public void LoadFile(string path)
        {
            file = string.Format("{0}\\{1}.xml", path, LayoutName);

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

        private void ReplaceKeysChanged(object sender, ListChangedEventArgs e)
        {
            replaceKeys.Clear();
            replaceKeys.AddRange(setting.ReplaceKeys);

            // サイズ変更通知
            OnSizeChanged(setting.Width, Height);
        }

        public void Calc()
        {
            lock (syncObject)
            {
                for (var i = 0; i < captureKeys.Count; i++)
                {
                    var item = captureKeys[i];
                    var x = item.Point.X;

                    if (setting.Direction == Direction.Left)
                    {
                        if (item.UpDown == KeyboardUpDown.Down)
                        {
                            // なめらかに移動させる
                            if (i == 0)
                            {
                                x += (0f - x) * 0.075f;
                            }
                            else
                            {
                                x += (captureKeys[i - 1].Point.X + captureKeys[i - 1].Size.Width - x) * 0.075f;
                            }
                        }
                        else
                        {
                            var ca = (float)item.Color.A;
                            var cr = (float)item.Color.R;
                            var cg = (float)item.Color.G;
                            var cb = (float)item.Color.B;

                            // 座標フェード
                            x += (-item.Size.Width - 10 - x) * 0.125f;

                            // 色フェード
                            ca += (setting.BackgroundColor.A - ca) * 0.075f;
                            cr += (setting.BackgroundColor.R - cr) * 0.075f;
                            cg += (setting.BackgroundColor.G - cg) * 0.075f;
                            cb += (setting.BackgroundColor.B - cb) * 0.075f;

                            item.Color = ColorEx.FromArgb((int)ca, (int)cr, (int)cg, (int)cb);
                        }

                        item.Point = new PointF(x, 0.0f);

                        if (item.Point.X < -item.Size.Width)
                        {
                            captureKeys.Remove(item);
                            continue;
                        }
                    }
                    else
                    {
                        var size = Utility.MeasureString(item.String, setting.Font);

                        if (item.UpDown == KeyboardUpDown.Down)
                        {
                            if (i == 0)
                            {
                                x += ((Width - size.Width) - x) * 0.075f;
                            }
                            else
                            {
                                x += (captureKeys[i - 1].Point.X - size.Width - x) * 0.075f;
                            }
                        }
                        else
                        {
                            var ca = (float)item.Color.A;
                            var cr = (float)item.Color.R;
                            var cg = (float)item.Color.G;
                            var cb = (float)item.Color.B;

                            x += (Width + 10 - x) * 0.125f;
                            ca += (setting.BackgroundColor.A - ca) * 0.075f;
                            cr += (setting.BackgroundColor.R - cr) * 0.075f;
                            cg += (setting.BackgroundColor.G - cg) * 0.075f;
                            cb += (setting.BackgroundColor.B - cb) * 0.075f;

                            item.Color = ColorEx.FromArgb((int)ca, (int)cr, (int)cg, (int)cb);
                        }

                        item.Point = new PointF(x, 0.0f);

                        if (item.Point.X > Width)
                        {
                            captureKeys.Remove(item);
                            continue;
                        }
                    }
                }
            }
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

            for (var i = captureKeys.Count - 1; i >= 0; i--)
            {
                var key = captureKeys[i];

                solidBrush.Color = key.Color;

                // StackLayout描画
                g.DrawString(key.String, setting.Font, solidBrush, key.Point);
            }
        }

        public void KeyboardHooked(KeyboardHookedEventArgs e)
        {
            lock (syncObject)
            {
                if (e.UpDown == KeyboardUpDown.Down)
                {
                    if (captureKeys.Count > 0 && captureKeys.Last().Key == e.KeyCode && captureKeys.Last().UpDown == KeyboardUpDown.Down) { }
                    else
                    {
                        var replace = Utility.GetReplacedName(e.KeyCode, replaceKeys.ToArray());
                        var size = Utility.MeasureString(replace, setting.Font);

                        if (setting.Direction == Direction.Left)
                        {
                            // キー追加
                            captureKeys.Add(new CaptureKey() { Key = e.KeyCode, Point = new Point(Width, 0), UpDown = KeyboardUpDown.Down, Color = setting.KeyDownColor, String = replace, Size = size });
                        }
                        else
                        {  // キー追加
                            captureKeys.Add(new CaptureKey() { Key = e.KeyCode, Point = new Point((int)-size.Width, 0), UpDown = KeyboardUpDown.Down, Color = setting.KeyDownColor, String = replace, Size = size });
                        }
                    }
                }
                else if (e.UpDown == KeyboardUpDown.Up)
                {
                    captureKeys.ForEach(item =>
                    {
                        if (item.UpDown == KeyboardUpDown.Down && item.Key == e.KeyCode)
                        {
                            item.UpDown = KeyboardUpDown.Up;
                        }
                    });
                }
            }

            // 設定フォームにキーボードフックを流す
            settingForm.KeyboardHooked(this, e);
        }
        public void MouseHooked(MouseHookedEventArgs e)
        {
            // マウスフックは実装しません。
        }

        public void WindowResize(Size size) =>
            OnSizeChanged(setting.Width = size.Width, Height);

        public void OnSizeChanged(int width, int height) =>
            SizeChanged?.Invoke(this, new SizeChangedEventArgs(width, height));

        public void ShowSettingForm(Form owner) =>
            settingForm.Show();

        public void Dispose()
        {

        }
    }
}
