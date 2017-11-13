using strange.extensions.command.impl;

public class StartDragCommand : Command
{
    public override void Execute()
    {
        GameManagerFacade.OnStartDrag();
    }
}