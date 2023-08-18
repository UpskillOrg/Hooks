using HooksCore.Hook;
using System.Windows.Forms;

namespace HooksCore.Implementation;

internal abstract class EventFacade : IMouseEvents
{
    private MouseListener _mouseListener;

    public event MouseEventHandler MouseClick
    {
        add { GetMouseListener().MouseClick += value; }
        remove { GetMouseListener().MouseClick -= value; }
    }

    public event MouseEventHandler MouseMove
    {
        add { GetMouseListener().MouseMove += value; }
        remove { GetMouseListener().MouseMove -= value; }
    }

    private MouseListener GetMouseListener()
    {
        var target = _mouseListener;
        if (target != null) return target;
        target = CreateMouseListener();
        _mouseListener = target;
        return target;
    }

    protected abstract MouseListener CreateMouseListener();

    public void Dispose()
    {
        if (_mouseListener != null) _mouseListener.Dispose();
    }
}