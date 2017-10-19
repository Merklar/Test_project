using strange.extensions.command.impl;

public class OnFigureUpCommand : Command
{

    public override void Execute()
    {
        GameManagerFacade.OnFigureUp();
    }
}
