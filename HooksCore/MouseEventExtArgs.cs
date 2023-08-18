using HooksCore.WinApi;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HooksCore
{
    public class MouseEventExtArgs : MouseEventArgs
    {
        internal MouseEventExtArgs(MouseButtons buttons, int clicks, Point point,
            bool isMouseButtonDown, bool isMouseButtonUp)
            : base(buttons, clicks, point.X, point.Y, 0)
        {
            IsMouseButtonDown = isMouseButtonDown;
            IsMouseButtonUp = isMouseButtonUp;            
        }

        public bool Clicked
        {
            get { return Clicks > 0; }
        }

        public bool IsMouseButtonDown { get; }

        public bool IsMouseButtonUp { get; }

        internal Point Point
        {
            get { return new Point(X, Y); }
        }

        internal static MouseEventExtArgs FromRawDataGlobal(CallbackData data)
        {
            var wParam = data.WParam;
            var lParam = data.LParam;
            var mSwapButton = data.MSwapButton;

            var marshalledMouseStruct = (MouseStruct)Marshal.PtrToStructure(lParam, typeof(MouseStruct));
            return FromRawDataUniversal(wParam, marshalledMouseStruct, mSwapButton);
        }

        private static MouseEventExtArgs FromRawDataUniversal(IntPtr wParam, MouseStruct mouseInfo, int mSwapButton)
        {
            var button = MouseButtons.None;
            var clickCount = 0;

            var isMouseButtonDown = false;
            var isMouseButtonUp = false;

            switch ((long)wParam)
            {
                case Messages.WM_LBUTTONDOWN:
                    isMouseButtonDown = true;
                    button = MouseButtons.Left;
                    clickCount = 1;
                    break;
                case Messages.WM_LBUTTONUP:
                    isMouseButtonUp = true;
                    button = MouseButtons.Left;
                    clickCount = 1;
                    break;
                case Messages.WM_LBUTTONDBLCLK:
                    isMouseButtonDown = true;
                    button = MouseButtons.Left;
                    clickCount = 2;
                    break;
                case Messages.WM_RBUTTONDOWN:
                    isMouseButtonDown = true;
                    button = MouseButtons.Right;
                    clickCount = 1;
                    break;
                case Messages.WM_RBUTTONUP:
                    isMouseButtonUp = true;
                    button = MouseButtons.Right;
                    clickCount = 1;
                    break;
                case Messages.WM_RBUTTONDBLCLK:
                    isMouseButtonDown = true;
                    button = MouseButtons.Right;
                    clickCount = 2;
                    break;
                case Messages.WM_MBUTTONDOWN:
                    isMouseButtonDown = true;
                    button = MouseButtons.Middle;
                    clickCount = 1;
                    break;
                case Messages.WM_MBUTTONUP:
                    isMouseButtonUp = true;
                    button = MouseButtons.Middle;
                    clickCount = 1;
                    break;
                case Messages.WM_MBUTTONDBLCLK:
                    isMouseButtonDown = true;
                    button = MouseButtons.Middle;
                    clickCount = 2;
                    break;               
            }

            if (mSwapButton > 0)
            {
                button = button == MouseButtons.Left ? MouseButtons.Right : button == MouseButtons.Right ? MouseButtons.Left : button;
            }

            var e = new MouseEventExtArgs(
                button,
                clickCount,
                mouseInfo.Point,
                isMouseButtonDown,
                isMouseButtonUp);

            return e;
        }
    }
}
