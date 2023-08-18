namespace HooksCore.WinApi;

internal struct CallbackData
{
    internal CallbackData(nint wParam, nint lParam, int mSwapButton = 0)
    {
        WParam = wParam;
        LParam = lParam;
        MSwapButton = mSwapButton;
    }

    internal nint WParam { get; }

    internal nint LParam { get; }

    internal int MSwapButton { get; set; }
}
