namespace Confetti;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        
        ApplicationConfiguration.Initialize();
        
        var screens = Screen.PrimaryScreen;
        if (screens == null) return;
        var confettiForm = new ConfettiForm(screens.Bounds);
        Application.Run(confettiForm);
    }
}