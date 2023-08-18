using System.Runtime.InteropServices;

namespace HooksCore.Helpers;

[StructLayout(LayoutKind.Sequential)]
public struct POINT
{
    public int x;
    public int y;
}
