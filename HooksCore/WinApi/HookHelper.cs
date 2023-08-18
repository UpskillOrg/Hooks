using HooksCore.Implementation;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace HooksCore.WinApi;

internal static class HookHelper
{
    private static HookProcedure _globalHookProc;

    public static HookResult HookGlobalMouse(Callback callback)
    {
        return HookGlobal(HookIds.WH_MOUSE_LL, callback);
    }

    private static HookResult HookGlobal(int hookId, Callback callback)
    {
        _globalHookProc = (code, param, lParam) => HookProcedure(code, param, lParam, callback);

        var hookHandle = HookNativeMethods.SetWindowsHookEx(
            hookId,
            _globalHookProc,
            Process.GetCurrentProcess().MainModule.BaseAddress,
            0);

        if (hookHandle.IsInvalid)
            ThrowLastUnmanagedErrorAsException();

        return new HookResult(hookHandle, _globalHookProc);
    }

    private static nint HookProcedure(int nCode, nint wParam, nint lParam, Callback callback)
    {
        var passThrough = nCode != 0;
        if (passThrough)
            return CallNextHookEx(nCode, wParam, lParam);

        var callbackData = new CallbackData(wParam, lParam);
        var continueProcessing = callback(callbackData);

        if (!continueProcessing)
            return new nint(-1);

        return CallNextHookEx(nCode, wParam, lParam);
    }

    private static nint CallNextHookEx(int nCode, nint wParam, nint lParam)
    {
        return HookNativeMethods.CallNextHookEx(nint.Zero, nCode, wParam, lParam);
    }

    private static void ThrowLastUnmanagedErrorAsException()
    {
        var errorCode = Marshal.GetLastWin32Error();
        throw new Win32Exception(errorCode);
    }
}
