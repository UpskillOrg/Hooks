using HooksCore.WinApi;

namespace HooksCore.Implementation;

internal class GlobalMouseListener : MouseListener
{
    public GlobalMouseListener() : base(HookHelper.HookGlobalMouse)
    {
    }

    protected override MouseEventExtArgs GetEventArgs(CallbackData data)
    {
        return MouseEventExtArgs.FromRawDataGlobal(data);
    }
}