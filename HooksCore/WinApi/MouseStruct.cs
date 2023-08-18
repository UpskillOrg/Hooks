using System.Drawing;
using System.Runtime.InteropServices;

namespace HooksCore.WinApi;

[StructLayout(LayoutKind.Explicit)]
internal struct MouseStruct
{
    [FieldOffset(0x00)] public Point Point;
}