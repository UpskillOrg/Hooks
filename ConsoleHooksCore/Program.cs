using HooksCore.Helpers;
using HooksCore.Hook;
using System;
using System.Windows.Forms;

namespace ConsoleHooksCore;

internal class Program
{
    private static IMouseEvents _mouseEvents;

    static void Main(string[] args)
    {
        var mouseEvents = Hook.GlobalEvents();
        Subscribe(mouseEvents);

        Console.WriteLine("Press Enter To Exit");

        Application.Run(new ApplicationContext());

        Console.ReadKey();
        Unsubscribe();
    }

    private static void Subscribe(IMouseEvents events)
    {
        _mouseEvents = events;
        _mouseEvents.MouseMove += HookManager_MouseMove;
        _mouseEvents.MouseClick += HookManager_MouseClick;
    }

    private static void HookManager_MouseClick(object sender, MouseEventArgs e)
    {
        Console.WriteLine(string.Format("MouseClick:{0}", e.Button));
    }

    private static void HookManager_MouseMove(object sender, MouseEventArgs e)
    {
        Console.WriteLine(string.Format(@"{0}: X={1},Y={2}", ExeNameHelper.GetName(new POINT { x = e.X, y = e.Y }), e.X, e.Y));
    }

    private static void Unsubscribe()
    {
        _mouseEvents.Dispose();
        _mouseEvents = null;
    }
}
