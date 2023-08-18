using HooksCore.WinApi;
using System;

namespace HooksCore.Implementation;

internal abstract class BaseListener : IDisposable
{
    private HookResult Handle { get; set; }

    protected BaseListener(Subscribe subscribe)
    {
        Handle = subscribe(Callback);
    }

    public void Dispose()
    {
        Handle.Dispose();
    }

    protected abstract bool Callback(CallbackData data);
}