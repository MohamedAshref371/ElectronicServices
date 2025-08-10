namespace ElectronicServices
{
    internal static class Program
    {
        public static Form1 Form;
        [STAThread]
        static void Main()
        {
            
            ApplicationConfiguration.Initialize();

            if (!File.Exists("Guna.UI2.dll"))
            {
                MessageBox.Show("Guna.UI2.dll file is missing.\ngithub.com/MohamedAshref371/ElectronicServices");
                return;
            }

            Application.ThreadException += (sender, e) =>
                LogError(e.Exception);

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Exception ex = (Exception)e.ExceptionObject;
                LogError(ex);
            };

            try
            {
                Mutex mutex = new(true, Path.GetFullPath("data").Replace(":", "").Replace("\\", ""), out bool createdNew);
                if (createdNew)
                {
                    Form = new Form1();
                    Application.Run(Form);
                    mutex.ReleaseMutex();
                }
                else MessageBox.Show("áÞÏ ÝÊÍÊ ÇáÈÑäÇãÌ ÈÇáÝÚá");
            }
            catch (Exception ex)
            {
                LogError(ex);
                MessageBox.Show("ÍÏË ÎØÃ ÛíÑ ãÊæÞÚ¡ ÓíÊã ÅÛáÇÞ ÇáÈÑäÇãÌ");
            }
        }

        public static void LogError(Exception ex, bool inTryCatch = false)
            => File.AppendAllText("Errors.txt", $"{DateTime.Now}{(inTryCatch ? "  -  Inside Custom Try-Catch Block" : "")}\n{ex.Message}\n{ex.StackTrace}\n------------------\n\n");

    }
}