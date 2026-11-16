using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DesktopPet
{
    public sealed class PetWindow : Form
    {
        private readonly Timer animationTimer;
        private readonly string[] petMessages = new string[]
        {
            "今天也要元气满满！",
            "谢谢你的摸摸～",
            "陪我玩一会儿吧！"
        };
        private string message = "今天也要元气满满！";
        private Point mouseDownScreen;
        private Point windowDownLocation;
        private bool isDragging;
        private int frame;
        private int mood;
        private DateTime reactionExpires;

        private PetWindow()
        {
            Text = "小橘桌面宠物";
            ClientSize = new Size(280, 350);
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            BackColor = Color.Magenta;
            TransparencyKey = Color.Magenta;
            TopMost = true;
            DoubleBuffered = true;
            KeyPreview = true;

            Rectangle workArea = Screen.PrimaryScreen.WorkingArea;
            Location = new Point(
                Math.Max(workArea.Left, workArea.Right - Width - 28),
                Math.Max(workArea.Top, workArea.Bottom - Height - 36));

            animationTimer = new Timer();
            animationTimer.Interval = 80;
            animationTimer.Tick += delegate
            {
                frame++;
                if (DateTime.UtcNow > reactionExpires)
                {
                    reactionExpires = DateTime.MinValue;
                }
                Invalidate();
            };
            animationTimer.Start();

            FormClosed += delegate { animationTimer.Dispose(); };
            KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    Close();
                }
            };
        }

        public static void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PetWindow());
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            graphics.Clear(TransparencyKey);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            DrawBubble(graphics);

            int bounce = (int)Math.Round(Math.Sin(frame * 0.075) * 2.0);
            DrawPet(graphics, bounce);
            DrawReaction(graphics, bounce);
            DrawControls(graphics);
            DrawCloseButton(graphics);
        }

        private void DrawBubble(Graphics graphics)
        {
            Rectangle bubble = new Rectangle(25, 12, 230, 40);
            using (SolidBrush fill = new SolidBrush(Color.FromArgb(250, 255, 250, 235)))
            {
                FillRoundedRectangle(graphics, fill, bubble, 15);
            }

            using (Pen outline = new Pen(Color.FromArgb(220, 228, 166, 119), 1.5f))
            {
                DrawRoundedRectangle(graphics, outline, bubble, 15);
            }

            using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(112, 75, 58)))
            using (Font font = new Font("Microsoft YaHei UI", 10.0f, FontStyle.Bold))
            using (StringFormat format = new StringFormat())
            {
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                graphics.DrawString(message, font, textBrush, bubble, format);
            }

            Point[] tail = new Point[]
            {
                new Point(126, 50),
                new Point(139, 50),
                new Point(132, 59)
            };
            using (SolidBrush tailBrush = new SolidBrush(Color.FromArgb(250, 255, 250, 235)))
            using (Pen tailOutline = new Pen(Color.FromArgb(220, 228, 166, 119), 1.2f))
            {
                graphics.FillPolygon(tailBrush, tail);
                graphics.DrawLine(tailOutline, tail[0], tail[2]);
                graphics.DrawLine(tailOutline, tail[2], tail[1]);
            }
        }

        private void DrawPet(Graphics graphics, int bounce)
        {
            int y = bounce;
            Color outlineColor = Color.FromArgb(178, 92, 56);
            Color furColor = Color.FromArgb(247, 171, 112);

            using (SolidBrush shadow = new SolidBrush(Color.FromArgb(50, 93, 62, 44)))
            {
                graphics.FillEllipse(shadow, 80, 276, 120, 11);
            }

            using (Pen tail = new Pen(outlineColor, 7.0f))
            {
                tail.StartCap = LineCap.Round;
                tail.EndCap = LineCap.Round;
                graphics.DrawBezier(tail, 191, 224 + y, 222, 226 + y, 224, 193 + y, 205, 190 + y);
            }

            using (SolidBrush fur = new SolidBrush(furColor))
            using (Pen outline = new Pen(outlineColor, 2.3f))
            using (SolidBrush innerEar = new SolidBrush(Color.FromArgb(255, 209, 146)))
            using (SolidBrush belly = new SolidBrush(Color.FromArgb(255, 226, 190)))
            using (SolidBrush paw = new SolidBrush(Color.FromArgb(255, 219, 177)))
            {
                graphics.FillEllipse(fur, 77, 77 + y, 52, 59);
                graphics.FillEllipse(fur, 151, 77 + y, 52, 59);
                graphics.DrawEllipse(outline, 77, 77 + y, 52, 59);
                graphics.DrawEllipse(outline, 151, 77 + y, 52, 59);
                graphics.FillEllipse(innerEar, 89, 90 + y, 27, 34);
                graphics.FillEllipse(innerEar, 164, 90 + y, 27, 34);

                graphics.FillEllipse(fur, 80, 162 + y, 120, 103);
                graphics.DrawEllipse(outline, 80, 162 + y, 120, 103);
                graphics.FillEllipse(belly, 104, 194 + y, 72, 55);
                graphics.FillEllipse(paw, 89, 244 + y, 40, 22);
                graphics.FillEllipse(paw, 151, 244 + y, 40, 22);

                graphics.FillEllipse(fur, 73, 91 + y, 134, 116);
                graphics.DrawEllipse(outline, 73, 91 + y, 134, 116);
            }

            DrawFace(graphics, y);

            using (SolidBrush sparkle = new SolidBrush(Color.FromArgb(255, 215, 136)))
            using (Font starFont = new Font("Segoe UI Symbol", 14.0f, FontStyle.Bold))
            {
                graphics.DrawString("✦", starFont, sparkle, 49, 150 + y);
                graphics.DrawString("✦", starFont, sparkle, 207, 135 + y);
            }
        }

        private void DrawFace(Graphics graphics, int y)
        {
            using (SolidBrush eye = new SolidBrush(Color.FromArgb(78, 53, 47)))
            using (SolidBrush cheek = new SolidBrush(Color.FromArgb(160, 238, 127, 125)))
            using (SolidBrush nose = new SolidBrush(Color.FromArgb(201, 111, 87)))
            using (Pen smile = new Pen(Color.FromArgb(140, 75, 57), 2.4f))
            {
                if (mood == 2)
                {
                    smile.StartCap = LineCap.Round;
                    smile.EndCap = LineCap.Round;
                    graphics.DrawArc(smile, 103, 137 + y, 22, 12, 15, 150);
                    graphics.DrawArc(smile, 155, 137 + y, 22, 12, 15, 150);
                }
                else
                {
                    graphics.FillEllipse(eye, 108, 136 + y, 11, mood == 1 ? 12 : 16);
                    graphics.FillEllipse(eye, 160, 136 + y, 11, mood == 1 ? 12 : 16);
                }

                graphics.FillEllipse(cheek, 91, 158 + y, 23, 12);
                graphics.FillEllipse(cheek, 165, 158 + y, 23, 12);
                graphics.FillEllipse(nose, 136, 158 + y, 9, 7);
                smile.StartCap = LineCap.Round;
                smile.EndCap = LineCap.Round;
                graphics.DrawArc(smile, 127, 160 + y, 28, 17, 15, 150);
            }
        }

        private void DrawReaction(Graphics graphics, int bounce)
        {
            if (DateTime.UtcNow > reactionExpires)
            {
                return;
            }

            using (SolidBrush heartBrush = new SolidBrush(Color.FromArgb(225, 238, 105, 117)))
            using (Font heartFont = new Font("Segoe UI Symbol", 18.0f, FontStyle.Bold))
            {
                graphics.DrawString("♥", heartFont, heartBrush, 54, 100 + bounce);
                graphics.DrawString("♥", heartFont, heartBrush, 194, 109 - bounce);
            }
        }

        private void DrawControls(Graphics graphics)
        {
            string[] labels = new string[] { "摸摸", "玩耍", TopMost ? "置顶" : "取消" };
            int[] centers = new int[] { 70, 140, 210 };

            for (int i = 0; i < centers.Length; i++)
            {
                Rectangle button = new Rectangle(centers[i] - 22, 286, 44, 44);
                Color buttonColor = i == 2 && TopMost
                    ? Color.FromArgb(255, 246, 198, 164)
                    : Color.FromArgb(238, 255, 250, 239);

                using (SolidBrush fill = new SolidBrush(buttonColor))
                using (Pen outline = new Pen(Color.FromArgb(215, 212, 150, 112), 1.5f))
                {
                    graphics.FillEllipse(fill, button);
                    graphics.DrawEllipse(outline, button);
                }

                DrawControlIcon(graphics, i, centers[i]);

                using (SolidBrush labelBrush = new SolidBrush(Color.FromArgb(130, 81, 61)))
                using (Font labelFont = new Font("Microsoft YaHei UI", 8.0f, FontStyle.Bold))
                using (StringFormat format = new StringFormat())
                {
                    format.Alignment = StringAlignment.Center;
                    graphics.DrawString(labels[i], labelFont, labelBrush, centers[i], 331, format);
                }
            }
        }

        private void DrawControlIcon(Graphics graphics, int index, int center)
        {
            if (index == 0)
            {
                using (SolidBrush heart = new SolidBrush(Color.FromArgb(225, 111, 117)))
                using (Font font = new Font("Segoe UI Symbol", 21.0f, FontStyle.Bold))
                using (StringFormat format = new StringFormat())
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    graphics.DrawString("♥", font, heart, new Rectangle(center - 21, 286, 42, 42), format);
                }
            }
            else if (index == 1)
            {
                using (Pen ball = new Pen(Color.FromArgb(194, 125, 75), 2.0f))
                {
                    graphics.DrawEllipse(ball, center - 11, 297, 22, 22);
                    graphics.DrawArc(ball, center - 5, 298, 13, 20, 75, 205);
                    graphics.DrawArc(ball, center - 12, 302, 22, 11, 180, 180);
                }
            }
            else
            {
                using (Pen pin = new Pen(Color.FromArgb(194, 125, 75), 2.6f))
                using (SolidBrush pinHead = new SolidBrush(Color.FromArgb(225, 136, 104)))
                {
                    pin.StartCap = LineCap.Round;
                    pin.EndCap = LineCap.Round;
                    graphics.FillEllipse(pinHead, center - 6, 295, 12, 12);
                    graphics.DrawLine(pin, center, 306, center, 319);
                    graphics.DrawLine(pin, center, 316, center - 6, 322);
                }
            }
        }

        private void DrawCloseButton(Graphics graphics)
        {
            using (SolidBrush fill = new SolidBrush(Color.FromArgb(235, 255, 250, 239)))
            using (Pen outline = new Pen(Color.FromArgb(215, 212, 150, 112), 1.2f))
            using (Pen cross = new Pen(Color.FromArgb(170, 104, 77), 1.7f))
            {
                graphics.FillEllipse(fill, 253, 4, 22, 22);
                graphics.DrawEllipse(outline, 253, 4, 22, 22);
                graphics.DrawLine(cross, 261, 11, 267, 17);
                graphics.DrawLine(cross, 267, 11, 261, 17);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            mouseDownScreen = Cursor.Position;
            windowDownLocation = Location;
            isDragging = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if ((Control.MouseButtons & MouseButtons.Left) == 0)
            {
                return;
            }

            Point cursor = Cursor.Position;
            int dx = cursor.X - mouseDownScreen.X;
            int dy = cursor.Y - mouseDownScreen.Y;
            if (!isDragging && (Math.Abs(dx) > 4 || Math.Abs(dy) > 4))
            {
                isDragging = true;
            }

            if (isDragging)
            {
                Location = new Point(windowDownLocation.X + dx, windowDownLocation.Y + dy);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left && !isDragging)
            {
                HandleClick(e.Location);
            }
            isDragging = false;
        }

        private void HandleClick(Point point)
        {
            if (point.X >= 250 && point.Y <= 29)
            {
                Close();
                return;
            }

            if (point.Y >= 280 && point.Y <= 346)
            {
                if (point.X >= 42 && point.X < 105)
                {
                    mood = (mood + 1) % 3;
                    React("呼噜～好喜欢你！");
                }
                else if (point.X >= 105 && point.X < 175)
                {
                    mood = 1;
                    React("耶！我们来玩吧！");
                }
                else if (point.X >= 175 && point.X <= 245)
                {
                    TopMost = !TopMost;
                    React(TopMost ? "置顶已开启，陪着你。" : "好呀，我先乖乖待着。");
                }
                return;
            }

            if (point.Y >= 75 && point.Y <= 275)
            {
                mood = (mood + 1) % 3;
                React(petMessages[frame % petMessages.Length]);
            }
        }

        private void React(string newMessage)
        {
            message = newMessage;
            reactionExpires = DateTime.UtcNow.AddSeconds(2.2);
            Invalidate();
        }

        private static void FillRoundedRectangle(Graphics graphics, Brush brush, Rectangle bounds, int radius)
        {
            using (GraphicsPath path = RoundedRectanglePath(bounds, radius))
            {
                graphics.FillPath(brush, path);
            }
        }

        private static void DrawRoundedRectangle(Graphics graphics, Pen pen, Rectangle bounds, int radius)
        {
            using (GraphicsPath path = RoundedRectanglePath(bounds, radius))
            {
                graphics.DrawPath(pen, path);
            }
        }

        private static GraphicsPath RoundedRectanglePath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
