using HooksCore.Extentions;
using HooksCore.Hook;
using HooksCore.WinApi;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HooksCore.Implementation;

internal abstract class MouseListener : BaseListener, IMouseEvents
{
    private Point previousPoint;
    protected readonly Point _uninitialisedPoint = new Point(-99999, -99999);
    private readonly MouseButtonSet singleDown;
    public event MouseEventHandler MouseUp;

    protected MouseListener(Subscribe subscribe)
        : base(subscribe)
    {
        previousPoint = _uninitialisedPoint;
        singleDown = new MouseButtonSet();
    }

    protected override bool Callback(CallbackData data)
    {
        var mouseEventArgs = GetEventArgs(data);

        if (HasMoved(mouseEventArgs.Point))
            ProcessMove(ref mouseEventArgs);

        if (mouseEventArgs.IsMouseButtonUp)
            ProcessUp(ref mouseEventArgs);

        if (mouseEventArgs.IsMouseButtonDown)
            ProcessDown(ref mouseEventArgs);

        return true;
    }

    private void ProcessMove(ref MouseEventExtArgs e)
    {
        previousPoint = e.Point;
       
        OnMove(e);
    }

    protected virtual void OnMove(MouseEventArgs e)
    {
        var handler = MouseMove;
        if (handler != null) handler(this, e);
    }

    private bool HasMoved(Point actualPoint)
    {
        return previousPoint != actualPoint;
    }

    protected virtual void ProcessDown(ref MouseEventExtArgs e)
    {
        if (e.Clicks == 1)
            singleDown.Add(e.Button);
    }

    protected virtual void ProcessUp(ref MouseEventExtArgs e)
    {    
        if (singleDown.Contains(e.Button))
        {
            OnClick(e);
            singleDown.Remove(e.Button);
        }
    }

    protected virtual void OnUp(MouseEventExtArgs e)
    {
        var handler = MouseUp;
        if (handler != null) handler(this, e);
    }

    protected virtual void OnClick(MouseEventExtArgs e)
    {
        var handler = MouseClick;
        if (handler != null) handler(this, e);
    }

    protected abstract MouseEventExtArgs GetEventArgs(CallbackData data);

    public event MouseEventHandler MouseMove;

    public event MouseEventHandler MouseClick;
}