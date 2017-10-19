using System;
using strange.extensions.command.impl;
using UnityEngine;

public class MainContext : SignalContext
{
    public MainContext(MonoBehaviour view): base(view)
    {

    }

    protected override void mapBindings()
    {
        base.mapBindings();

        commandBinder.Bind<AppStartSignal>().To<AppStartCommand>().Once();
        commandBinder.Bind<OnFigurePressedSignal>().To<OnFigurePressCommand>();
        commandBinder.Bind<OnFigureUpSignal>().To<OnFigureUpCommand>();
    }
}