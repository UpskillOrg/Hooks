namespace HooksCore.WinApi;

public delegate nint HookProcedure(int nCode, nint wParam, nint lParam);
