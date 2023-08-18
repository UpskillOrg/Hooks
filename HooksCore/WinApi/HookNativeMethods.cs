using System.Runtime.InteropServices;

namespace HooksCore.WinApi;

internal static class HookNativeMethods
{
    [DllImport("user32.dll", CharSet = CharSet.Auto,
        CallingConvention = CallingConvention.StdCall)]
    internal static extern nint CallNextHookEx(
        nint idHook,
        int nCode,
        nint wParam,
        nint lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto,
        CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    internal static extern HookProcedureHandle SetWindowsHookEx(
        int idHook,
        HookProcedure lpfn,
        nint hMod,
        int dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto,
        CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    internal static extern int UnhookWindowsHookEx(nint idHook);
}