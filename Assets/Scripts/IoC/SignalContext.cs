using UnityEngine;
using strange.extensions.command.impl;
using strange.extensions.command.api;
using strange.extensions.context.impl;
using strange.extensions.context.api;

public class SignalContext : MVCSContext
{
    public static OnFigurePressedSignal OnFigurePressedSignal { get; private set; }
    public static OnFigureUpSignal OnFigureUpSignal { get; private set; }
    public static OnStartDragSignal OnStartDragSignal { get; private set; }


    public SignalContext (MonoBehaviour contextView): base(contextView, ContextStartupFlags.MANUAL_MAPPING)
    {

    }

    protected override void addCoreComponents()
    {
        base.addCoreComponents();

        injectionBinder.Unbind<ICommandBinder>();
        injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
    }

    public override void Launch()
    {
        base.Launch();

        var startSignal = injectionBinder.GetInstance<AppStartSignal>();
        OnFigurePressedSignal = injectionBinder.GetInstance<OnFigurePressedSignal>();
        OnFigureUpSignal = injectionBinder.GetInstance<OnFigureUpSignal>();
        OnStartDragSignal = injectionBinder.GetInstance<OnStartDragSignal>();
        startSignal.Dispatch();
    }

    protected override void mapBindings()
    {
        base.mapBindings();
    }
}