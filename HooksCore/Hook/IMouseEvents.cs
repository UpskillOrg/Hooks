using System;
using System.Windows.Forms;

namespace HooksCore.Hook;

public interface IMouseEvents : IDisposable
{
    event MouseEventHandler MouseMove;

    event MouseEventHandler MouseClick;
}