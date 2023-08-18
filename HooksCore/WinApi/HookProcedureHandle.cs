using Microsoft.Win32.SafeHandles;

namespace HooksCore.WinApi;

public class HookProcedureHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    static HookProcedureHandle()
    {
    }

    public HookProcedureHandle()
        : base(true)
    {
    }

    protected override bool ReleaseHandle()
    {
        var ret = HookNativeMethods.UnhookWindowsHookEx(handle);
        if (ret != 0)
        {
            Dispose();
            return true;
        }
        else
            return true;
    }
}