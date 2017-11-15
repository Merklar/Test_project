using strange.extensions.command.impl;

public class GameOverCommand : Command
{
   
    public override void Execute()
    {
        UIFacade.GameOver();
        ControllerFacade.GameOver();
    }
}
