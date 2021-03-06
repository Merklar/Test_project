﻿using System;
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
        commandBinder.Bind<OnStartDragSignal>().To<StartDragCommand>();
        commandBinder.Bind<OnPlaySoundSignal>().To<PlaySoundCommand>();
        commandBinder.Bind<GameOverSignal>().To<GameOverCommand>();
    }
}