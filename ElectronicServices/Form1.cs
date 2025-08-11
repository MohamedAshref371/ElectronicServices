namespace ElectronicServices
{
    public partial class Form1 : Form
    {
        private readonly int SizeX = 950, SizeY = 700;
        private int NewSizeX = 950, NewSizeY = 700;

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = new() { Interval = 10 };

            timer.Tick += (s, e1) =>
            {
                timer.Stop();

                if (Control.MouseButtons == MouseButtons.None)
                    this.Opacity = 1.0;
            };


            MouseEventHandler meh = (s, e1) =>
            {
                if (WindowState != FormWindowState.Maximized && e1.Button == MouseButtons.Left)
                {
                    this.Opacity = 0.9;
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);

                    timer.Start();
                }
            };

            headerPanel.MouseDown += meh; ;
            formIcon.MouseDown += meh;
            formTitle.MouseDown += meh;
            phoneNumber.MouseDown += meh;

        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        FormSize? fs;
        private void MaximizeBtn_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
                maximizeBtn.Image = Properties.Resources.Fullscreen;
                fs = new FormSize(NewSizeX, NewSizeY, SizeX, SizeY);
                fs.SetControls(Controls);
                fs = null;
            }
            else if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
                maximizeBtn.Image = Properties.Resources.FullscreenExit;
                NewSizeX = Size.Width; NewSizeY = Size.Height;
                fs = new FormSize(SizeX, SizeY, NewSizeX, NewSizeY);
                fs.SetControls(Controls);
            }

            if (WindowState != FormWindowState.Minimized)
            {
                int minSize = Math.Min(minimizeBtn.Width, minimizeBtn.Height);
                Size newSize = new(minSize, minSize);
                minimizeBtn.ImageSize = newSize;
                maximizeBtn.ImageSize = newSize;
                exitBtn.ImageSize = newSize;
            }
        }

        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }


    }
}
