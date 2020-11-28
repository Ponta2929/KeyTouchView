using ImageLayout.Utility;
using KeyTouchView.Plugin;
using KeyTouchView.Utility.Hook;
using KeyTouchView.Utility.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ImageLayout
{
    public class ImageLayout : IPluginLayout, IPluginSetting
    {
        #region グローバル変数

        // 同期オブジェクト
        private static object syncObject = new object();

        // 設定ファイル
        private string file = "";

        // 設定
        private Setting setting = Setting.GetInstance();
        private SettingForm settingForm;

        // キーリスト
        private List<KeyInfo> captureKeys = new List<KeyInfo>();
        private List<KeyItem> keyItem = new List<KeyItem>();

        // 描画
        private Bitmap surface;
        private Bitmap normalBitmap;
        private Bitmap pressBitmap;

        #endregion

        public event SizeChangedEventHandler SizeChanged;

        public string LayoutName => "ImageLayout";

        public bool HasSettingForm => true;

        public int Width => setting.Width;

        public int Height => setting.Height;

        public void Initialize()
        {
            settingForm = new SettingForm();
            setting.SkinChanged += SkinChanged;

            // スキン適用
            setting.OnSkinChanged();
        }

        private void SkinChanged(object sender, EventArgs e)
        {
            captureKeys.Clear();
            keyItem.Clear();

            if (setting.Skin != null)
            {
                // 画像読み込み
                normalBitmap = new Bitmap($"{Path.GetDirectoryName(setting.Skin.FileName)}\\{setting.Skin.SkinInfo.NormalImage}");
                pressBitmap = new Bitmap($"{Path.GetDirectoryName(setting.Skin.FileName)}\\{setting.Skin.SkinInfo.PressImage}");
                surface = new Bitmap(normalBitmap.Width, normalBitmap.Height);

                // キー情報読み込み
                foreach (var item in setting.Skin.SkinInfo.Keys)
                {
                    captureKeys.Add(new KeyInfo() { Key = item.Key });
                }

                keyItem.AddRange(setting.Skin.SkinInfo.Keys);

                // 設定でリサイズ
                CheckSkinSetting(false);
                WindowResize(new Size(Width, Height));
            }
        }

        public void LoadFile(string path)
        {
            file = $"{path}\\{LayoutName}.xml";
            var files = Directory.GetFiles($"{path}\\{LayoutName}.Skins", "*.xml", SearchOption.AllDirectories);

            try
            {
                // 設定読み込み
                setting.FileDeserialize(file);
            }
            catch
            {

            }
            finally
            {
            }

            foreach (var file in files)
            {
                var skin = new Skin()
                {
                    FileName = file,
                    SkinInfo = Serializer.FileDeserialize<SkinInfomation>(file)
                };

                // 読み込まれたスキンを追加
                setting.SkinFiles.Add(skin);
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

        public void Calc()
        {

        }

        public void Draw(Graphics g)
        {
            if (surface != null)
            {
                using (var s = Graphics.FromImage(surface))
                {
                    // 背景クリア
                    s.Clear(Color.Transparent);

                    // 通常状態描画
                    if (normalBitmap != null)
                    {
                        s.DrawImage(normalBitmap, new Rectangle(Point.Empty, normalBitmap.Size), new Rectangle(Point.Empty, normalBitmap.Size), GraphicsUnit.Pixel);
                    }

                    // キー押下時描画
                    if (normalBitmap != null)
                    {
                        lock (syncObject)
                        {
                            for (var i = 0; i < captureKeys.Count; i++)
                            {
                                var hit = keyItem.Where(target => target.Key == captureKeys[i].Key).ToArray();

                                foreach (var find in hit)
                                {
                                    if (captureKeys[i].UpDown == KeyboardUpDown.Down)
                                    {
                                        s.DrawImage(pressBitmap, new Rectangle(find.X, find.Y, find.Width, find.Height), new Rectangle(find.X, find.Y, find.Width, find.Height), GraphicsUnit.Pixel);
                                    }
                                }
                            }
                        }
                    }

                    // サーフェイスを転写
                    g.DrawImage(surface, new Rectangle(0, 0, Width, Height));
                }
            }
        }

        public void KeyboardHooked(KeyboardHookedEventArgs e)
        {
            lock (syncObject)
            {
                var find = captureKeys.Find(target => target.Key == e.KeyCode);

                if (find != null)
                {
                    find.UpDown = e.UpDown;
                }
            }
        }

        public void MouseHooked(MouseHookedEventArgs e)
        {
            // マウスフックは実装しません。
        }

        public void WindowResize(Size size)
        {
            // 比率を保ってリサイズ
            if (normalBitmap != null && normalBitmap.Width != 0 && normalBitmap.Height != 0)
            {
                var px = (float)normalBitmap.Width / (float)normalBitmap.Height;

                if (Width != size.Width)
                {
                    OnSizeChanged(setting.Width = size.Width, setting.Height = (int)((float)setting.Width / px));
                }
                else
                {
                    OnSizeChanged(setting.Width = (int)((float)size.Height * px), setting.Height = size.Height);
                }

                // スキン設定チェック
                CheckSkinSetting(true);
            }
        }

        public void OnSizeChanged(int width, int height) =>
            SizeChanged?.Invoke(this, new SizeChangedEventArgs(width, height));

        public void ShowSettingForm(Form owner) =>
            settingForm.Show();

        public void Dispose()
        {

        }

        private void CheckSkinSetting(bool set)
        {
            if (setting.SkinSetting.Any(target => target.SkinFile == setting.Skin.FileName))
            {
                var find = setting.SkinSetting.Where(target => target.SkinFile == setting.Skin.FileName).ToArray()[0];

                if (set)
                {
                    find.Width = Width;
                    find.Height = Height;
                }
                else
                {

                    setting.Width = find.Width;
                    setting.Height = find.Height;
                }
            }
            else
            {
                setting.SkinSetting.Add(new SkinSetting() { SkinFile = setting.Skin.FileName, Width = Width, Height = Height });
            }
        }
    }
}
