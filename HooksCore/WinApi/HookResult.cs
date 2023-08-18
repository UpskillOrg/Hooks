using System;

namespace HooksCore.WinApi;

public class HookResult : IDisposable
{
    public HookResult(HookProcedureHandle handle, HookProcedure procedure)
    {
        Handle = handle;
        Procedure = procedure;
    }

    public HookProcedureHandle Handle { get; }

    public HookProcedure Procedure { get; }

    public void Dispose()
    {
        Handle.Dispose();
    }
}