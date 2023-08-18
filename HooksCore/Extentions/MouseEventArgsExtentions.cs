using System.Drawing;
using System.Windows.Forms;

namespace HooksCore.Extentions
{
    internal static class MouseEventArgsExtentions
    {
        public static Point GetPoint(this MouseEventArgs eventArgs)
        {
            return new Point { X = eventArgs.X, Y = eventArgs.Y };
        }
    }
}
