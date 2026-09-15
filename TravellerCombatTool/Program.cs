using System;
using System.Windows.Forms;

namespace TravellerTools.TravellerCombatTool;

/// <summary>Starts the application's Windows Forms message loop.</summary>
internal static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new TravellerCombatTool());
    }
}
