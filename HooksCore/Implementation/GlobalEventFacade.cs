namespace HooksCore.Implementation;

internal class GlobalEventFacade : EventFacade
{
    protected override MouseListener CreateMouseListener()
    {
        return new GlobalMouseListener();
    }
}