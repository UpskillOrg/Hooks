using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace HooksCore.Helpers;

public static class ExeNameHelper
{
    // Define the process access rights
    const uint PROCESS_TERMINATE = 0x0001;
    const uint PROCESS_QUERY_INFORMATION = 0x0400;
    const uint PROCESS_VM_READ = 0x0010;

    [DllImport("psapi.dll", CharSet = CharSet.Auto)]
    static extern int GetModuleFileNameEx(nint hProcess, nint hModule, StringBuilder lpFilename, int nSize);

    [DllImport("kernel32.dll")]
    static extern nint OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

    [DllImport("user32.dll")]
    static extern nint WindowFromPoint(POINT Point);

    [DllImport("user32.dll")]
    static extern bool GetCursorPos(out POINT lpPoint);

    [DllImport("user32.dll")]
    static extern uint GetWindowThreadProcessId(nint hWnd, out uint lpdwProcessId);

    public static string GetName(POINT point)
    {
        nint hWnd = WindowFromPoint(point);
        GetWindowThreadProcessId(hWnd, out uint processId);
        nint hProcess = OpenProcess(PROCESS_TERMINATE | PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, processId);
        var lpFilename = new StringBuilder(260);
        int result = GetModuleFileNameEx(hProcess, nint.Zero, lpFilename, lpFilename.Capacity);
        if (result != 0)
        {
            return Path.GetFileName(lpFilename.ToString());
        }
        return null;
    }
}
