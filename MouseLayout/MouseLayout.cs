using KeyTouchView.Plugin;
using KeyTouchView.Utility.Hook;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MouseLayout
{
    public class MouseLayout : IPluginLayout
    {
        public string LayoutName => "MouseLayout";

        public int? Width { get; set; }
        public int? Height { get; set; }

        private int x, y;
        private int p_x, p_y;

        private bool left, right;

        Bitmap bitmap = new Bitmap("E:\\84438564.jpeg");

        Timer timer = new Timer(1000);

        public void Calc()
        {
        }


        public void Initialize()
        {
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        public void Dispose()
        {

        }

        public void Draw(System.Drawing.Graphics g)
        {
            // 描画処理
            g.Clear(Color.White);


            g.DrawImage(bitmap, new Point());
            if (left)
                g.DrawString("←クリック", new Font("MS UI Gothic", 12), Brushes.Black, 0, 30);
            if (right)
                g.DrawString("→クリック", new Font("MS UI Gothic", 12), Brushes.Black, 0, 60);

            g.DrawString(string.Format("{0} x {1}", x, y), new Font("MS UI Gothic", 12), Brushes.Black, 0, 0);

            g.DrawLine(Pens.Red, x, y, p_x, p_y);

        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            p_x = x;
            p_y = y;
        }

        public void KeyboardHooked(KeyboardHookedEventArgs e)
        {

        }

        public void MouseHooked(MouseHookedEventArgs e)
        {
            if (e.State == MouseMessage.LeftButtonUp)
                left = false;
            if (e.State == MouseMessage.LeftButtonDown)
                left = true;
            if (e.State == MouseMessage.RightButtonUp)
                right = false;
            if (e.State == MouseMessage.RightButtonDown)
                right = true;

            x = e.Point.X;
            y = e.Point.Y;
        }

        public void WindowResize(System.Drawing.Size size)
        {

        }
    }
}
