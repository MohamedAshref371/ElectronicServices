using Guna.UI2.WinForms;

namespace ElectronicServices;

class FormSize(int oldSizeX, int oldSizeY, int newSizeX, int newSizeY)
{
    private readonly double xDiv = newSizeX / (double)oldSizeX,
                            yDiv = newSizeY / (double)oldSizeY;

    public void SetControl(Control control)
    {
        Point point = control.Location; Size size = control.Size;
        control.Font = new Font(control.Font.FontFamily, Roundf(control.Font.Size * ((xDiv < yDiv && xDiv > 1 || xDiv > yDiv && xDiv < 1) ? xDiv : yDiv)));
        control.Location = new Point(Round(point.X * xDiv), Round(point.Y * yDiv));
        control.Size = new Size(Round(size.Width * xDiv), Round(size.Height * yDiv));
    }

    public void SetControls(Control.ControlCollection controls)
    {
        for (int i = 0; i < controls.Count; i++)
        {
            SetControl(controls[i]);
            if (controls[i] is Panel || controls[i] is CustomerRow || controls[i] is TransactionRow || controls[i] is WalletRow || controls[i] is RecordRow || controls[i] is ExpenseRow)
                SetControls(controls[i].Controls);
        }
    }

    public Point GetNewPoint(Point p) => new(Round(p.X * xDiv), Round(p.Y * yDiv));

    public Size GetNewSize(Size sz) => new(Round(sz.Width * xDiv), Round(sz.Height * yDiv));

    public int GetNewX(int x) => Round(x * xDiv);

    public int GetNewY(int y) => Round(y * yDiv);

    public static int Round(double num) => (int)Math.Round(num);

    public static float Roundf(double num) => (float)Math.Round(num, 3);
}
