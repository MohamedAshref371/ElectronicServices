using Guna.UI2.WinForms;

namespace ElectronicServices;

class FormSize(int oldSizeX, int oldSizeY, int newSizeX, int newSizeY)
{
    private readonly double xDiv = newSizeX / (double)oldSizeX,
                            yDiv = newSizeY / (double)oldSizeY;

    public void SetControl(Control control, bool setFont = true, bool loc = true)
    {
        if (loc) control.Location = new Point(Round(control.Location.X * xDiv), Round(control.Location.Y * yDiv));
        control.Size = new Size(Round(control.Size.Width * xDiv), Round(control.Size.Height * yDiv));
        if (setFont)
            control.Font = new Font(control.Font.FontFamily, Roundf(control.Font.Size * ((xDiv < yDiv && xDiv > 1 || xDiv > yDiv && xDiv < 1) ? xDiv : yDiv)));
    }

    public void SetControls(Control.ControlCollection controls)
    {
        for (int i = 0; i < controls.Count; i++)
        {
            if (controls[i] is Guna2TextBox)
                SetControl(controls[i], false);
            else
                SetControl(controls[i]);
            if (controls[i] is Panel || controls[i] is CustomerRow || controls[i] is TransactionRow)
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
