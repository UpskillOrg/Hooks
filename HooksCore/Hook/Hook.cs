using HooksCore.Implementation;

namespace HooksCore.Hook;

public static class Hook
{
    public static IMouseEvents GlobalEvents()
    {
        var globalEventFacade = new GlobalEventFacade();
        return globalEventFacade;
    }
}