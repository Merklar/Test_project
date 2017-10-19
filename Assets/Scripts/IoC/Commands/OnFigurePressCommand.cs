using strange.extensions.command.impl;
using UnityEngine;

public class OnFigurePressCommand : Command
{
    [Inject]
    public Transform Transform { get; private set; }

    public override void Execute()
    {
        GameManagerFacade.OnFigurePressed(Transform);
    }
}
